using UnityEngine;

public class Line
{
    Orientation orientation;
    Vector2Int coords;

    public Line(Orientation orientation, Vector2Int coords)
    {
        this.orientation = orientation;
        this.coords = coords;
    }

    public Orientation Orientation { get => this.orientation; set => this.orientation = value; }
    public Vector2Int Coords { get => this.coords; set => this.coords = value; }
}

public enum Orientation
{
    Horizontal = 0,
    Vertical = 1,
}