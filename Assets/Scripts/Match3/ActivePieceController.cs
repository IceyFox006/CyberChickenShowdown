using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActivePieceController : MonoBehaviour
{
    private Player owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;
    [SerializeField] private Image _selectedBorder;

    private Vector2 positionOnBoard;
    public bool isUpdating = false;

    public GridPoint GridPoint { get => _gridPoint; set => _gridPoint = value; }
    public Vector2 PositionOnBoard { get => positionOnBoard; set => positionOnBoard = value; }
    public Image SelectedBorder { get => _selectedBorder; set => _selectedBorder = value; }
    public MatchPieceSO MatchPiece { get => _matchPiece; set => _matchPiece = value; }
    public bool IsUpdating { get => isUpdating; set => isUpdating = value; }
    public Player Owner { get => owner; set => owner = value; }

    public void SetUp(MatchPieceSO matchPiece)
    {
        _matchPiece = matchPiece;
        owner.Game.GameBoard[_gridPoint.X, _gridPoint.Y].MatchPiece = matchPiece;
        if (matchPiece.Element.Element != Enums.Element.Empty)
            GetComponent<Image>().enabled = true;
        ApplySprite();
        SetUpInteractability();
    }
    public void SetUp()
    {
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
    public bool UpdatePiece()
    {
        if (Vector3.Distance(GetComponent<RectTransform>().anchoredPosition, positionOnBoard) > 0.05f) //
        {
            MovePositionTo(positionOnBoard);
            isUpdating = true;
            return true;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = positionOnBoard;
            isUpdating = false;
            return false;
        }

    }
    public void Select()
    {
        if (isUpdating || ((int)_matchPiece.Element.Element < 1))
            return;
        owner.Game.IsSelecting = true;
        if (owner.Game.PieceMover.MovingPiece == null)
        {
            owner.Game.StartSwapPiece = this; //
            owner.Game.PieceMover.MovePiece(this);
            DisableAllButtonsExceptSurrounding();
        }
        else
        {
            owner.Game.EndSwapPiece = this;
            owner.Game.PieceMover.DropPiece(true);
            owner.Game.DeselectAllPieces();
        }
    }
    public void OnEnterHover()
    {
        owner.Game.PieceMover.MoveToSpot = transform.position;
        _selectedBorder.enabled = true;
    }
    public void OnExitHover()
    {
        if (!owner.Game.IsSelecting)
            _selectedBorder.enabled = false;
    }
    public void PlayBreakParticles()
    {
        if (_matchPiece.BreakParticlePrefab == null)
            return;
        GameObject particlesGO = Instantiate(_matchPiece.BreakParticlePrefab, this.transform);
    }
    private void MovePosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition += position * Time.deltaTime * (owner.Game.PieceSize.x / 4f);
    }
    public void MovePositionTo(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, position, Time.deltaTime * (owner.Game.PieceSize.x / 2f));
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
    public void ResetPositionOnBoard()
    {
        positionOnBoard = new Vector2(owner.Game.HolderStartOffset.x + (owner.Game.PieceSize.x * _gridPoint.X), owner.Game.HolderStartOffset.y - (owner.Game.PieceSize.y * _gridPoint.Y));
    }
}
