using UnityEngine;

public class Divider
{
    Direction dir;
    Vector2Int coords;

    public Divider(Direction dir, Vector2Int coords)
    {
        this.dir = dir;
        this.coords = coords;
    }

    public Direction Direction
    {
        get => this.dir;
        set => this.dir = value;
    }
    public Vector2Int Coords
    {
        get => this.coords;
        set => this.coords = value;
    }
}

public enum Direction
{
    Horizontal = 0,
    Vertical = 1,
}
