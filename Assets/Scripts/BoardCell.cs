using UnityEngine;

[System.Serializable]
public class BoardCell : MonoBehaviour
{
    private Player owner;
    [SerializeField] private MatchPieceSO _matchPiece;
    [SerializeField] private GridPoint _gridPoint;
    [SerializeField] private ActivePieceController _activePieceController;

    public MatchPieceSO MatchPiece { get => _matchPiece; set => _matchPiece = value; }
    public GridPoint GridPoint { get => _gridPoint; set => _gridPoint = value; }
    public ActivePieceController ActivePieceController { get => _activePieceController; set => _activePieceController = value; }

    public BoardCell(MatchPieceSO matchPiece, GridPoint gridPoint)
    {
        this._matchPiece = matchPiece;
        this._gridPoint = gridPoint;
    }
    //public void SetPiece(MatchPieceSO matchPiece)
    //{
    //    _matchPiece = matchPiece;
    //    _activePieceController.SetUp(_matchPiece);
    //}
    public void SetPiece(ActivePieceController piece)
    {
        owner = piece.Owner;
        _activePieceController = piece;
        _matchPiece = (piece == null) ? owner.Game.BlankPiece : piece.MatchPiece;
        if (piece == null)
            return;
        piece.GridPoint = _gridPoint;
    }
}
