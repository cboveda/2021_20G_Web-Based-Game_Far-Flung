using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public int chunkDim = 241;
    public Color[] terrainGradient = new Color[2];

    void Start() {

        float[,] terrain = TerrainGenerator.GetTerrainHeights(0, 0, chunkDim);
        meshFilter.sharedMesh = MeshGenerator.GenerateTerrainMesh(terrain, 50).CreateMesh();

        // fetch a texture for mesh
        Texture2D texture = TerrainTextureGenerator.CreateTexture(terrainGradient, terrain, chunkDim, chunkDim);
        meshRenderer.sharedMaterial.mainTexture = texture;
        //meshRenderer.transform.localScale = new Vector3(chunkDim, 1, chunkDim); // might interfere with height collisions, can restrict

    }
}