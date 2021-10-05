using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public static Material mapMaterial;

    public int tileMeshDim = 225;
    public Color[] terrainGradient = new Color[2];
    public GameObject satellite;
    public int renderDistance = 500;
    public float terrainScale;

    public static int tileRenderRange;
    private Dictionary<Vector2, MapSection> mapSecDic = new Dictionary<Vector2, MapSection>();
    private List<MapSection> mapSecLst = new List<MapSection>();


    void Start() {
        tileRenderRange = Mathf.RoundToInt( renderDistance / tileMeshDim );
        terrainScale = 150;
    }

    void Update() {

        // calculate the coordinates of 9 adjacent tiles;

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
                    MapSection nSec = new MapSection( z, x, tileMeshDim, terrainScale );
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

        public MapSection( int z, int x, int tileDim, float terrainScale ) {

            isVisible = false;
            real_coord = new Vector3( (x * tileDim), 0, (z * tileDim) );

            meshObj = new GameObject("Mesh(" + real_coord.ToString() + ")");
            meshRenderer = meshObj.AddComponent<MeshRenderer>();
            meshFilter = meshObj.AddComponent<MeshFilter>();

            meshRenderer.material = mapMaterial;

            int meshDim = tileDim + 1;

            float[,] terrain = TerrainGenerator.GetTerrainHeights( real_coord, meshDim );
            meshFilter.mesh = MeshGenerator.GenerateTerrainMesh( terrain, meshDim, terrainScale ).CreateMesh();

            meshObj.transform.position = real_coord;
            meshRenderer.material.SetColor("_color", Color.gray);
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