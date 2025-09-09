using UnityEngine;

[System.Serializable]
public class BoardCell : MonoBehaviour
{
    //[SerializeField] private int _index; //0 = none, 1 = red, 2 = yellow, 3 = green, 4 = blue, 5 = purple
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
}
