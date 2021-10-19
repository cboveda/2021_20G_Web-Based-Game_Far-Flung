using System.Collections.Generic;
using System.Collections.Concurrent;
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

    private Dictionary<Vector2, MapTile> tileDict = new Dictionary<Vector2, MapTile>();
    private ConcurrentQueue<MapTileReady> readyTiles = new ConcurrentQueue<MapTileReady>();
    private ConcurrentQueue<MapTileJob> tileJobs = new ConcurrentQueue<MapTileJob>();

    void Start() {

        tileRenderRange = Mathf.RoundToInt( renderDistance / tileDim );

        new Thread( RunTileGenerator ).Start();
    }

    void Update() {
        
        // calculate the coordinates of 9 adjacent tiles, not memory efficient;

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileDim);

        for ( int z = ( currTileZ - tileRenderRange ); z <= ( currTileZ + tileRenderRange ); ++z ) 
        {
            for ( int x = ( currTileX - tileRenderRange ); x <= ( currTileX + tileRenderRange ); ++x ) 
            {
                Vector2 pVec = new Vector2( z, x );

                if ( tileDict.ContainsKey( pVec ) ) {

                    tileDict[pVec].SetVisible();

                } else {

                    MapTile tile = new MapTile( "Mesh( " + z + ", " + x + " )", sigSpawner );

                    tileJobs.Enqueue (
                        new MapTileJob {
                            key = pVec, tile = tile, z = z, x = x, tileDim = tileDim, terrainScale = terrainScale, 
                            surfaceGrad = surfaceGrad, terrainSeed = terrainSeed, basePerlinCurve = basePerlinCurve
                        }
                    );

                    tileDict.Add( pVec, tile );
                }
            }
        }

        foreach ( KeyValuePair<Vector2, MapTile> mt in tileDict ) {
      
            mt.Value.Update();
            mt.Value.UnsetVisible();
        }

        MapTileReady rTile;

        if ( readyTiles.TryDequeue( out rTile ) ) {

            rTile.tile.GenerateTile();

        }
    }

    void RunTileGenerator() {
        
        while ( true ) {

            MapTileJob job;

            if ( tileJobs.TryDequeue( out job ) ) {

                MapTileFactory.CreateMapTile( job.tile, job.z, job.x, job.tileDim, 
                    job.terrainScale, job.surfaceGrad, job.terrainSeed, job.basePerlinCurve );

                readyTiles.Enqueue( 
                    new MapTileReady {
                        key = job.key, tile = job.tile
                    }
                 );
            }
        }
    }
}

public struct MapTileJob {
    public Vector2 key;
    public MapTile tile; 
    public int z; 
    public int x;
    public int tileDim;
    public float terrainScale;
    public Gradient surfaceGrad;
    public int terrainSeed;
    public AnimationCurve basePerlinCurve;
}

public struct MapTileReady {
    public Vector2 key;
    public MapTile tile;
}