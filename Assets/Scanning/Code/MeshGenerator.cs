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