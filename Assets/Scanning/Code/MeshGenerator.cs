using UnityEngine;
using System.Runtime.CompilerServices;

/* 
    A utility class for generating meshData objects for which represent
   terrain heights. 
*/

public static class MeshGenerator
{  
    public static MeshData GenerateTerrainMesh( Vector3 real_coord, int terrainSeed, 
            int meshDim, float terrainScale, AnimationCurve basePerlinCurve ) {

        MeshData meshData = new MeshData( meshDim, terrainScale );
        Vector3 super_coord = real_coord - new Vector3( 1, 0, 1 );
        int superDim = meshDim + 2;

        meshData.superHeights = TerrainGenerator.GetTerrainHeights( super_coord, superDim, terrainSeed, basePerlinCurve );
        meshData.normalizedHeightMap = getSubsetHeightMapFromSuperset( meshData.superHeights, meshDim, superDim );

        // set mesh data
        int vi = 0;
        for ( int z = 0; z < meshDim; ++z )
        {
            for ( int x = 0; x < meshDim; ++x )
            {
                meshData.vertices[vi] = new Vector3( x, ( terrainScale * meshData.normalizedHeightMap[x, z] ), z );

                if ( x < (meshDim-1) && z < (meshDim-1) ) 
                {
                    meshData.AddTriangle( (vi),   (vi+meshDim), (vi+1)         );
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
    public float[,] superHeights;
    public int[] triangles;

    int triangleIndex;
    int meshDim;
    float terrainScale;

    public MeshData( int meshDim, float terrainScale ) {
        vertices = new Vector3[ meshDim * meshDim ];
        uvs = new Vector2[ meshDim * meshDim ];
        triangles = new int[ ( meshDim - 1 ) * ( meshDim - 1 ) * 6 ];
        triangleIndex = 0;
        this.meshDim = meshDim;
        this.terrainScale = terrainScale;
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
        mesh.RecalculateNormals(); // ~ 0.3 ms 
        mesh.normals = UpdateBorderNormals( mesh.normals ); // ~ 10 ms, god awful 
        return mesh;
    }

    /* This fucntion designates the border normal vectors of a mesh which need to be recalculated
     * in light of adjacent mesh normals 
     */
    Vector3[] UpdateBorderNormals( Vector3[] normals ) {

        for ( int x = 0; x < meshDim; ++x )
        {
            int z = 0;

            normals[ x ] = CalcNormalFromPoints( (x+1), (z+1) );
            
            z = ( meshDim-1 );

            normals[ x + (z * meshDim) ] = CalcNormalFromPoints( (x+1), (z+1) );
        }
        
        for ( int z = 0; z < meshDim; ++z )
        {
            int x = 0;
            
            normals[ z * meshDim ] = CalcNormalFromPoints( (x+1), (z+1) );

            x = ( meshDim-1 );

            normals[ x + (z * meshDim) ] = CalcNormalFromPoints( (x+1), (z+1) );
        }
        return normals;
    }

    /* A single border normals vector is recalculatd from the six adjacent triangle vectors */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    Vector3 CalcNormalFromPoints( int x, int z ) {

        Vector3 norm = AdjacentTriangleNormal( Vscale( x, z ), Vscale( x-1, z ), Vscale( x-1, z+1 ) );

        norm += AdjacentTriangleNormal( Vscale( x, z ), Vscale( x-1, z+1 ), Vscale( x  , z+1 ) );
        norm += AdjacentTriangleNormal( Vscale( x, z ), Vscale( x  , z+1 ), Vscale( x+1, z   ) );
        norm += AdjacentTriangleNormal( Vscale( x, z ), Vscale( x+1, z   ), Vscale( x+1, z-1 ) );
        norm += AdjacentTriangleNormal( Vscale( x, z ), Vscale( x+1, z-1 ), Vscale( x  , z-1 ) );
        norm += AdjacentTriangleNormal( Vscale( x, z ), Vscale( x  , z-1 ), Vscale( x-1, z   ) );

        return norm.normalized;
    }

    /* Creates vertical scaled terrain heights using the super map featuring adjacent mesh data */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    Vector3 Vscale( int x, int z ) {
        return new Vector3( x, terrainScale * superHeights[x, z], z );
    }

    /* Calculates the cross product of three vectors */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    Vector3 AdjacentTriangleNormal( Vector3 r, Vector3 a, Vector3 b ) {
        Vector3 ra = a - r;
        Vector3 rb = b - r;
        return Vector3.Cross( ra, rb ).normalized;
    }
}