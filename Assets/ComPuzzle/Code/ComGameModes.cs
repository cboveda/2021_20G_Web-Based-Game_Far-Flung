using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ComGameModes : MonoBehaviour
{
        
    GameObject tileNumberObject;
    SpriteRenderer tileNumberRend;
    Toggle easyToggle;
    GameObject easyToggleObject;
    GameObject tilePosObject;
    GameObject blankPosObject;

    public void EasyModeOn()
    {
        //Debug.Log("easy mode");

        string[] tileNumbers = { "n1", "n2", "n3", "n4", "n5", "n6", "n7", "n8", "n9", "n10", "n11", "n12" };
        string sortingLayer = "";
        string finalSortingLayer = "";

        easyToggleObject = GameObject.Find("EasyMode");
        easyToggle = easyToggleObject.GetComponent<Toggle>();

        sortingLayer = "Default";        
        if (easyToggle.isOn)
        {
            sortingLayer = "Numbers";
            finalSortingLayer = "FinalPieceNumber";
        }

        foreach (string tileNumber in tileNumbers)
        {
            // set the sorting layer for the tile numbers based on mode
            tileNumberObject = GameObject.Find(tileNumber);
            tileNumberRend = tileNumberObject.GetComponent<SpriteRenderer>();
            tileNumberRend.sortingLayerName = sortingLayer;
            if ( tileNumber == "n4")
            {
                tileNumberRend.sortingLayerName = finalSortingLayer;
            }
        }       
    }

    public void SolvePuzzle()
    {
        //Debug.Log("solve puzzle");

        string tileNumber = "";
        float[] winPos = { 0.0F, 0.0F };
        int blankPosition = 0;
        int x = 0;
        int y = 1;
        int[] tilePositions = { 11, 12, 13, 21, 22, 23, 24, 31, 32, 33, 34, };

        foreach (int tilePosition in tilePositions)
        {
            // set the win position for each tile
            winPos = FindObjectOfType<ComGameData>().getWinPosition(tilePosition);
            tileNumber = tilePosition.ToString();
            tilePosObject = GameObject.Find(tileNumber);
            tilePosObject.transform.position = new Vector3(winPos[x], winPos[y]);         
        }

        // set the win position for blank tile 
        winPos = FindObjectOfType<ComGameData>().getWinPosition(blankPosition);
        blankPosObject = GameObject.Find("blank");
        blankPosObject.transform.position = new Vector3(winPos[x], winPos[y]);

        FindObjectOfType<TileActions>().showFinalInstructions();

    }

}
