using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator
{
    List<RoomNode> allNodes = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }

    // return rooms and corridors
    public List<Node> CalculateDungeon(
        int maxIterations, int roomWidthMin, int roomLengthMin, 
        float botCornerMod, float topCornerMod, int offset, int corridorWidth)
    {
        // implement Binary Space Partitioner algo
        BinarySpacePartitioner bsp = 
            new BinarySpacePartitioner(this.dungeonWidth, this.dungeonLength);
        allNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);

        // get spaces to generate room at
        List<Node> roomSpaces = StructureHelper.TraverseGraphs(bsp.RootNode);

        // generate rooms in room space nodes
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRooms(roomSpaces, botCornerMod, topCornerMod, offset);

        CorridorGenerator corridorGenerator = new CorridorGenerator();
        var corridorList = corridorGenerator.CreateCorridor(allNodes, corridorWidth);

        return new List<Node>(roomList).Concat(corridorList).ToList();
    }
}
