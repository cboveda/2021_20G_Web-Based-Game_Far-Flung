using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    public int tileDim = 225;
    public int renderDistance = 500;
    public float terrainScale = 150;
    public static int tileRenderRange;
    public int terrainSeed = 20;
    public Gradient surfaceGrad;
    public GameObject satellite;
    public SignalSpawner sigSpawner;
    public AnimationCurve basePerlinCurve;

    private Dictionary<Vector2, MapTile> mapSecDic = new Dictionary<Vector2, MapTile>();
    private List<MapTile> mapSecLst = new List<MapTile>();

    void Start() {
        tileRenderRange = Mathf.RoundToInt( renderDistance / tileDim );
    }

    void Update() {
        
        // calculate the coordinates of 9 adjacent tiles, not memory efficient;

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileDim);

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
                    MapTile tile = new MapTile( "Mesh( " + z + ", " + x + " )", tileDim, sigSpawner );

                    ThreadStart threadStart = delegate {
                        MapTileFactory.CreateMapTile( tile, z, x, tileDim, terrainScale, surfaceGrad, terrainSeed, basePerlinCurve );
                    };

                    new Thread(threadStart).Start();

                    mapSecDic.Add( pVec, tile );
                    mapSecLst.Add( tile );
                    tile.SetVisible();
                }
            }
        }

        foreach ( MapTile ms in mapSecLst ) 
        {
            ms.Update();
        }
    }
}