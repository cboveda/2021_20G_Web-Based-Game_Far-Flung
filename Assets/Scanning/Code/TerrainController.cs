using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public static Material mapMaterial;

    public int chunkMeshDim = 225;
    public static Color[] terrainGradient = new Color[2];
    public GameObject satellite;
    public int renderDistance = 500;

    public static int chunkRenderRange;
    private Dictionary<Vector2, MapSection> mapSecDic = new Dictionary<Vector2, MapSection>();
    private List<MapSection> mapSecLst = new List<MapSection>();


    void Start() {
        chunkRenderRange = Mathf.RoundToInt( renderDistance / chunkMeshDim );
    }

    void Update() {

        // calculate the coordinates of 9 adjacent chunks;

        int currChunkX = (int) Mathf.Floor(satellite.transform.position.x / chunkMeshDim);
        int currChunkZ = (int) Mathf.Floor(satellite.transform.position.z / chunkMeshDim);

        foreach ( MapSection ms in mapSecLst ) {
            ms.UnsetVisible();
        }

        for ( int z = (currChunkZ-chunkRenderRange); z <= (currChunkZ+chunkRenderRange); ++z ) {
            for ( int x = (currChunkX-chunkRenderRange); x <= (currChunkX+chunkRenderRange); ++x ) {

                Vector2 pVec = new Vector2( x, z );

                if ( mapSecDic.ContainsKey( pVec ) ) {
                    mapSecDic[pVec].SetVisible();
                } else {
                    MapSection nSec = new MapSection( x, z, chunkMeshDim );
                    mapSecDic.Add( pVec, nSec );
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
        public Vector3 real_coord;

        GameObject meshObj;
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        public MapSection( int x, int z, int scale ) {

            isVisible = false;
            real_coord = new Vector3( (x * scale), 0, (z * scale) );

            meshObj = new GameObject("Mesh(" + real_coord.ToString() + ")");
            meshRenderer = meshObj.AddComponent<MeshRenderer>();
            meshFilter = meshObj.AddComponent<MeshFilter>();

            meshRenderer.material = mapMaterial;

            float[,] terrain = TerrainGenerator.GetTerrainHeights( (x * scale), (z * scale), scale);
            meshFilter.mesh = MeshGenerator.GenerateTerrainMesh(terrain, 50).CreateMesh();

            meshObj.transform.position = real_coord;

            // fetch a texture for mesh
            //Texture2D texture = TerrainTextureGenerator.CreateTexture(terrainGradient, terrain, scale, scale);
            //meshRenderer.sharedMaterial.mainTexture = texture;
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