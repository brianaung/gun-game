using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    private List<Node> childrenList;

    // getters and setters
    public Node Parent { get; set; }
    public List<Node> ChildrenList
    {
        get => childrenList;
    }
    public bool Visited { get; set; }
    public int Index { get; set; }
    public bool isCorridor { get; set; }
    public Vector2Int BotLeft { get; set; }
    public Vector2Int BotRight { get; set; }
    public Vector2Int TopRight { get; set; }
    public Vector2Int TopLeft { get; set; }

    public Node(Node parentNode)
    {
        childrenList = new List<Node>();
        this.Parent = parentNode;
        this.isCorridor = false;
        if (parentNode is not null)
        {
            parentNode.AddChild(this);
        }
    }

    public void AddChild(Node node)
    {
        childrenList.Add(node);
    }

    public void RemoveChild(Node node)
    {
        childrenList.Remove(node);
    }
}
