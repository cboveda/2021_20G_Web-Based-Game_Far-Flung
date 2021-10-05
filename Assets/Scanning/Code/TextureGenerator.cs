using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D CreateTexture( Color[] gradient, float[,] heights, int tileDim ) {
        Texture2D texture = new Texture2D(tileDim, tileDim);
		texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.SetPixels( CreateColorMap(gradient, heights, tileDim) );
        texture.Apply();
        return texture;
    }

    public static Color[] CreateColorMap( Color[] gradient, float[,] heights, int tileDim ) {

        Color[] colors = new Color[(tileDim * tileDim)];

        for ( int z = 0; z < tileDim; ++z ) {
            for ( int x = 0; x < tileDim; ++x ) {
                colors[ (z * tileDim) + x ] = Color.Lerp( gradient[0], gradient[1], heights[z,x]);
            }
        }

        return colors;
    }
}
