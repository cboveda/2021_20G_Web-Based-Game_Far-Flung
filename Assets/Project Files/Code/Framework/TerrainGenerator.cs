using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int xDim = 500; // x
    public int zDim = 500; // z
    public int yDim = 100; // y
    public int perlin_octaves = 4;

    private float normalMin;
    private float normalMax;

    public float lacunarity;
    public float persistance;

    private int satillite_offset;
    private bool ascending;

    public float zScale;
    public float xScale;

    private float[,] terrain_heights;

    // Start is called before the first frame update
    void Start()
    {
        satillite_offset = 0;
        ascending = true;

        zScale = zDim;
        xScale = xDim;

        lacunarity = 2f;
        persistance = 0.5f;

        normalMax = float.MinValue;
        normalMin = float.MaxValue;

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
        terrain_heights = NormalizeHeights( terrain_heights );

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

        float amplitude = 1f;
        float frequency = 1f;
        float height = 0;


        for ( int o = 0; o < octaves; ++o ) {

            float zCoord = ((float)z / zScale) * frequency;
            float xCoord = ((float)x / xScale) * frequency;

            height += ( Mathf.PerlinNoise( zCoord, xCoord ) * amplitude );

            amplitude *= persistance;
            frequency *= lacunarity;
        }

        if (height > normalMax) {
            normalMax = height;
        } else if ( height < normalMin ) {
            normalMin = height;
        }

        return height;
    }

    float[,] NormalizeHeights( float[,] heights ) {

        for ( int z = 0; z < zDim; ++z ) {

            for ( int x = 0; x < xDim; ++x ) {

                heights[z, x] = Mathf.InverseLerp(normalMin, normalMax, heights[z,x]);
            }
        }

        return heights;
    }
       

    float[,] UpdateHeights( float[,] heights ) {

        for ( int z = 0; z < zDim-1; ++z ) {

            for ( int x = 0; x < xDim; ++x ) {

                heights[z, x] = heights[z+1,x];
            }
        }

        for ( int x = 0; x < xDim; ++x ) {

            float hTemp = PerlinCalculator( (zDim-1 + satillite_offset), x, perlin_octaves );
            heights[zDim-1, x] = Mathf.InverseLerp(normalMin, normalMax, hTemp);
             
        }

        return heights;
    }
}
