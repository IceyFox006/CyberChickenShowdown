using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Player owner;
    private ElementSO element;
    private List<GridPoint> connectedPoints;

    public Player Owner { get => owner; set => owner = value; }
    public ElementSO Element { get => element; set => element = value; }
    public List<GridPoint> ConnectedPoints { get => connectedPoints; set => connectedPoints = value; }
}
