using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public Material material;
    public int dungeonWidth = 450, dungeonLength = 250;
    public int roomWidthMin = 70, roomLengthMin = 70;
    public int maxIterations = 10;
    public int corridorWidth = 5;
    [Range(0.0f, 0.3f)] public float botCornerMod = 0.3f;
    [Range(0.7f, 1.0f)] public float topCornerMod = 0.7f;
    [Range (0, 2)] public int offset = 1;

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    private void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateRooms(
                                        maxIterations, roomWidthMin, roomLengthMin, 
                                        botCornerMod, topCornerMod, offset);
        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }
    }

    // Code Monkey Tutorial: How to create mesh from code
    // https://www.youtube.com/watch?v=gmuHI_wsOgI
    // Week 2 workshop
    // https://github.com/COMP30019/Workshop-2/blob/main/Assets/GenerateCube.cs
    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Mesh mesh = new Mesh();

        Vector3 bottomLeftV = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightV = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftV = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightV = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV
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

        GameObject floor = new GameObject("Mesh Floor", typeof(MeshFilter), typeof(MeshRenderer));
        floor.transform.position = Vector3.zero;
        floor.transform.localScale = Vector3.one;
        floor.GetComponent<MeshFilter>().mesh = mesh;
        floor.GetComponent<MeshRenderer>().material = material;
    }
}
