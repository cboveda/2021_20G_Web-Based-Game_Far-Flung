using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileActions : MonoBehaviour
{

    SpriteRenderer rend;
    Color currentColor;
    bool onTile = false;
    bool validMove = false;
    string tileName = "";

    Vector3 tilePosition;
    Vector3 blankPosition;
    GameObject blank;

    void Start()
    {
        // get current tile color
        rend = GetComponent<SpriteRenderer>();
        currentColor = rend.color;
    }

    void OnMouseEnter()
    {
        // highlight tile
        rend.color = Color.yellow;
        tileName = rend.sprite.name;

        onTile = true;
        validMove = checkValidMove();
        Debug.Log(validMove);
    }

    void OnMouseExit()
    {
        // reset tile color
        rend.color = currentColor;
        onTile = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && onTile && validMove)
        {
            Debug.Log("Pressed primary button on " + tileName);

            // get current tile and blank tile positions
            tilePosition = rend.transform.position;
            blank = GameObject.Find("blank");
            blankPosition = blank.transform.position;

            // swap current tile and blank tile positons
            rend.transform.position = blankPosition;
            blank.transform.position = tilePosition;
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
}
