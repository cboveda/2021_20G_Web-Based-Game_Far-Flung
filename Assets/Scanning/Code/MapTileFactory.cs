using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileFactory {

    public static void CreateMapTile(
            MapTile tile, int z, int x, int tileDim, float terrainScale, Gradient surfaceGrad, 
            int terrainSeed, AnimationCurve basePerlinCurve ) {

        tile.real_coord = new Vector3( (x * tileDim), 0, (z * tileDim) );
        tile.terrainScale = terrainScale;
        
        int meshDim = tileDim + 1;

        tile.meshData = MeshGenerator.GenerateTerrainMesh( tile.real_coord, terrainSeed, meshDim, terrainScale, basePerlinCurve );

        tile.colors = TextureGenerator.CreateColorMap( surfaceGrad, tile.meshData.normalizedHeightMap, meshDim );

        tile.fresh = true;
    }
}

public class MapTile {

    private bool isVisible;
    public bool fresh;
    public Vector3 real_coord;
    int meshDim;
    public float terrainScale;

    public GameObject meshObj;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public GameObject[] neutronSignals;
    public MeshData meshData;
    public Color[] colors;
    SignalSpawner sigSpawner;

    public MapTile( string tileName, int tileDim, SignalSpawner sigSpawner ) {

        isVisible = false;
        fresh = false;
        meshDim = tileDim + 1;
        meshObj = new GameObject( tileName );
        meshRenderer = meshObj.AddComponent<MeshRenderer>();
        meshFilter = meshObj.AddComponent<MeshFilter>();
        meshCollider = meshObj.AddComponent<MeshCollider>();
        this.sigSpawner = sigSpawner;
    }

    public void SetVisible() {
        isVisible = true;
    }

    public void UnsetVisible() {
        isVisible = false;
    }

    public void Update() {

        if ( fresh ) {
            meshFilter.mesh = meshData.CreateMesh();
            meshCollider.sharedMesh = meshFilter.mesh;
            meshObj.transform.position = real_coord;
            meshRenderer.material.mainTexture = TextureGenerator.CreateTexture( colors, meshDim );
            neutronSignals = sigSpawner.CreateSignals( meshData.normalizedHeightMap, terrainScale, real_coord );
            fresh = false;
        }
        if ( isVisible ) {
            meshObj.SetActive( isVisible );
        }
    }
}