using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    public int tileDim = 100;
    public int renderDistance = 500;
    public float terrainScale = 150;
    public static int tileRenderRange;
    public int terrainSeed = 20;
    public Gradient surfaceGrad;
    public GameObject satellite;
    public SignalSpawner sigSpawner;
    public AnimationCurve basePerlinCurve;

    private Dictionary<Vector2, OpenMapTile> tileDict = new Dictionary<Vector2, OpenMapTile>();
    private Queue<MapTileJob> stageOne = new Queue<MapTileJob>();
    private Queue<MapTileJob> stageTwo = new Queue<MapTileJob>();

    void Start() {

        tileRenderRange = Mathf.RoundToInt( renderDistance / tileDim );
    }

    void Update() {

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

                    stageOne.Enqueue (
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

        // update each tiles visibility each frame and prune tiles from dict as the fall behind
        foreach ( KeyValuePair<Vector2, OpenMapTile> mt in tileDict ) {
    
            mt.Value.tile.Update();
            mt.Value.tile.UnsetVisible();
        }

        if ( stageTwo.Count > 0 ) {

            stageTwo.Dequeue().tile.GenerateTile();

        } else if ( stageOne.Count > 0 ) {

            MapTileJob job = stageOne.Dequeue();
            
            MapTileFactory.CreateMapTile( job.tile, job.z, job.x, job.tileDim, 
                job.terrainScale, job.surfaceGrad, job.terrainSeed, job.basePerlinCurve );

            stageTwo.Enqueue( job );
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
    public volatile MapTile tile;
}