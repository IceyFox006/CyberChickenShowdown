using UnityEngine;

public class HolderPiece
{
    private GridPoint gridPoint;
    private ElementSO element;

    public GridPoint GridPoint { get => gridPoint; set => gridPoint = value; }
    public ElementSO Element { get => element; set => element = value; }

    public HolderPiece(GridPoint gridPoint, ElementSO element)
    {
        this.gridPoint = gridPoint;
        this.element = element;
    }
}
