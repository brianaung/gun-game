using System;
using System.Collections.Generic;

/* return all nodes that will be the final rooms (no more children node) */
public static class StructureHelper
{
    public static List<Node> TraverseGraphs(RoomNode parentNode)
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
}