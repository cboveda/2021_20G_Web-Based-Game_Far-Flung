using UnityEngine;

public class MapTileFactory {

    static Shader shad = Shader.Find("Custom/CurvedWorld");

    public static MapTileJob MapTileStageOne( MapTileJob job ) {

        job.coord = new Vector3( (job.x * job.tileDim), 0, (job.z * job.tileDim) );
        job.meshDim = job.tileDim + 1;
        job.meshData = MeshGenerator.GenerateTerrainMesh( job.coord, job.tSeed, job.meshDim, job.tScale, job.pCurve );
        job.colors = TextureGenerator.CreateColorMap( job.surfaceGrad, job.meshData.normalizedHeightMap, job.meshDim );
        return job;
    }

    public static MapTileJob MapTileStageTwo( MapTileJob job ) {

        job.tile.meshFilter.mesh = job.meshData.CreateMesh();
        job.tile.meshCollider.sharedMesh = job.tile.meshFilter.mesh;
        return job;
    }

    public static void MapTileStageThree( MapTileJob job ) {
        job.tile.meshRenderer.material.shader = shad;
        job.tile.meshRenderer.material.mainTexture = TextureGenerator.CreateTexture( job.colors, job.meshDim );
        job.tile.meshObj.transform.position = job.coord;
        job.tile.neutronSignals = job.sigSpawner.CreateSignals( job.meshData.normalizedHeightMap, job.tScale, job.coord );
        job.tile.fin = true;
    }
}

public class MapTile {

    private bool isVisible;
    public bool fin;

    public GameObject meshObj;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public GameObject[] neutronSignals;

    public MapTile( string tileName ) {

        isVisible = false;
        fin = false;

        meshObj = new GameObject( tileName );
        meshRenderer = meshObj.AddComponent<MeshRenderer>();
        meshFilter = meshObj.AddComponent<MeshFilter>();
        meshCollider = meshObj.AddComponent<MeshCollider>();
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

    public void Destroy() {

        Object.Destroy( meshFilter.sharedMesh );
        Object.Destroy( meshFilter.mesh );
        Object.Destroy( meshFilter );

        Object.Destroy( meshCollider.sharedMesh );
        Object.Destroy( meshCollider.material );
        Object.Destroy( meshCollider );

        Object.Destroy( meshRenderer.material.mainTexture );
        Object.Destroy( meshRenderer.material );
        Object.Destroy( meshRenderer );
        
        Object.Destroy( meshObj );

        foreach ( GameObject o in neutronSignals ) {
            Object.Destroy( o );
        }
    }
}