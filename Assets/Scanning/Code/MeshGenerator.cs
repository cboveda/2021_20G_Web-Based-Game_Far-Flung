using UnityEngine;

/* 
    A utility class for generating meshData objects for which represent
   terrain heights. 
*/

public static class MeshGenerator
{  
    public static MeshData GenerateTerrainMesh( float[,] heightMap, int meshDim, float terrainScale ) {

        MeshData meshData = new MeshData( meshDim );
        int vi = 0;

        // set mesh data
        for ( int z = 0; z < meshDim; ++z )
        {
            for ( int x = 0; x < meshDim; ++x )
            {
                meshData.vertices[vi] = new Vector3( x, ( terrainScale * heightMap[x, z] ), z );

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
}

public class MeshData {

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

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

        // prune vertices
        // prune triangles
        // prune normals

        return mesh;
    }
}