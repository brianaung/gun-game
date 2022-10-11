using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNode rootNode;

    public RoomNode RootNode { get => rootNode; }

    public BinarySpacePartitioner(int dungeonWidth, int dungeonLength)
    {
        // create root node from 0,0 to max dungeon size
        this.rootNode = new RoomNode(new Vector2Int(0, 0),
                                     new Vector2Int(dungeonWidth, dungeonLength),
                                     null,
                                     0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);
        int i = 0;
        while (i < maxIterations && graph.Count > 0)
        {
            i++;
            RoomNode currNode = graph.Dequeue();
            if (currNode.Width >= roomWidthMin*2 || currNode.Length >= roomLengthMin*2)
            {
                // split the room if size bigger than specified min values
                SplitSpace(currNode, listToReturn, roomLengthMin, roomWidthMin, graph);
            }
        }
        return listToReturn;
    }

    private void SplitSpace(RoomNode currNode, List<RoomNode> listToReturn, int roomLengthMin, int roomWidthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(
            currNode.BottomLeftAreaCorner,
            currNode.TopRightAreaCorner,
            roomWidthMin,
            roomLengthMin);

        RoomNode node1, node2;
        if (line.Orientation == Orientation.Horizontal)
        {
            node1 = new RoomNode(
                currNode.BottomLeftAreaCorner,
                new Vector2Int(currNode.TopRightAreaCorner.x, line.Coords.y),
                currNode,
                currNode.TreeLayerIndex + 1);
            node2 = new RoomNode(
                new Vector2Int(currNode.BottomLeftAreaCorner.x, line.Coords.y),
                currNode.TopRightAreaCorner,
                currNode,
                currNode.TreeLayerIndex + 1);
        } else
        {
            node1 = new RoomNode(
                currNode.BottomLeftAreaCorner,
                new Vector2Int(line.Coords.x, currNode.TopRightAreaCorner.y),
                currNode,
                currNode.TreeLayerIndex + 1);
            node2 = new RoomNode(
                new Vector2Int(line.Coords.x, currNode.BottomLeftAreaCorner.y),
                currNode.TopRightAreaCorner,
                currNode,
                currNode.TreeLayerIndex + 1);
        }
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    private void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    // choose split orientation based on size of the space
    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner,
                                      Vector2Int topRightAreaCorner,
                                      int roomWidthMin,
                                      int roomLengthMin)
    {
        Orientation orientation;
        bool lengthStatus = 
            (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin;
        bool widthStatus =
            (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomWidthMin;

        // check length and width status
        // to determine which dir we can divide the space
        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        } else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        } else
        {
            orientation = Orientation.Horizontal;
        }

        return new Line(orientation, GetCoordsForOrientation(
            orientation,
            bottomLeftAreaCorner,
            topRightAreaCorner,
            roomWidthMin,
            roomLengthMin));
    }

    private Vector2Int GetCoordsForOrientation(Orientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coords = Vector2Int.zero;
        if (orientation == Orientation.Horizontal)
        {
            coords = new Vector2Int(
                0, 
                Random.Range(
                (bottomLeftAreaCorner.y + roomLengthMin),
                (topRightAreaCorner.y - roomLengthMin)
                ));
        } else
        {
            coords = new Vector2Int(
                Random.Range(
                (bottomLeftAreaCorner.x + roomWidthMin),
                (topRightAreaCorner.x - roomWidthMin)
                ),
                0);
        }

        return coords;
    }

}