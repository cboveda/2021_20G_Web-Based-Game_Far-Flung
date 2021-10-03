using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public int chunkMeshDim = 301;
    public Color[] terrainGradient = new Color[2];
    public GameObject satellite;
    public int renderDistance = 500;

    private int chunkRenderRange;
    private Dictionary<Vector2, MapSection> mapSecDic = new Dictionary<Vector2, MapSection>();
    private List<MapSection> mapSecLst = new List<MapSection>();


    void Start() {

        chunkRenderRange = Mathf.RoundToInt( renderDistance / chunkMeshDim );

        float[,] terrain = TerrainGenerator.GetTerrainHeights(0, 0, chunkMeshDim);
        meshFilter.sharedMesh = MeshGenerator.GenerateTerrainMesh(terrain, 50).CreateMesh();

        // fetch a texture for mesh
        Texture2D texture = TerrainTextureGenerator.CreateTexture(terrainGradient, terrain, chunkMeshDim, chunkMeshDim);
        meshRenderer.sharedMaterial.mainTexture = texture;


    }

    void UpdateChunksInView() {

        // calculate the coordinates of 9 adjacent chunks;

        int currChunkX = (int) Mathf.Floor(satellite.transform.position.x / chunkMeshDim);
        int currChunkZ = (int) Mathf.Floor(satellite.transform.position.z / chunkMeshDim);

        Vector2 vecSample = new Vector2();

        foreach ( MapSection ms in mapSecLst ) {
            ms.UnsetVisible();
        }

        for ( int z = (currChunkZ-chunkRenderRange); z <= (currChunkZ+chunkRenderRange); ++z ) {
            for ( int x = (currChunkX-chunkRenderRange); x <= (currChunkX+chunkRenderRange); ++x ) {

                vecSample.x = x;
                vecSample.y = z;

                if ( mapSecDic.ContainsKey( vecSample ) ) {
                    mapSecDic[vecSample].SetVisible();
                } else {
                    Vector2 nVec = new Vector2(x,z);
                    MapSection nSec = new MapSection(x,z);
                    mapSecDic.Add( nVec, nSec );
                    mapSecLst.Add( nSec );
                    nSec.SetVisible();
                }
            }
        }

        foreach ( MapSection ms in mapSecLst ) {
            ms.Update();
        }

    }

    public class MapSection {

        private bool isVisible;
        public Vector2 coord;

        public MapSection(int x, int z) {
            isVisible = false;
            coord = new Vector2(x, z);
        }

        public void SetVisible() {
            isVisible = true;
        }

        public void UnsetVisible() {
            isVisible = false;
        }

        public void Update() {}

    }


}