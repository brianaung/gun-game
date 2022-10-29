using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class MapBuilder : MonoBehaviour
{
    // NOTE: rooms smaller than 50/50 causes overlapping walls bug
    public Material material;
    List<Room> allNodes = new List<Room>();
    public int width = 180,
        length = 120;
    public int roomWidthMin = 50,
        roomLengthMin = 50;
    public int maxIterations = 4;
    public int corridorWidth = 8;

    [Range(0.0f, 0.3f)]
    public float botCornerMod = 0.2f;

    [Range(0.7f, 1.0f)]
    public float topCornerMod = 0.8f;

    [Range(0, 2)]
    public int offset = 1;

    // for constructing walls
    public GameObject wallPrefab;
    List<Vector3Int> allVertDoorPos;
    List<Vector3Int> allHoriDoorPos;
    List<Vector3Int> allVertWallPos;
    List<Vector3Int> allHoriWallPos;

    // player object
    public GameObject player;
    public GameObject Enemy;
    public int enemyNumber;
    public GameObject[] envProps;

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    // create/render a dungeon that contains rooms and corridors and props
    public void CreateDungeon()
    {
        DestroyAllChildren();

        //DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = CalculateDungeon();

        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
        allVertDoorPos = new List<Vector3Int>();
        allHoriDoorPos = new List<Vector3Int>();
        allVertWallPos = new List<Vector3Int>();
        allHoriWallPos = new List<Vector3Int>();

        GameObject otherPropsParent = new GameObject("PropsParent");
        otherPropsParent.transform.parent = transform;

        // render map (floor and walls)
        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BotLeft, listOfRooms[i].TopRight);
            if (!listOfRooms[i].isCorridor)
            {
                // randomly place props
                foreach (var prop in envProps)
                {
                    PlacePrefab(prop, otherPropsParent, RandomPosInRoom(listOfRooms[i]));
                    PlacePrefab(prop, otherPropsParent, RandomPosInRoom(listOfRooms[i]));
                }
            }
        }
        CreateWalls(wallParent);

        // Spawn the player in the middle of first room
        Vector2Int roomCenter = (listOfRooms[0].BotLeft + listOfRooms[0].TopRight) / 2;
        Vector3Int finalPos = new Vector3Int(roomCenter.x, 0, roomCenter.y);
        player.transform.position = finalPos;
    }

    // return a list of spaces (rooms and corridors)
    public List<Node> CalculateDungeon()
    {
        // implement Binary Space Partitioner algo
        BSP bsp = new BSP(this.width, this.length);
        this.allNodes = bsp.CreateRoomSpaces(
            this.maxIterations,
            this.roomWidthMin,
            this.roomLengthMin
        );

        // get spaces to generate room at
        List<Node> roomSpaces = Helper.TraverseGraphs(bsp.Root);

        // generate rooms in room space nodes
        var roomList = CreateRoom(roomSpaces);

        // generate corridors
        var corridors = CreateCorridor();

        return new List<Node>(roomList).Concat(corridors).ToList();
    }

    // return a list of rooms
    public List<Room> CreateRoom(List<Node> roomSpaces)
    {
        List<Room> rooms = new List<Room>();
        foreach (var roomspace in roomSpaces)
        {
            Vector2Int newBotLeft = Helper.GenerateBotLeft(
                roomspace.BotLeft,
                roomspace.TopRight,
                this.botCornerMod,
                this.offset
            );

            Vector2Int newBotRight = Helper.GenerateTopRight(
                roomspace.BotLeft,
                roomspace.TopRight,
                this.topCornerMod,
                this.offset
            );

            roomspace.BotLeft = newBotLeft;
            roomspace.TopRight = newBotRight;
            roomspace.BotRight = new Vector2Int(newBotRight.x, newBotLeft.y);
            roomspace.TopLeft = new Vector2Int(newBotLeft.x, newBotRight.y);

            rooms.Add((Room)roomspace);
        }
        return rooms;
    }

    // return a list of corridors
    public List<Node> CreateCorridor()
    {
        List<Node> corridors = new List<Node>();
        Queue<Room> structsToCheck = new Queue<Room>(
            this.allNodes.OrderByDescending(node => node.Index).ToList()
        );

        while (structsToCheck.Count > 0)
        {
            var node = structsToCheck.Dequeue();

            if (node.ChildrenList.Count == 0)
            {
                continue;
            }

            Corridor corridor = new Corridor(
                node.ChildrenList[0],
                node.ChildrenList[1],
                this.corridorWidth
            );
            corridor.isCorridor = true;
            corridors.Add(corridor);
        }
        return corridors;
    }

    // return a random position inside each room
    private Vector2Int RandomPosInRoom(Node room)
    {
        var center = (room.BotLeft + room.TopRight) / 2;
        var x = Random.Range(room.BotLeft.x + 2, room.BotRight.x - 2);
        var z = Random.Range(room.BotLeft.y + 2, room.TopLeft.y - 2);
        var pos = new Vector2Int(x, z);
        return pos;
    }

    // place prefabs onto the specified location
    private void PlacePrefab(GameObject prefabAsset, GameObject propsParent, Vector2Int twoDPos)
    {
        // to render prefab starting from bottom (not their pivot)
        var prefabOffset = 0;

        var finalPos = new Vector3Int(twoDPos.x, prefabOffset, twoDPos.y);

        Instantiate(prefabAsset, finalPos, Quaternion.identity, propsParent.transform);
    }

    private void CreateWalls(GameObject wallParent)
    {
        foreach (var wallPos in allHoriWallPos)
        {
            CreateAWall(wallParent, wallPos, wallPrefab, false);
        }
        foreach (var wallPos in allVertWallPos)
        {
            CreateAWall(wallParent, wallPos, wallPrefab, true);
        }
    }

    private void CreateAWall(
        GameObject wallParent,
        Vector3Int wallPos,
        GameObject wallPrefab,
        bool isVert
    )
    {
        var wallOffset = (int)wallPrefab.GetComponent<Renderer>().bounds.size.y / 2;
        wallPos = new Vector3Int(wallPos.x, wallPos.y + wallOffset, wallPos.z);

        if (isVert)
        {
            Instantiate(wallPrefab, wallPos, Quaternion.Euler(0, 90, 0), wallParent.transform);
        }
        else
        {
            Instantiate(wallPrefab, wallPos, Quaternion.identity, wallParent.transform);
        }
    }

    // Code Monkey Tutorial: How to create mesh from code
    // https://www.youtube.com/watch?v=gmuHI_wsOgI
    // Week 2 workshop
    // https://github.com/COMP30019/Workshop-2/blob/main/Assets/GenerateCube.cs
    private void CreateMesh(Vector2 botLeft, Vector2 topRight)
    {
        var mesh = new Mesh();

        var topLeftV = new Vector3(botLeft.x, 0, topRight.y);
        var botLeftV = new Vector3(botLeft.x, 0, botLeft.y);
        var botRightV = new Vector3(topRight.x, 0, botLeft.y);
        var topRightV = new Vector3(topRight.x, 0, topRight.y);

        Vector3[] vertices = new Vector3[] { topLeftV, topRightV, botLeftV, botRightV };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };

        // render mesh plane
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GameObject floor = new GameObject("Mesh Floor", typeof(MeshFilter), typeof(MeshRenderer));
        floor.GetComponent<MeshFilter>().mesh = mesh;
        floor.GetComponent<MeshRenderer>().material = material;
        floor.AddComponent<MeshCollider>();
        floor.AddComponent<Rigidbody>();
        floor.GetComponent<Rigidbody>().isKinematic = true;
        floor.GetComponent<Rigidbody>().useGravity = false;

        floor.transform.parent = transform;

        // Add Enemy Spawner to each floor
        floor.AddComponent<EnemySpawner>();
        floor.GetComponent<EnemySpawner>().theEnemy = this.Enemy;
        floor.GetComponent<EnemySpawner>().enemyNumber = this.enemyNumber;
        floor.GetComponent<EnemySpawner>().Floor = vertices;

        // horizontal walls and doors
        for (int i = (int)botLeftV.x; i < (int)botRightV.x; i++)
        {
            var wallPos = new Vector3(i, 0, botLeftV.z);
            AddWallPos(wallPos, allHoriWallPos, allHoriDoorPos);
        }
        for (int i = (int)topLeftV.x; i < (int)topRightV.x; i++)
        {
            var wallPos = new Vector3(i, 0, topRightV.z);
            AddWallPos(wallPos, allHoriWallPos, allHoriDoorPos);
        }
        // vertical walls and doors
        for (int j = (int)botLeftV.z; j < (int)topLeftV.z; j++)
        {
            var wallPos = new Vector3(botLeftV.x, 0, j);
            AddWallPos(wallPos, allVertWallPos, allVertDoorPos);
        }
        for (int j = (int)botRightV.z; j < (int)topRightV.z; j++)
        {
            var wallPos = new Vector3(botRightV.x, 0, j);
            AddWallPos(wallPos, allVertWallPos, allVertDoorPos);
        }
    }

    private void AddWallPos(Vector3 wallPos, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPos);

        /* rooms are generated first
           then corridors are added
           that means where the corridor and room intercept,
           there should alrdy be a wall in wall list
           so that point can become a door */
        if (wallList.Contains(point))
        {
            // can use this to implement door instead of empty entrance here
            doorList.Add(point);

            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }

    private void DestroyAllChildren()
    {
        while (transform.childCount != 0)
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }
}
