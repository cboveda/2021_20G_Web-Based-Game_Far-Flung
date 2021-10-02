using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainTextureGenerator
{
    public static Texture2D CreateTexture( Color[] gradient, float[,] heights, int zDim, int xDim ) {
        Texture2D texture = new Texture2D(zDim, xDim);
		texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.SetPixels( CreateColorMap(gradient, heights, zDim, xDim) );
        texture.Apply();
        return texture;
    }

    private static Color[] CreateColorMap( Color[] gradient, float[,] heights, int zDim, int xDim ) {

        Color[] colors = new Color[(zDim * xDim)];

        for ( int z = 0; z < zDim; ++z ) {
            for ( int x = 0; x < xDim; ++x ) {
                colors[ (z * xDim) + x ] = Color.Lerp( gradient[0], gradient[1], heights[z,x]);
            }
        }

        return colors;
    }
}
