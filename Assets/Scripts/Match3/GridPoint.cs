/*
 * Marlow Greenan
 * 8/31/2025
 */
using UnityEngine;

[System.Serializable]
public class GridPoint
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }

    public GridPoint(int  x, int y)
    {
        _x = x;
        _y = y;
    }

    //Returns true if x and y are the same value as the gridPoint x and y.
    public bool Equals(GridPoint gridPoint)
    {
        return (_x == gridPoint.X && _y == gridPoint.Y);
    }

    //Returns a vector2 with the values of x and y.
    public Vector2 ToVector2()
    {
        return new Vector2(_x, _y);
    }

    //Adds the gridPoint values to x and y.
    public void Add(GridPoint gridPoint)
    {
        _x += gridPoint.X;
        _y += gridPoint.Y;
    }

    //Multiplies x and y by multiplier.
    public void Multiply(int mutiplier)
    {
        _x *= mutiplier;
        _y *= mutiplier;
    }

    public string AsString()
    {
        return "[ " + X + ", " + Y + " ]";
    }

    //Returns a new gridpoint from a vector2.
    public static GridPoint TransformFromVector(Vector2 vector2)
    {
        return new GridPoint((int)vector2.x, (int)vector2.y);
    }

    //Returns a new gridpoint from a vector3
    public static GridPoint TransformFromVector(Vector3 vector3)
    {
        return new GridPoint((int)vector3.x, (int)vector3.y);
    }

    //Returns a new GridPoint that has the added values of gridPoint1 and gridPoint2.
    public static GridPoint Add(GridPoint gridPoint1, GridPoint gridPoint2)
    {
        return new GridPoint(gridPoint1.X + gridPoint2.X, gridPoint1.Y + gridPoint2.Y);
    }

    //Returns a new GridPoint that is gridPoint multiplied by mulitplier.
    public static GridPoint Multiply(GridPoint gridPoint, int mutiplier)
    {
        return new GridPoint(gridPoint.X *  mutiplier, gridPoint.Y * mutiplier);
    }

    //Creates a new GridPoint with the same x and y values as gridPoint.
    public static GridPoint PositionClone(GridPoint gridPoint)
    {
        return new GridPoint(gridPoint._x, gridPoint._y);
    }

    public static GridPoint zero
    {
        get { return new GridPoint(0, 0); }
    }
    public static GridPoint one
    {
        get { return new GridPoint(1, 1); }
    }
    public static GridPoint up
    {
        get { return new GridPoint(0, 1); }
    }
    public static GridPoint down
    {
        get { return new GridPoint(0, -1); }
    }
    public static GridPoint right
    {
        get { return new GridPoint(1, 0); }
    }
    public static GridPoint left
    {
        get { return new GridPoint(-1, 0); }
    }
}
