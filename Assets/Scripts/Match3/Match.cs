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

        ValidateConnectPoints();
    }
    public void ValidateConnectPoints()
    {
    }
}
//List<GridPoint> validatedPoints = new List<GridPoint>();
//bool foundDuplicate = false;
//for (int p1 = 0; p1 < connectedPoints.Count; p1++)
//{
//    foundDuplicate = false;
//    for (int p2 = p1 + 1; p2 < connectedPoints.Count; p2++)
//    {
//        if (connectedPoints[p1].Equals(connectedPoints[p2]))
//            foundDuplicate = true;
//    }
//}
//connectedPoints = validatedPoints;
