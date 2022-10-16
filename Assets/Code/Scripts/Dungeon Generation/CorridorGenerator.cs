using System;
using System.Collections.Generic;
using System.Linq;

public class CorridorGenerator
{
    public List<Node> CreateCorridor(List<RoomNode> allNodes, int corridorWidth)
    {
        List<Node> corridorList = new List<Node>();
        Queue<RoomNode> structsToCheck = new Queue<RoomNode>(
            allNodes.OrderByDescending(node => node.TreeLayerIndex).ToList()
        );

        while (structsToCheck.Count > 0)
        {
            var node = structsToCheck.Dequeue();

            if (node.ChildrenNodeList.Count == 0) { continue; }

            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth);
            corridor.isCorridor = true;
            corridorList.Add(corridor);
        }
        return corridorList;
    }
}