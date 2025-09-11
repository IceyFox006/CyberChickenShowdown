using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    private PlayerMatch3 owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;
    [SerializeField] private Image _selectedBorder;

    private Vector2 positionOnBoard;

    public GridPoint GridPoint { get => _gridPoint; set => _gridPoint = value; }
    public Vector2 PositionOnBoard { get => positionOnBoard; set => positionOnBoard = value; }
    public Image SelectedBorder { get => _selectedBorder; set => _selectedBorder = value; }

    public void SetUp(PlayerMatch3 owner, MatchPieceSO matchPiece, GridPoint gridPoint)
    {
        this.owner = owner;
        _matchPiece = matchPiece;
        SetPosition(gridPoint);
        ApplySprite();
        SetUpInteractability();
    }

    public void Select()
    {
        owner.PieceMover.CurrentSelectedPiece = this;
        DisableAllButtonsExceptSurrounding();
        _selectedBorder.gameObject.SetActive(true);
        _selectedBorder.sprite = owner.PieceMover.CurrentlySelectedBorderSprite;

        Debug.Log(owner.PlayerName + "-\t selects [" + _gridPoint.X + "," + _gridPoint.Y + "]");
    }
    public void Deselect()
    {
        if (owner.PieceMover.PreviousSelectedPiece == null) 
            return;
        owner.PieceMover.PreviousSelectedPiece.SelectedBorder.gameObject.SetActive(false);

        Debug.Log(owner.PlayerName + "-\t deselects [" + _gridPoint.X + "," + _gridPoint.Y + "]");
    }
    private void DisableAllButtonsExceptSurrounding()
    {
        GridPoint upPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.up.X, _gridPoint.Y + GridPoint.up.Y);
        GridPoint downPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.down.X, _gridPoint.Y + GridPoint.down.Y);
        GridPoint rightPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.right.X, _gridPoint.Y + GridPoint.right.Y);
        GridPoint leftPieceGridPoint = new GridPoint(_gridPoint.X + GridPoint.left.X, _gridPoint.Y + GridPoint.left.Y);
        GridPoint[] pointsSelectable = {_gridPoint, upPieceGridPoint, downPieceGridPoint, rightPieceGridPoint, leftPieceGridPoint};
    
        for (int x = 0; x < owner.BoardWidth; x++)
        {
            for (int y = 0; y < owner.BoardHeight; y++)
                owner.GameBoard[x, y].ActivePieceController.GetComponent<Button>().enabled = false;

        }
        foreach (GridPoint gridPoint in pointsSelectable)
        {
            if (!owner.IsGridPointInBounds(gridPoint) || owner.GameBoard[gridPoint.X, gridPoint.Y].MatchPiece.BoardFunction == Enums.MatchPieceFunction.Unmoveable)
                continue;
            owner.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.GetComponent<Button>().enabled = true;
            owner.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.SelectedBorder.gameObject.SetActive(true);
            owner.GameBoard[gridPoint.X, gridPoint.Y].ActivePieceController.SelectedBorder.sprite = owner.PieceMover.PreviouslySelectedBorderSprite;
        }
    }
    private void MovePiecePosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition += position * Time.deltaTime * (owner.PieceSize.x / 4f);
    }
    public void MovePiecePositionTo(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, position, Time.deltaTime * (owner.PieceSize.x / 4f));
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
        positionOnBoard = new Vector2(owner.HolderStartOffset.x + (owner.PieceSize.x * _gridPoint.X), owner.HolderStartOffset.y - (owner.PieceSize.y * _gridPoint.Y));
    }
}
