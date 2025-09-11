using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    private Player owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;
    [SerializeField] private Image _selectedBorder;

    private Vector2 positionOnBoard;

    public GridPoint GridPoint { get => _gridPoint; set => _gridPoint = value; }
    public Vector2 PositionOnBoard { get => positionOnBoard; set => positionOnBoard = value; }
    public Image SelectedBorder { get => _selectedBorder; set => _selectedBorder = value; }
    public MatchPieceSO MatchPiece { get => _matchPiece; set => _matchPiece = value; }

    public void SetUp(MatchPieceSO matchPiece)
    {
        _matchPiece = matchPiece;
        ApplySprite();
        SetUpInteractability();
    }
    public void SetUp(Player owner, MatchPieceSO matchPiece, GridPoint gridPoint)
    {
        this.owner = owner;
        _matchPiece = matchPiece;
        SetPosition(gridPoint);
        ApplySprite();
        SetUpInteractability();
    }

    public void Select()
    {
        if (owner.Game.PieceMover.CurrentSelectedPiece == null)
        {
            owner.Game.PieceMover.CurrentSelectedPiece = this;
            DisableAllButtonsExceptSurrounding();
            _selectedBorder.enabled = true;
            _selectedBorder.sprite = owner.Game.PieceMover.CurrentlySelectedBorderSprite;
            Debug.Log(owner.Name + "-\t selects [" + _gridPoint.X + "," + _gridPoint.Y + "]");
        }
        else
        {
            owner.Game.PieceMover.PreviousSelectedPiece = owner.Game.PieceMover.CurrentSelectedPiece;
            SwapPieces(owner.Game.PieceMover.PreviousSelectedPiece);
            owner.Game.DeselectAllPieces();
        }
    }
    private void SwapPieces(ActivePieceController swapFromPiece)
    {
        Debug.Log(owner.Name + "-\t swaps " + "[" + swapFromPiece.GridPoint.X + ", " + swapFromPiece.GridPoint.Y + "]" + "with [" + _gridPoint.X + "," + _gridPoint.Y + "]");
        MatchPieceSO swapToPieceHolder = _matchPiece;
        SetUp(swapFromPiece.MatchPiece);
        swapFromPiece.SetUp(swapToPieceHolder);

        //_matchPiece = swapFromPiece.MatchPiece;
        //swapFromPiece.MatchPiece = swapToPieceHolder;

    }
    private void DisableAllButtonsExceptSurrounding()
    {
        GridPoint upPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.up.X, _gridPoint.Y + GridPoint.up.Y);
        GridPoint downPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.down.X, _gridPoint.Y + GridPoint.down.Y);
        GridPoint rightPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.right.X, _gridPoint.Y + GridPoint.right.Y);
        GridPoint leftPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.left.X, _gridPoint.Y + GridPoint.left.Y);
        GridPoint[] pointsSelectable = {_gridPoint, upPieceGridPoint, downPieceGridPoint, rightPieceGridPoint, leftPieceGridPoint};
    
        for (int x = 0; x < owner.Game.BoardWidth; x++)
        {
            for (int y = 0; y < owner.Game.BoardHeight; y++)
                owner.Game.GameBoard[x, y].ActivePieceController.GetComponent<Button>().enabled = false;
        }
        foreach (GridPoint gridPoint in pointsSelectable)
        {
            if (!owner.Game.IsGridPointInBounds(gridPoint) || owner.Game.GameBoard[gridPoint.X, gridPoint.Y].MatchPiece.BoardFunction == Enums.MatchPieceFunction.Unmoveable)
                continue;
            owner.Game.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.GetComponent<Button>().enabled = true;
            owner.Game.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.SelectedBorder.enabled = true;
            owner.Game.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.SelectedBorder.sprite = owner.Game.PieceMover.PreviouslySelectedBorderSprite;
        }
    }
    private void MovePiecePosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition += position * Time.deltaTime * (owner.Game.PieceSize.x / 4f);
    }
    public void MovePiecePositionTo(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, position, Time.deltaTime * (owner.Game.PieceSize.x / 4f));
    }
    public void ReleasePiece()
    {
        Debug.Log("Releases\t\t[" + _gridPoint.X + "," + _gridPoint.Y + "]");
    }

    private void SetPosition(GridPoint gridPoint)
    {
        _gridPoint = gridPoint;
        ResetPositionOnBoard();
    }
    private void SetUpInteractability()
    {
        if (_matchPiece.BoardFunction != Enums.MatchPieceFunction.Unmoveable)
            return;
        GetComponent<Button>().enabled = false;
    }
    private void ApplySprite()
    {
        gameObject.GetComponent<Image>().sprite = _matchPiece.Sprite;
    }
    private void ResetPositionOnBoard()
    {
        Debug.Log(owner.Name);
        positionOnBoard = new Vector2(owner.Game.HolderStartOffset.x + (owner.Game.PieceSize.x * _gridPoint.X), owner.Game.HolderStartOffset.y - (owner.Game.PieceSize.y * _gridPoint.Y));
    }
}
