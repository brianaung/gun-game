using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        // implement Binary Space Partitioner algo
        BinarySpacePartitioner bsp = 
            new BinarySpacePartitioner(this.dungeonWidth, this.dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);

        // get spaces to generate room at
        List<Node> roomSpaces = StructureHelper.TraverseGraphs(bsp.RootNode);

        // generate rooms in room space nodes
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRooms(roomSpaces);

        return new List<Node>(roomList);
    }
}
