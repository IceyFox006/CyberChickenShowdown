using JetBrains.Annotations;
using System.Collections.Generic;

public class Match
{
    private Player owner;
    private ElementSO element;
    private List<GridPoint> connectedPoints;

    public Player Owner { get => owner; set => owner = value; }
    public ElementSO Element { get => element; set => element = value; }
    public List<GridPoint> ConnectedPoints { get => connectedPoints; set => connectedPoints = value; }

    public Match(Player owner, ElementSO element, List<GridPoint> connectedPoints)
    {
        this.owner = owner;
        this.element = element;
        this.connectedPoints = connectedPoints;

        DeleteDuplicates();
    }
    public void DeleteDuplicates()
    {

    }
}

