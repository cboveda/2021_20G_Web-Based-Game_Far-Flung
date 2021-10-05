using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    A utility class for createing height maps using perlin noise which emulate
    psyche 16's surface.
*/
public static class TerrainGenerator
{
    public static int perlin_octaves = 5;
    public static float lacunarity = 2f;
    public static float persistance = 0.4f;

    public static float[,] GetTerrainHeights( Vector3 vBase, int meshDim ) {

        float[,] heights = new float[meshDim, meshDim];
        float normalMin = float.MaxValue;
        float normalMax = float.MinValue;
        
        for ( int x = 0; x < meshDim; ++x ) {
            for ( int z = 0; z < meshDim; ++z ) {

                heights[x, z] = PerlinCalculator( ( vBase.x + x ), ( vBase.z + z ), meshDim );

                if (heights[x, z] > normalMax) {
                    normalMax = heights[x, z];
                } else if ( heights[x, z] < normalMin ) {
                    normalMin = heights[x, z];
                }
            }
        }

        float normMaxP = 0f;
        float amplitude = 1f;

        for ( int o = 0; o < perlin_octaves; ++o ) {

            normMaxP += amplitude;
            amplitude *= persistance;
        }
        return heights;
    }

    static float PerlinCalculator( float x, float z, int meshDim ) {

        float amplitude = 1f;
        float frequency = 1f;
        float height = 0;

        for ( int o = 0; o < perlin_octaves; ++o ) {

            float xCoord = ((float)x / meshDim) * frequency;
            float zCoord = ((float)z / meshDim) * frequency;

            height += Mathf.PerlinNoise( xCoord, zCoord ) * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        return height;
    }
}
