using UnityEngine;

public class HolderPiece
{
    private GridPoint gridPoint;
    private ElementSO element;
    private MatchPieceSO matchPiece;

    public GridPoint GridPoint { get => gridPoint; set => gridPoint = value; }
    public ElementSO Element { get => element; set => element = value; }
    public MatchPieceSO MatchPiece { get => matchPiece; set => matchPiece = value; }

    public HolderPiece(GridPoint gridPoint, ElementSO element)
    {
        this.gridPoint = gridPoint;
        this.element = element;
    }
    public HolderPiece(GridPoint gridPoint, MatchPieceSO matchPiece)
    {
        this.gridPoint = gridPoint;
        this.matchPiece = matchPiece;
    }
}
