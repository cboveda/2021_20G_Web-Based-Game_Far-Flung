using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int xDim = 500; // x
    public int zDim = 500; // z
    public int yDim = 20; // y
    public int perlin_octaves = 3;

    public float lacunarity;
    public float persistance;

    private float satillite_offset, satillite_speed;
    private bool ascending;

    public float tempScale = 20f;

    private float[,] terrain_heights;

    // Start is called before the first frame update
    void Start()
    {
        satillite_offset = 0;
        satillite_speed = 2f;
        ascending = true;

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GetTerrainData( terrain.terrainData );
    }

        // Update is called once per frame
    void Update() {

        if ( ascending ) {
            satillite_offset += 1;
        } else {
            satillite_offset -= 1;
        }

        if ( satillite_offset > 99999 ) {
            ascending = false;
        }
        if ( satillite_offset <= 0 ) {
            ascending = true;
        }

        terrain_heights = UpdateHeights( terrain_heights );

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData.SetHeights(0,0, terrain_heights);
    }

    TerrainData GetTerrainData( TerrainData terrainData ) {

        terrainData.heightmapResolution = xDim + 1;
        terrain_heights = GenerateHeights();

        terrainData.size = new Vector3( xDim, yDim, zDim );
        terrainData.SetHeights(0, 0, terrain_heights);
        return terrainData;
    }

    float[,] GenerateHeights() {

        float[,] heights = new float[zDim, xDim];

        for ( int z = 0; z < zDim; ++z ) {

            for ( int x = 0; x < xDim; ++x ) {

                heights[z, x] = PerlinCalculator( z, x, perlin_octaves );
            }
        }
        return heights;
    }

    float PerlinCalculator( int z, int x, int octaves ) {

        float zCoord = ((float)(z+satillite_offset) / zDim) * tempScale;
        float xCoord = ((float)x / xDim) * tempScale;

        return Mathf.PerlinNoise( zCoord, xCoord );
    }

    float[,] UpdateHeights( float[,] heights ) {

        for ( int z = 0; z < zDim-1; ++z ) {

            for ( int x = 0; x < xDim; ++x ) {

                heights[z, x] = heights[z+1,x];
            }
        }

        for ( int x = 0; x < xDim; ++x ) {
            heights[zDim-1, x] = PerlinCalculator( zDim-1, x, perlin_octaves );
        }

        return heights;
    }
}
