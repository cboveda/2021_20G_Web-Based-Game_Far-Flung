using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComGameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetTileStartPositions();
    }


    void SetTileStartPositions()
    {
        //Debug.Log("set start positions");

        float[] startPos = { 0.0F, 0.0F };
        int[] tilePositions = { 0, 11, 12, 13, 21, 22, 23, 24, 31, 32, 33, 34, };
        int x = 0;
        int y = 1;
        string tileNumber = "";
        GameObject tilePosObject;

        // set start positions
        foreach (int tilePosition in tilePositions)
        {
            startPos = FindObjectOfType<ComGameData>().getStartPosition(tilePosition);

            tileNumber = tilePosition.ToString();
            if (tileNumber == "0")
            {
                tileNumber = "blank";
            }
            tilePosObject = GameObject.Find(tileNumber);
            tilePosObject.transform.position = new Vector3(startPos[x], startPos[y]);

        }
    }
}