using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    public int tileMeshDim = 225;
    public int renderDistance = 500;
    public float terrainScale = 150;
    public static int tileRenderRange;
    public int terrainSeed = 20;
    public Gradient surfaceGrad;
    public GameObject satellite;
    public SignalSpawner sigSpawner;

    private Dictionary<Vector2, MapTile> mapSecDic = new Dictionary<Vector2, MapTile>();
    private List<MapTile> mapSecLst = new List<MapTile>();

    void Start() {
        tileRenderRange = Mathf.RoundToInt( renderDistance / tileMeshDim );
    }

    void Update() {
        
        // calculate the coordinates of 9 adjacent tiles, not memory efficient;

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileMeshDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileMeshDim);

        foreach ( MapTile ms in mapSecLst ) {
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
                    MapTile nSec = MapTileFactory.CreateMapTile( z, x, tileMeshDim, terrainScale, surfaceGrad, sigSpawner, terrainSeed );
                    mapSecDic.Add( pVec, nSec );
                    mapSecLst.Add( nSec );
                    nSec.SetVisible();
                }
            }
        }

        foreach ( MapTile ms in mapSecLst ) 
        {
            ms.Update();
        }
    }
}