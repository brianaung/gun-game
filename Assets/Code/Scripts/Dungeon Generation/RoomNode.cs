using UnityEngine;

public class RoomNode : Node
{
    public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, Node parentNode, int index) : base(parentNode)
    {
        this.BottomLeftAreaCorner = bottomLeftAreaCorner;
        this.TopRightAreaCorner = TopRightAreaCorner;
        this.BottomRightAreaCorner = new Vector2Int(topRightAreaCorner.x, bottomLeftAreaCorner.y);
        this.TopLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, topRightAreaCorner.y);
        this.TreeLayerIndex = index;
    }

    public int Width { get => (int)(this.TopRightAreaCorner.x - this.BottomLeftAreaCorner.x); }
    public int Length { get => (int)(this.TopRightAreaCorner.y - this.BottomLeftAreaCorner.y); }
}