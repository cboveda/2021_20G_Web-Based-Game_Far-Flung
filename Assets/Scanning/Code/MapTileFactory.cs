using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileFactory {

    public static MapTile CreateMapTile(
            int z, int x, int tileDim, float terrainScale, Gradient surfaceGrad, 
            SignalSpawner sigSpawner, int terrainSeed ) {

        
        Vector3 real_coord = new Vector3( (x * tileDim), 0, (z * tileDim) );
        Vector3 super_coord = real_coord - new Vector3(1, 0, 1);
        
        int meshDim = tileDim + 1;
        int meshSuperDim = tileDim + 3;

        MapTile tile = new MapTile( "Mesh(" + real_coord.ToString() + ")" );

        float[,] terrain = TerrainGenerator.GetTerrainHeights( real_coord, meshDim, terrainSeed );

        tile.meshFilter.mesh = MeshGenerator.GenerateTerrainMesh( terrain, meshDim, terrainScale ).CreateMesh();
        tile.meshRenderer.material.mainTexture = TextureGenerator.CreateTexture( surfaceGrad, terrain, meshDim );

        tile.meshCollider.sharedMesh = tile.meshFilter.mesh;
        tile.meshObj.transform.position = real_coord;

        tile.neutronSignals = sigSpawner.CreateSignals (terrain, terrainScale, real_coord );

        return tile;
    }
}

public class MapTile {

    private bool isVisible;
    public Vector3 real_coord;

    public GameObject meshObj;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public GameObject[] neutronSignals;

    public MapTile( string tileName ) {

        isVisible = false;
        meshObj = new GameObject( tileName );
        meshRenderer = meshObj.AddComponent<MeshRenderer>();
        meshFilter = meshObj.AddComponent<MeshFilter>();
        meshCollider = meshObj.AddComponent<MeshCollider>();
        // meshRenderer.material = mapMaterial;
    }

    public void SetVisible() {
        isVisible = true;
    }

    public void UnsetVisible() {
        isVisible = false;
    }

    public void Update() {
        meshObj.SetActive( isVisible );
    }
}