using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Dictionary<Vector2, OpenMapTile> tileDict = new Dictionary<Vector2, OpenMapTile>();
    private ConcurrentQueue<MapTile> readyTiles = new ConcurrentQueue<MapTile>();
    private ConcurrentQueue<MapTileJob> tileJobs = new ConcurrentQueue<MapTileJob>();

    void Start() {

        tileRenderRange = Mathf.RoundToInt( renderDistance / tileDim );

        Thread t = new Thread( RunTileGenerator );
        FlightControl fc = FindObjectOfType<FlightControl>();

        fc.addListener( t );
        t.Start();
    }

    void Update() {
        
        // calculate the coordinates of 9 adjacent tiles, not memory efficient;

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileDim);

        for ( int z = currTileZ; z <= ( currTileZ + tileRenderRange ); ++z ) 
        {
            for ( int x = ( currTileX - tileRenderRange ); x <= ( currTileX + tileRenderRange ); ++x ) 
            {
                Vector2 pVec = new Vector2( z, x );

                if ( tileDict.ContainsKey( pVec ) ) {

                    tileDict[pVec].tile.SetVisible();

                } else {

                    MapTile tile = new MapTile( "Mesh( " + z + ", " + x + " )", sigSpawner );

                    tileJobs.Enqueue (
                        new MapTileJob {
                            tile = tile, z = z, x = x, tileDim = tileDim, terrainScale = terrainScale, 
                            surfaceGrad = surfaceGrad, terrainSeed = terrainSeed, basePerlinCurve = basePerlinCurve
                        }
                    );

                    OpenMapTile omTile = new OpenMapTile {
                        key = pVec, tile = tile
                    };

                    tileDict.Add( pVec, omTile );
                }
            }
        }

        foreach ( KeyValuePair<Vector2, OpenMapTile> mt in tileDict ) {
      
            mt.Value.tile.Update();
            mt.Value.tile.UnsetVisible();

            if ( mt.Value.key.x < currTileZ ) { // .x is in this case the Z vector, 2d vs 3d vector
                tileDict.Remove( mt.Value.key );
            }
        }

        MapTile oTile;

        if ( readyTiles.TryDequeue( out oTile ) ) {

            oTile.GenerateTile();
        }
    }

    void RunTileGenerator() {

        try {
            while ( true ) {

                MapTileJob job;

                if ( tileJobs.TryDequeue( out job ) ) {

                    MapTileFactory.CreateMapTile( job.tile, job.z, job.x, job.tileDim, 
                        job.terrainScale, job.surfaceGrad, job.terrainSeed, job.basePerlinCurve );

                    readyTiles.Enqueue( job.tile );
                }
                Debug.Log( "gen up" );
            }
        } catch ( ThreadAbortException ex ) {
            Debug.Log( "Tile generator closed : " + ex.ToString() );
        }
    }
}

public struct MapTileJob {
    public MapTile tile; 
    public int z; 
    public int x;
    public int tileDim;
    public float terrainScale;
    public Gradient surfaceGrad;
    public int terrainSeed;
    public AnimationCurve basePerlinCurve;
}

public struct OpenMapTile {
    public Vector2 key;
    public MapTile tile;
}