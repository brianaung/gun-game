using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/* return all nodes that will be the final rooms (no more children node)
   aka. all the leaf nodes
 */
public static class Helper
{
    public static List<Node> TraverseGraphs(Node parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();

        // only one node, no need to traverse and look for children
        if (parentNode.ChildrenList.Count == 0)
        {
            return new List<Node>() { parentNode };
        }

        // put all children in graphs to check
        foreach (var child in parentNode.ChildrenList)
        {
            nodesToCheck.Enqueue(child);
        }

        // extract all childrenless nodes
        while (nodesToCheck.Count > 0)
        {
            var currNode = nodesToCheck.Dequeue();
            if (currNode.ChildrenList.Count == 0)
            {
                listToReturn.Add(currNode);
            }
            else
            {
                foreach (var child in currNode.ChildrenList)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }

        return listToReturn;
    }

    public static Vector2Int GenerateBotLeft(
        Vector2Int boundaryLeftPoint,
        Vector2Int boundaryRightPoint,
        float pointModifier,
        int offset
    )
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        return new Vector2Int(
            Random.Range(minX, (int)(minX + (maxX - minX) * pointModifier)),
            Random.Range(minY, (int)(minY + (maxY - minY) * pointModifier))
        );
    }

    public static Vector2Int GenerateTopRight(
        Vector2Int boundaryLeftPoint,
        Vector2Int boundaryRightPoint,
        float pointModifier,
        int offset
    )
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
