using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSpawner : MonoBehaviour
{
    public GameObject prefabSignal;
    
    [Range(0, 5)]
    public int spawnFrequency = 5;

    public GameObject[] CreateSignals( float[,] terrain_heights, float terrainScale, Vector3 terrainBase ) {

        int size = (int) Random.Range(0, spawnFrequency);

        GameObject[] sigRefArr = new GameObject[size];

        for ( int i = 0; i < size; ++i ) {

            int xCord = (int) Random.Range( 0, terrain_heights.GetLength(0) );
            int zCord = (int) Random.Range( 0, terrain_heights.GetLength(1) );
            int yCord = (int) (terrain_heights[xCord, zCord] * terrainScale ) - 1;

            sigRefArr[i] = Instantiate( prefabSignal, new Vector3( (terrainBase.x + xCord), ( 50 + yCord ), ( terrainBase.z + zCord) ), Quaternion.identity );
        }

        return sigRefArr;
    }
}
