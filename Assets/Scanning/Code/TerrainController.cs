using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {

    public int tileDim = 100;
    public int renderDistX = 500;
    public int renderDistZ = 800;
    public float terrainScale = 150;
    public static int tileRenderRangeZ; // forward
    public static int tileRenderRangeX; // left-right
    public int terrainSeed = 20;
    public Gradient surfaceGrad;
    public GameObject satellite;
    public SignalSpawner sigSpawner;
    public AnimationCurve basePerlinCurve;

    [HideInInspector]
    public Dictionary<Vector2, OpenMapTile> tileDict = new Dictionary<Vector2, OpenMapTile>();

    Queue<MapTileJob> stageOne = new Queue<MapTileJob>();
    Queue<MapTileJob> stageTwo = new Queue<MapTileJob>();
    Queue<MapTileJob> stageThree = new Queue<MapTileJob>();

    List<Vector2> forRemoval = new List<Vector2>();
    bool unloadWaste;
    
    void Awake() {

        satellite.GetComponent<FlightControl>().speed = 0;

        tileRenderRangeZ = Mathf.RoundToInt( renderDistZ / tileDim );
        tileRenderRangeX = Mathf.RoundToInt( renderDistX / tileDim );
        unloadWaste = false;

        Update();
        while ( stageOne.Count != 0 ) {
            Update();
        }
    }

    void Start() {
        satellite.GetComponent<FlightControl>().speed = 40f;
    }

    void Update() {


        

        int currTileZ = (int) Mathf.Floor(satellite.transform.position.z / tileDim);
        int currTileX = (int) Mathf.Floor(satellite.transform.position.x / tileDim);

        for ( int z = currTileZ; z <= ( currTileZ + tileRenderRangeZ ); ++z ) 
        {
            for ( int x = ( currTileX - tileRenderRangeX ); x <= ( currTileX + tileRenderRangeX ); ++x ) 
            {
                Vector2 pVec = new Vector2( z, x ); // .x = z, .y = x

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

            if ( mt.Key.x < currTileZ && mt.Value.tile.fin ) {
                forRemoval.Add( mt.Key ); 
            }
        }

        foreach ( Vector2 v2 in forRemoval ) {

            tileDict[v2].tile.Destroy();
            tileDict.Remove( v2 );
            unloadWaste = true;
        }

        forRemoval.Clear();

        // cycle through queued jobs
        if ( stageThree.Count > 0 ) {
        
            MapTileFactory.MapTileStageThree( stageThree.Dequeue() );

        } else if ( stageTwo.Count > 0 ) {

            stageThree.Enqueue( MapTileFactory.MapTileStageTwo( stageTwo.Dequeue() ) );
            
        } else if ( stageOne.Count > 0 ) {
            
            stageTwo.Enqueue( MapTileFactory.MapTileStageOne( stageOne.Dequeue() ) );

        } else if ( unloadWaste ) {

            Resources.UnloadUnusedAssets();
            unloadWaste = false;
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