using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TileActions : MonoBehaviour
{

    SpriteRenderer rend;
    SpriteRenderer instructionsRend;
    Color currentColor;
    Color instructionsColor;
    bool onTile = false;
    bool validMove = false;
    string tileName = "";
    string winScene = "";
    bool success = false;

    Vector3 tilePosition;
    Vector3 blankPosition;
    GameObject blank;
    GameObject instructionsObject;
    GameObject instructionsBox;
    string objectName = "";
    string instructionsLayer = "";

    ComGameData gameData;


    void Start()
    {
        // get current tile color
        rend = GetComponent<SpriteRenderer>();
        currentColor = rend.color;


        // get the instructions objects starting color and layer
        instructionsObject = GameObject.Find("InstructionsText");
        instructionsColor = instructionsObject.GetComponent<Text>().color;

        instructionsBox = GameObject.Find("InstructionsBox");
        instructionsRend = instructionsBox.GetComponent<SpriteRenderer>();
        instructionsLayer = instructionsRend.sortingLayerName;
    }




    void OnMouseEnter()
    {
        // highlight tile
        rend.color = Color.yellow;
        tileName = rend.sprite.name;

        onTile = true;
        validMove = checkValidMove();
        //Debug.Log(validMove);

        // show instructions
        objectName = rend.transform.name;
        //Debug.Log(objectName);
        if (objectName == "Instructions")
        {            
            instructionsObject.GetComponent<Text>().color = Color.yellow;
            instructionsRend.sortingLayerName = "Numbers";
        }
        
        

    }

    void OnMouseExit()
    {
        // reset tile color
        rend.color = currentColor;
        onTile = false;

        // hide instructions
        if (objectName == "Instructions")
        {
            instructionsObject.GetComponent<Text>().color = instructionsColor;
            instructionsRend.sortingLayerName = instructionsLayer;
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && onTile && validMove)
        {
            //Debug.Log("Pressed primary button on " + tileName);

            // get current tile and blank tile positions
            tilePosition = rend.transform.position;
            blank = GameObject.Find("blank");
            blankPosition = blank.transform.position;

            // swap current tile and blank tile positons
            rend.transform.position = blankPosition;
            blank.transform.position = tilePosition;

            success = checkSuccess();

            if (success)
            {
                winScene = "comGameWin";
                SceneManager.LoadScene(winScene);
            }

        }
    }

    public bool checkValidMove()
    {
        blank = GameObject.Find("blank");
        blankPosition = blank.transform.position;
        tilePosition = rend.transform.position;

        float yDiff = 0.0F;
        float xDiff = 0.0F;
        bool validMove = false;

        // check if current tile and blank tile in same column
        if (tilePosition.x == blankPosition.x)
        {
            yDiff = Math.Abs(tilePosition.y - blankPosition.y);
            //Debug.Log(yDiff);
        }

        if (yDiff == 2.0F)
        {
            validMove = true;
        }

        // check if current tile and blank tile in same row
        if (validMove == false && tilePosition.y == blankPosition.y)
        {
            xDiff = Math.Abs(tilePosition.x - blankPosition.x);
            //Debug.Log(xDiff);
        }

        if (xDiff == 3.0F)
        {
            validMove = true;
        }

        return validMove;
    }

    public bool checkSuccess()
    {

        float[] winPos = { 0.0F, 0.0F };
        int numberOfTiles = 11;
        int position = 11;
        int increment = 1;
        Vector3 tilePos;
        string tileNumber = "";
        int x = 0;
        int y = 1;

        for (int i = 0; i < numberOfTiles; i++)
        {

            winPos = FindObjectOfType<ComGameData>().getWinPosition(position);

            tileNumber = position.ToString();
            tilePos = GameObject.Find(tileNumber).transform.position;

            if (i == 2)
            {
                position += 7;
            }
            if (i == 6)
            {
                position += 6;
            }
            position += increment;

            if (winPos[x] != tilePos.x)
            {
                //Debug.Log("false");
                return false;
            }

            if (winPos[y] != tilePos.y)
            {
                //Debug.Log("false");
                return false;
            }

            //Debug.Log(winPos[x]);
            //Debug.Log(tilePos.x);

            //Debug.Log(winPos[y]);
            //Debug.Log(tilePos.y);
        }

        //Debug.Log("true");
        return true;
    }
}