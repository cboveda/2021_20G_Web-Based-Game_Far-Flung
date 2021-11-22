using NUnit.Framework;
using UnityEngine;

public class ScanningTest {

    [Test]
    public void Test_TerrainGeneratorGetTerrainHeights() {

        float[,] testSpec = {
            { 0.183221f, 0.2240982f, 0.183221f, 0.1014668f },
            { 0.1423439f, 0.1219054f, 0.1423439f, 0.1627825f },
            { 0.183221f, 0.1014668f, 0.183221f, 0.2649753f },
            { 0.1423439f, 0.1014668f, 0.2240982f, 0.3058524f }
        };

        Vector3 baseVec = new Vector3( 0, 0, 0 );
        int meshDim = 4;
        int terrainSeed = 0;
        AnimationCurve baseCurve = new AnimationCurve();

        float[,] result = TerrainGenerator.GetTerrainHeights( baseVec, meshDim, terrainSeed, baseCurve );

        for ( int i = 0; i < meshDim; ++i ) {

            for ( int j = 0; j < meshDim; ++j ) {

                Assert.AreEqual( result[i,j], testSpec[i,j], 0.0001f );
            }
        }
    }

    [Test]
    public void Test_MeshGeneratorGenerateTerrainMesh() {

        Vector3 real_coord = new Vector3( 0, 0, 0 ); 
        int terrainSeed = 0; 
        int meshDim = 4; 
        float terrainScale = 1f; 
        AnimationCurve basePerlinCurve = new AnimationCurve();

        MeshData data = MeshGenerator.GenerateTerrainMesh( real_coord, terrainSeed, meshDim, terrainScale, basePerlinCurve );

        Assert.AreEqual( data.superHeights.GetLength(0), (meshDim + 2) );
        Assert.AreEqual( data.superHeights.GetLength(1), (meshDim + 2) );
        Assert.AreEqual( data.normalizedHeightMap.GetLength(0), meshDim );
        Assert.AreEqual( data.normalizedHeightMap.GetLength(1), meshDim );

        int c = 0;
        int tri = 0;

        for ( int z = 0; z < meshDim; ++z ) {
            for ( int x = 0; x < meshDim; ++x ) {

                Assert.AreEqual( data.vertices[c], new Vector3( x, ( terrainScale * data.normalizedHeightMap[x, z] ), z ) );

                if ( x < (meshDim-1) && z < (meshDim-1) ) 
                {
                    Assert.AreEqual( data.triangles[ tri ]    , c  );
                    Assert.AreEqual( data.triangles[ tri + 1 ], c + meshDim );
                    Assert.AreEqual( data.triangles[ tri + 2 ], c + 1);
                    Assert.AreEqual( data.triangles[ tri + 3 ], c + 1);
                    Assert.AreEqual( data.triangles[ tri + 4 ], c + meshDim );
                    Assert.AreEqual( data.triangles[ tri + 5 ], c + meshDim + 1);
                    tri += 6;
                }
                Assert.AreEqual( data.uvs[c], new Vector2( ( (float)x / ( meshDim - 1 ) ), ( (float)z / ( meshDim - 1 ) ) ) );
                c++;
            }
        }
    }

    [Test]
    public void Test_TextureGeneratorCreateColorMap() {

        Gradient gradient = new Gradient();
        float[,] heights = { 
            {0.0f, 0.5f, 1.0f},
            {0.1f, 0.3f, 0.9f},
            {0.5f, 0.2f, 0.7f}
        };
        int tileDim = 3;

        Color[] colors = TextureGenerator.CreateColorMap( gradient, heights, tileDim );

        for ( int i = 0; i < 3; ++i ) {
            for ( int j = 0; j < 3; ++j ) {
                Assert.AreEqual( colors[ (i * tileDim) + j ], gradient.Evaluate( heights[i, j] ) );
            }
        }
    }

    [Test]
    public void Test_TextureGeneratorCreateTexture() {

        int tileDim = 5;
        Color[] colors = new Color[ tileDim*tileDim ];

        for ( int i = 0; i < tileDim*tileDim; ++i ) {
            colors[ i ] = new Color(0.2f, 0.6f, 0.9f);
        }

        Texture2D tex = TextureGenerator.CreateTexture( colors, tileDim );
        
        Assert.AreEqual( tex.wrapMode, TextureWrapMode.Clamp );
        Assert.AreEqual( tex.filterMode, FilterMode.Point );
    }
}