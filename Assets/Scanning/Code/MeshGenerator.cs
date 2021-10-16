using UnityEngine;

/* 
    A utility class for generating meshData objects for which represent
   terrain heights. 
*/

public static class MeshGenerator
{  
    public static MeshData GenerateTerrainMesh( Vector3 real_coord, int terrainSeed, int meshDim, float terrainScale ) {

        MeshData meshData = new MeshData( meshDim );

        Vector3 super_coord = real_coord - new Vector3( 1, 0, 1 );
        int superDim = meshDim + 2;

        float[,] superHeights = TerrainGenerator.GetTerrainHeights( super_coord, superDim, terrainSeed );

        meshData.normalizedHeightMap = getSubsetHeightMapFromSuperset( superHeights, meshDim, superDim );

        int vi = 0;

        // set mesh data
        for ( int z = 0; z < meshDim; ++z )
        {
            for ( int x = 0; x < meshDim; ++x )
            {
                meshData.vertices[vi] = new Vector3( x, ( terrainScale * meshData.normalizedHeightMap[x, z] ), z );

                if ( x < (meshDim-1) && z < (meshDim-1) ) 
                {
                    meshData.AddTriangle( vi, (vi+meshDim), (vi+1) );
                    meshData.AddTriangle( (vi+1), (vi+meshDim), (vi+meshDim+1) );
                }

                meshData.uvs[vi] = new Vector2( ( (float)x / ( meshDim - 1 ) ), ( (float)z / ( meshDim - 1 ) ) );
                vi++;
            }
        }
        return meshData;
    }

    private static float[,] getSubsetHeightMapFromSuperset( float[,] superHeights, int meshDim, int superDim ) {

        float[,] subHeights = new float[meshDim, meshDim];

        for ( int x = 1; x < ( superDim-1 ); ++x )
        {
            for ( int z = 1; z < ( superDim-1 ); ++z )
            {
                subHeights[ ( x-1 ), ( z-1 ) ] = superHeights[x,z];
            }
        }

        return subHeights;
    }
}

public class MeshData {

    public Vector3[] vertices;
    public Vector3[] normals;
    public Vector2[] uvs;
    public float[,] normalizedHeightMap;
    public int[] triangles;

    int triangleIndex;

    public MeshData( int meshDim ) {
        vertices = new Vector3[ meshDim * meshDim ];
        uvs = new Vector2[ meshDim * meshDim ];
        triangles = new int[ ( meshDim - 1 ) * ( meshDim - 1 ) * 6 ];
        triangleIndex = 0;
    }

    public void AddTriangle( int a, int b, int c ) {
        triangles[triangleIndex] = a;
        triangles[++triangleIndex] = b;
        triangles[++triangleIndex] = c;
        triangleIndex++;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        //mesh.normals = UpdateBorderNormals()
        
        return mesh;
    }

}