using UnityEngine;

[System.Serializable]
public class SwappedPieces
{
    private ActivePieceController startPiece;
    private ActivePieceController endPiece;

    public SwappedPieces(ActivePieceController startPiece, ActivePieceController endPiece)
    {
        this.startPiece = startPiece;
        this.endPiece = endPiece;
    }

    public ActivePieceController StartPiece { get => startPiece; set => startPiece = value; }
    public ActivePieceController EndPiece { get => endPiece; set => endPiece = value; }

    public ActivePieceController GetOtherPiece(ActivePieceController piece)
    {
        if (piece == startPiece)
            return endPiece;
        else if (piece == endPiece)
            return startPiece;
        else
            return null;
    }
}
