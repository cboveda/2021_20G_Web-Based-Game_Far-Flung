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
    private Queue<MapTileJob> stageThree = new Queue<MapTileJob>();
    
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

                    MapTile tile = new MapTile( "Mesh( " + z + ", " + x + " )" );

                    stageOne.Enqueue (
                        new MapTileJob {
                            tile = tile, z = z, x = x, tileDim = tileDim, tScale = terrainScale, 
                            surfaceGrad = surfaceGrad, tSeed = terrainSeed, pCurve = basePerlinCurve,
                            sigSpawner = sigSpawner
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

        if ( stageThree.Count > 0 ) {
        
            MapTileFactory.MapTileStageThree( stageThree.Dequeue() );

            // finish process

        } else if ( stageTwo.Count > 0 ) {

            stageThree.Enqueue( MapTileFactory.MapTileStageTwo( stageTwo.Dequeue() ) );
            
        } else if ( stageOne.Count > 0 ) {
            
            stageTwo.Enqueue( MapTileFactory.MapTileStageOne( stageOne.Dequeue() ) );
        }        
    }
}

public struct MapTileJob {
    public MapTile tile; 
    public Vector3 coord;
    public Gradient surfaceGrad;
    public AnimationCurve pCurve;
    public SignalSpawner sigSpawner;
    public MeshData meshData;
    public Color[] colors;
    public int z; 
    public int x;
    public int tileDim;
    public int meshDim;
    public float tScale;
    public int tSeed;
}

public struct OpenMapTile {
    public Vector2 key;
    public MapTile tile;
}