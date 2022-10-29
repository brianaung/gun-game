using UnityEngine;

public class Room : Node
{
    public Room(Vector2Int botLeft, Vector2Int topRight, Node parent, int index) : base(parent)
    {
        this.BotLeft = botLeft;
        this.TopRight = topRight;
        this.BotRight = new Vector2Int(topRight.x, botLeft.y);
        this.TopLeft = new Vector2Int(botLeft.x, topRight.y);
        this.Index = index;
    }

    public int Width
    {
        get => (int)(this.TopRight.x - this.BotLeft.x);
    }
    public int Length
    {
        get => (int)(this.TopRight.y - this.BotLeft.y);
    }
}
