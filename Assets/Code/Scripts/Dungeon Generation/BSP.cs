using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BSP
{
    Room root;

    public Room Root
    {
        get => root;
    }

    public BSP(int dungeonWidth, int dungeonLength)
    {
        // create root node from 0,0 to max dungeon size
        this.root = new Room(
            new Vector2Int(0, 0),
            new Vector2Int(dungeonWidth, dungeonLength),
            null,
            0
        );
    }

    public List<Room> CreateRoomSpaces(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<Room> graph = new Queue<Room>();
        List<Room> roomSpaces = new List<Room>();
        graph.Enqueue(this.root);
        roomSpaces.Add(this.root);
        for (int i = 0; i < maxIterations && graph.Count > 0; i++)
        {
            Room currNode = graph.Dequeue();
            if (currNode.Width >= roomWidthMin * 2 || currNode.Length >= roomLengthMin * 2)
            {
                // split the room if size bigger than specified min values
                SplitSpace(currNode, roomSpaces, roomLengthMin, roomWidthMin, graph);
            }
        }
        return roomSpaces;
    }

    private void SplitSpace(
        Room currNode,
        List<Room> roomSpaces,
        int roomLengthMin,
        int roomWidthMin,
        Queue<Room> graph
    )
    {
        Divider line = GetLine(currNode.BotLeft, currNode.TopRight, roomWidthMin, roomLengthMin);

        Room node1,
            node2;
        if (line.Direction == Direction.Horizontal)
        {
            var topRight1 = new Vector2Int(currNode.TopRight.x, line.Coords.y);
            var botLeft2 = new Vector2Int(currNode.BotLeft.x, line.Coords.y);
            node1 = new Room(currNode.BotLeft, topRight1, currNode, currNode.Index + 1);
            node2 = new Room(botLeft2, currNode.TopRight, currNode, currNode.Index + 1);
        }
        else
        {
            var topRight1 = new Vector2Int(line.Coords.x, currNode.TopRight.y);
            var botLeft2 = new Vector2Int(line.Coords.x, currNode.BotLeft.y);
            node1 = new Room(currNode.BotLeft, topRight1, currNode, currNode.Index + 1);
            node2 = new Room(botLeft2, currNode.TopRight, currNode, currNode.Index + 1);
        }
        roomSpaces.Add(node1);
        graph.Enqueue(node1);
        roomSpaces.Add(node2);
        graph.Enqueue(node2);
    }

    // choose split dir based on size of the space
    private Divider GetLine(
        Vector2Int botLeft,
        Vector2Int topRight,
        int roomWidthMin,
        int roomLengthMin
    )
    {
        Direction dir;
        bool lengthStatus = (topRight.y - botLeft.y) >= 2 * roomLengthMin;
        bool widthStatus = (topRight.x - botLeft.x) >= 2 * roomWidthMin;

        // check length and width status
        // to determine which dir we can divide the space
        if (lengthStatus && widthStatus)
        {
            dir = (Direction)(Random.Range(0, 2));
        }
        else if (widthStatus)
        {
            dir = Direction.Vertical;
        }
        else
        {
            dir = Direction.Horizontal;
        }

        return new Divider(dir, GetCoords(dir, botLeft, topRight, roomWidthMin, roomLengthMin));
    }

    private Vector2Int GetCoords(
        Direction dir,
        Vector2Int botLeft,
        Vector2Int topRight,
        int roomWidthMin,
        int roomLengthMin
    )
    {
        Vector2Int pos = Vector2Int.zero;
        if (dir == Direction.Horizontal)
        {
            pos = new Vector2Int(
                0,
                Random.Range((botLeft.y + roomLengthMin), (topRight.y - roomLengthMin))
            );
        }
        else
        {
            pos = new Vector2Int(
                Random.Range((botLeft.x + roomWidthMin), (topRight.x - roomWidthMin)),
                0
            );
        }
        return pos;
    }
}
