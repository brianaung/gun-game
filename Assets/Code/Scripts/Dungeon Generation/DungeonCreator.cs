using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonCreator : MonoBehaviour
{
    // ??? is it a good feature to have ???
    // to generate the same map everytime
    // TODO: give player option to use either random or custom seed
    // should also be able to access current map seed
    // public int seed = 42;

    // NOTE: rooms smaller than 50/50 causes overlapping walls bug
    public Material material;
    public int dungeonWidth = 180, dungeonLength = 120;
    public int roomWidthMin = 50, roomLengthMin = 50;
    public int maxIterations = 4;
    public int corridorWidth = 8;
    [Range(0.0f, 0.3f)] public float botCornerMod = 0.2f;
    [Range(0.7f, 1.0f)] public float topCornerMod = 0.8f;
    [Range (0, 2)] public int offset = 1;

    // for constructing walls
    public GameObject wallPrefab;
    List<Vector3Int> allVertDoorPos;
    List<Vector3Int> allHoriDoorPos;
    List<Vector3Int> allVertWallPos;
    List<Vector3Int> allHoriWallPos;

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    public void CreateDungeon()
    {
        DestroyAllChildren();

        DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateDungeon(
                                        maxIterations, roomWidthMin, roomLengthMin, 
                                        botCornerMod, topCornerMod, offset, corridorWidth);

        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
        allVertDoorPos = new List<Vector3Int>();
        allHoriDoorPos = new List<Vector3Int>();
        allVertWallPos = new List<Vector3Int>();
        allHoriWallPos = new List<Vector3Int>();

        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }

        CreateWalls(wallParent);

    }

    private void CreateWalls(GameObject wallParent)
    {
        foreach(var wallPos in allHoriWallPos)
        {
            CreateAWall(wallParent, wallPos, wallPrefab, false);
        }
        foreach(var wallPos in allVertWallPos)
        {
            CreateAWall(wallParent, wallPos, wallPrefab, true);
        }
    }

    private void CreateAWall
        (GameObject wallParent, Vector3Int wallPos, GameObject wallPrefab, bool isVert)
    {
        if (isVert)
        {
            Instantiate(wallPrefab, wallPos, Quaternion.Euler(0, 90, 0), wallParent.transform);
        } else
        {
            Instantiate(wallPrefab, wallPos, Quaternion.identity, wallParent.transform);
        }
    }

    // Code Monkey Tutorial: How to create mesh from code
    // https://www.youtube.com/watch?v=gmuHI_wsOgI
    // Week 2 workshop
    // https://github.com/COMP30019/Workshop-2/blob/main/Assets/GenerateCube.cs
    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        var mesh = new Mesh();

        var topLeftV =
            new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        var botLeftV =
            new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        var botRightV =
            new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        var topRightV =
            new Vector3(topRightCorner.x, 0, topRightCorner.y);

        /*
        mesh.SetVertices(new[]
        {
            // topLeftV -> botLeftV -> botRightV
            topLeftV,
            botLeftV,
            botRightV,

            // topLeftV -> botRightV -> topRightV
            topLeftV,
            botRightV,
            topRightV
        });
        */

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            botLeftV,
            botRightV
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3
        };

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        /*
        var indices =
            Enumerable.Range(0, mesh.vertices.Length).Reverse().ToArray();

        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        */

        GameObject floor = new GameObject("Mesh Floor", typeof(MeshFilter), typeof(MeshRenderer));
        floor.transform.position = Vector3.zero;
        floor.transform.localScale = Vector3.one;
        floor.GetComponent<MeshFilter>().mesh = mesh;
        floor.GetComponent<MeshRenderer>().material = material;
        floor.AddComponent<MeshCollider>();
        floor.transform.parent = transform;

        // horizontal walls and doors
        for (int i = (int) botLeftV.x; i < (int) botRightV.x; i++)
        {
            var wallPos = new Vector3(i, 0, botLeftV.z);
            AddWallPos(wallPos, allHoriWallPos, allHoriDoorPos);
        }
        for (int i = (int) topLeftV.x; i < (int) topRightV.x; i++)
        {
            var wallPos = new Vector3(i, 0, topRightV.z);
            AddWallPos(wallPos, allHoriWallPos, allHoriDoorPos);
        }
        // vertical walls and doors
        for (int j = (int) botLeftV.z; j < (int) topLeftV.z; j++)
        {
            var wallPos = new Vector3(botLeftV.x, 0, j);
            AddWallPos(wallPos, allVertWallPos, allVertDoorPos);
        }
        for (int j = (int) botRightV.z; j < (int) topRightV.z; j++)
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
        } else
        {
            wallList.Add(point);
        }
    }

    private void DestroyAllChildren()
    {
        while(transform.childCount != 0)
        {
            foreach(Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }
}
