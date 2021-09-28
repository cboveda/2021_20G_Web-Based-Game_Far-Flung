using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int width = 500; // x
    public int length = 500; // z
    public int depth = 20; // y

    public float lacunarity;
    public float persistance;

    public float tempScale = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GetTerrainData( terrain.terrainData );
    }

    TerrainData GetTerrainData( TerrainData terrainData ) {

        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3( width, depth, length );
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() {

        float[,] heights = new float[width, length];

        for ( int x = 0; x < width; ++x ) {

            for ( int z = 0; z < length; ++z ) {

                heights[x,z] = PerlinCalculator(x, z);
            }
        }
        return heights;
    }

    float PerlinCalculator( int x, int z ) {

        float xCoord = (float)x / width * tempScale;
        float zCoord = (float)z / width * tempScale;

        return Mathf.PerlinNoise( xCoord, zCoord );
    }

    // Update is called once per frame
    void Update() {}
}
