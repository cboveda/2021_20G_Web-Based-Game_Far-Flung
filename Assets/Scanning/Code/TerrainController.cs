using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public static Material mapMaterial;

    public int tileMeshDim = 225;
    public int renderDistance = 500;
    public float terrainScale = 150;
    public static int tileRenderRange;
    public Gradient surfaceGrad;
    public GameObject satellite;
    public SignalSpawner sigSpawner;

    private Dictionary<Vector2, MapSection> mapSecDic = new Dictionary<Vector2, MapSection>();
    private List<MapSection> mapSecLst = new List<MapSection>();


    void Start() {
        tileRenderRange = Mathf.RoundToInt( renderDistance / tileMeshDim );
    }

    void Update() {

        // calculate the coordinates of 9 adjacent tiles, not memory efficient;

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileMeshDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileMeshDim);

        foreach ( MapSection ms in mapSecLst ) {
            ms.UnsetVisible();
        }

        for ( int z = ( currTileZ - tileRenderRange ); z <= ( currTileZ + tileRenderRange ); ++z ) 
        {
            for ( int x = ( currTileX - tileRenderRange ); x <= ( currTileX + tileRenderRange ); ++x ) 
            {
                Vector2 pVec = new Vector2( z, x );
                if ( mapSecDic.ContainsKey( pVec ) ) 
                {
                    mapSecDic[pVec].SetVisible();
                } 
                else 
                {
                    MapSection nSec = new MapSection( z, x, tileMeshDim, terrainScale, surfaceGrad, sigSpawner );
                    mapSecDic.Add( pVec, nSec );
                    mapSecLst.Add( nSec );
                    nSec.SetVisible();
                }
            }
        }

        foreach ( MapSection ms in mapSecLst ) 
        {
            ms.Update();
        }
    }

    public class MapSection {

        private bool isVisible;
        public Vector3 real_coord;

        GameObject meshObj;
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;
        GameObject[] neutronSignals;

        public MapSection( int z, int x, int tileDim, float terrainScale, Gradient surfaceGrad, SignalSpawner sigSpawner ) {

            isVisible = false;
            real_coord = new Vector3( (x * tileDim), 0, (z * tileDim) );

            meshObj = new GameObject("Mesh(" + real_coord.ToString() + ")");
            meshRenderer = meshObj.AddComponent<MeshRenderer>();
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshCollider = meshObj.AddComponent<MeshCollider>();

            meshRenderer.material = mapMaterial;

            int meshDim = tileDim + 1;

            float[,] terrain = TerrainGenerator.GetTerrainHeights( real_coord, meshDim );
            meshFilter.mesh = MeshGenerator.GenerateTerrainMesh( terrain, meshDim, terrainScale ).CreateMesh();
            meshRenderer.material.mainTexture = TextureGenerator.CreateTexture( surfaceGrad, terrain, meshDim );

            meshCollider.sharedMesh = meshFilter.mesh;
            meshObj.transform.position = real_coord;

            neutronSignals = sigSpawner.CreateSignals (terrain, terrainScale, real_coord );
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
}