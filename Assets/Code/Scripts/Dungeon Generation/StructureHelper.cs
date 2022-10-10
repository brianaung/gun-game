using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* return all nodes that will be the final rooms (no more children node) 
   aka. all the leaf nodes
 */
public static class StructureHelper
{
    public static List<Node> TraverseGraphs(Node parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();

        // only one node, no need to traverse and look for children
        if (parentNode.ChildrenNodeList.Count == 0)
        {
            return new List<Node>() { parentNode };
        }

        // put all children in graphs to check
        foreach(var child in parentNode.ChildrenNodeList)
        {
            nodesToCheck.Enqueue(child);
        }

        // extract all childrenless nodes
        while (nodesToCheck.Count > 0)
        {
            var currNode = nodesToCheck.Dequeue();
            if (currNode.ChildrenNodeList.Count == 0)
            {
                listToReturn.Add(currNode);
            } else
            {
                foreach(var child in currNode.ChildrenNodeList)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }

        return listToReturn;
    }

    // NOTE:
    // pointModifier - makes sure the rooms are of different sizes
    // offset - controls space between each room
    // current best settings:
    // 0.4 for BotLeft and 0.6 for TopRight with 1 offset
    // width: 450, height: 250, min room width/length: 70, max iteration: 10
    public static Vector2Int GenerateBottomLeftCornerBetween(
        Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        return new Vector2Int(
            Random.Range(minX, (int)(minX + (maxX - minX) * pointModifier)),
            Random.Range(minY, (int)(minY + (maxY - minY) * pointModifier)));
    }

    public static Vector2Int GenerateTopRightCornerBetween(
        Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        return new Vector2Int(
            Random.Range((int)(minX + (maxX - minX) * pointModifier), maxX),
            Random.Range((int)(minY + (maxY - minY) * pointModifier), maxY)
            );
    }

    public static Vector2Int GetMidPoint(Vector2Int v1, Vector2Int v2)
    {
        Vector2 mid = (v1 + v2) / 2;

        return new Vector2Int((int)mid.x, (int)mid.y);
    }
}