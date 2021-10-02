using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    void Start() {

        float[,] terrain = TerrainGenerator.GetTerrainHeights(0, 0, 241);
        meshFilter.sharedMesh = MeshGenerator.GenerateTerrainMesh(terrain, 50).CreateMesh();

    }
}
