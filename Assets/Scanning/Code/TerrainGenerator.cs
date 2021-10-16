using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    A utility class for createing height maps using perlin noise which emulate
    psyche 16's surface.
*/
public static class TerrainGenerator {

    public static int perlin_octaves = 5;
    public static float lacunarity = 2f;
    public static float persistance = 0.4f;

    public static float[,] GetTerrainHeights( Vector3 vBase, int meshDim, int terrainSeed ) {

        float[,] heights = new float[meshDim, meshDim];
        float normalMin = float.MaxValue;
        float normalMax = float.MinValue;

        Vector2[] offsets = CreateOffsets( vBase, terrainSeed );
        
        for ( int x = 0; x < meshDim; ++x ) {
            for ( int z = 0; z < meshDim; ++z ) {

                heights[x, z] = PerlinCalculator( x, z, offsets, meshDim );

                if (heights[x, z] > normalMax) {
                    normalMax = heights[x, z];
                } else if ( heights[x, z] < normalMin ) {
                    normalMin = heights[x, z];
                }
            }
        }

        float absMax = 0f;
        float amplitude = 1f;

        for ( int o = 0; o < perlin_octaves; ++o ) {

            absMax += amplitude;
            amplitude *= persistance;
        }

        return NormalizeHeights( heights, meshDim, absMax);
    }

    static float PerlinCalculator( float x, float z, Vector2[] offsets, int meshDim ) {

        float amplitude = 1f;
        float frequency = 1f;
        float height = 0;

        for ( int o = 0; o < perlin_octaves; ++o ) {

            float xCoord = ( ( x + offsets[o].x ) / meshDim ) * frequency;
            float zCoord = ( ( z + offsets[o].y ) / meshDim ) * frequency;

            height += Mathf.PerlinNoise( xCoord, zCoord ) * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        return height;
    }

    static Vector2[] CreateOffsets( Vector3 vBase, int terrainSeed ) {

        System.Random rand = new System.Random( terrainSeed );
        
        Vector2[] offsets = new Vector2 [perlin_octaves ];

        for ( int o = 0; o < perlin_octaves; ++o ) {

            float offX = rand.Next( -99999, 99999 ) + vBase.x;
            float offZ = rand.Next( -99999, 99999 ) + vBase.z;
            offsets[o] = new Vector2(offX, offZ);
        }
        return offsets;
    }

    static float[,] NormalizeHeights( float[,] heights, int meshDim, float max ) {

        for ( int x = 0; x < meshDim; ++x )
        {
            for ( int z = 0; z < meshDim; ++z )
            {
				heights [x, z] = Mathf.Clamp( (heights[x, z] / max), 0, 1 );
            }
        }
        return heights;
    }
}
