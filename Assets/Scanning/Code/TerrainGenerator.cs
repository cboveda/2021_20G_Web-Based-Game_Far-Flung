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
    public static float lacunarity = 2.2f;
    public static float persistance = 0.5f;

    public static float[,] GetTerrainHeights( int xOffset, int zOffset, int chunkDim ) {

        float[,] heights = new float[chunkDim, chunkDim];
        float normalMin = float.MaxValue;
        float normalMax = float.MinValue;

        for ( int z = 0; z < chunkDim; ++z ) {
            for ( int x = 0; x < chunkDim; ++x ) {

                heights[z, x] = PerlinCalculator( (z+zOffset), (x+xOffset), chunkDim );

                if (heights[z, x] > normalMax) {
                    normalMax = heights[z, x];
                } else if ( heights[z, x] < normalMin ) {
                    normalMin = heights[z, x];
                }
            }
        }
        return NormalizeHeights(heights, chunkDim, normalMin, normalMax);
    }

    private static float PerlinCalculator( int z, int x, int chunkDim ) {

        float amplitude = 1f;
        float frequency = 1f;
        float height = 0;

        for ( int o = 0; o < perlin_octaves; ++o ) {

            float zCoord = ((float)z / chunkDim) * frequency;
            float xCoord = ((float)x / chunkDim) * frequency;

            height += ( Mathf.PerlinNoise( zCoord, xCoord ) * amplitude );

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        return height;
    }

    private static float[,] NormalizeHeights( float[,] heights, int chunkDim, float normMin, float normMax ) {

        for ( int z = 0; z < chunkDim; ++z ) {
            for ( int x = 0; x < chunkDim; ++x ) {
                heights[z, x] = Mathf.InverseLerp(normMin, normMax, heights[z,x]);
            }
        }
        return heights;
    }
}
