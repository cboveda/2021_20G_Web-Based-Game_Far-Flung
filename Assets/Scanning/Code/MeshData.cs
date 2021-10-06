using UnityEngine;

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
        return mesh;
    }
}