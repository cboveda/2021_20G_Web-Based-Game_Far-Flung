using UnityEngine;

/* 
    A utility class for generating meshData objects for which represent
   terrain heights. 
*/

public static class MeshGenerator
{  
    public static MeshData GenerateTerrainMesh( float[,] heightMap, float terrainHeight ) {

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        MeshData meshData = new MeshData( width, height );
        int vi = 0;

        // set mesh data
        for ( int z = 0; z < height; ++z ) {
            for ( int x = 0; x < width; ++x ) {
                meshData.vertices[vi] = new Vector3(x, (terrainHeight * heightMap[x,z]), z);
                meshData.uvs[vi] = new Vector2( x/(float)width, z/(float)height );

                if ( x < (width-1) && z < (height-1) ) {

                    meshData.AddTriangle( vi, (vi+width), (vi+1) );
                    meshData.AddTriangle( (vi+1), (vi+width), (vi+width+1) );
                }
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

    public MeshData( int meshWidth, int meshHeight ) {
        vertices = new Vector3[ meshWidth * meshHeight ];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[ (meshWidth - 1) * (meshHeight -1 ) * 6 ];
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
        return mesh;
    }
}