using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int width = 500; // x
    public int length = 500; // z
    public int depth = 20; // y
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
        satillite_speed = 5f;
        ascending = true;

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GetTerrainData( terrain.terrainData );
    }

        // Update is called once per frame
    void Update() {

        if ( ascending ) {
            satillite_offset += Time.deltaTime * satillite_speed;
        } else {
            satillite_offset -= Time.deltaTime * satillite_speed;
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

        terrainData.heightmapResolution = width + 1;
        terrain_heights = GenerateHeights();

        terrainData.size = new Vector3( width, depth, length );
        terrainData.SetHeights(0, 0, terrain_heights);
        return terrainData;
    }

    float[,] GenerateHeights() {

        float[,] heights = new float[width, length];

        for ( int x = 0; x < width; ++x ) {

            for ( int z = 0; z < length; ++z ) {

                heights[x,z] = PerlinCalculator( x, z, perlin_octaves );
            }
        }
        return heights;
    }

    float PerlinCalculator( int x, int z, int octaves ) {

        float xCoord = (float)x / width * tempScale + satillite_offset;
        float zCoord = (float)z / width * tempScale + satillite_offset;

        return Mathf.PerlinNoise( xCoord, zCoord );
    }

    float[,] UpdateHeights( float[,] heights ) {

        for ( int x = 0; x < width; ++x ) {

            for ( int z = 0; z < length-1; ++z ) {

                heights[x, z] = heights[x,z+1];
            }
        }

        for ( int x = 0; x < width; ++x ) {

            heights[x, length-1] = PerlinCalculator(x, length-1, perlin_octaves );

        }

        return heights;
    }
}
