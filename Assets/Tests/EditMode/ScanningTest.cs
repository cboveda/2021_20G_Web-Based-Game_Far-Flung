using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
        int meshDim = 5; 
        float terrainScale = 1f; 
        AnimationCurve basePerlinCurve = new AnimationCurve();

        MeshData data = MeshGenerator.GenerateTerrainMesh( real_coord, terrainSeed, meshDim, terrainScale, basePerlinCurve );

        data.superHeights;

        data.normalizedHeightMap;

        data.vertices;

        data.uvs;

        data.triangles;
    }
}