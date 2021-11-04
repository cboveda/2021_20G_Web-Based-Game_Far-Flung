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
    bool success = false;

    Vector3 tilePosition;
    Vector3 blankPosition;
    GameObject blank;
    GameObject instructionsObject;
    GameObject instructionsBox;
    string objectName = "";
    string instructionsLayer = "";

    ComGameData gameData;

    string finalPiece = "row-1-column-4";
    string spriteName = "";
    bool mouseDown = false;
    bool finalOn = false;
    GameObject successObject;

    GameObject finalInstructionsObject;
    GameObject finalInstructionsBox;
    string finalInstructionsLayer = "";
    Color finalInstructionsColor;
    SpriteRenderer finalInstructionsRend;

    GameObject finalPieceObject;
    Vector3 finalPiecePosition;
    Vector3 finalPieceCurrentPos;

    GameObject viewImageObject;
    Color viewImageColor;
    SpriteRenderer viewImageRend;

    GameObject imageObject;
    string imageLayer = "";
    SpriteRenderer imageRend;

    GameObject lockedObject;
    string lockLayer = "";

    GameObject continueButton;
    GameObject continueText;

    GameObject background;
    GameObject board;

    GameObject imageTextBox;
    GameObject imageBox;
    GameObject instructionsTextBox;
    GameObject instructions;

    GameObject easyModeBackground;
    GameObject easyModeLabel;
    GameObject easyModeBox;
    GameObject solveText;
    GameObject solveButton;
        
    bool disable = false;


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

        // get the final instructions objects starting color and layer
        finalInstructionsObject = GameObject.Find("FinalInstructionsText");
        finalInstructionsColor = finalInstructionsObject.GetComponent<Text>().color;

        finalInstructionsBox = GameObject.Find("FinalInstructionsBox");
        finalInstructionsRend = finalInstructionsBox.GetComponent<SpriteRenderer>();
        finalInstructionsLayer = finalInstructionsRend.sortingLayerName;

        // get final piece position
        finalPieceObject = GameObject.Find("14");
        finalPiecePosition = finalPieceObject.transform.position;


        // get the image objects starting color and layer
        viewImageObject = GameObject.Find("ViewImage");
        viewImageRend = viewImageObject.GetComponent<SpriteRenderer>();
        viewImageColor = viewImageRend.color;
        imageObject = GameObject.Find("Image");
        imageRend = imageObject.GetComponent<SpriteRenderer>();
        imageLayer = imageRend.sortingLayerName;

        // get the locked objects starting color 
        lockedObject = GameObject.Find("lock");
        lockLayer = lockedObject.GetComponent<SpriteRenderer>().sortingLayerName;

        continueButton = GameObject.Find("Continue");
        continueButton.GetComponent<Button>().enabled = false;
        continueButton.GetComponent<Image>().enabled = false;

        continueText = GameObject.Find("ContinueText");
        continueText.GetComponent<Text>().enabled = false;
               

    }




    public void OnMouseEnter()
    {

        // disable tiles
        if (FindObjectOfType<TileActions>().DisableTiles == true)
        {
            return;
        }

        // highlight tile
        spriteName = rend.sprite.name;
        success = checkSuccess();
        if (spriteName != finalPiece || success)
        {
            rend.color = Color.yellow;
        }

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

        // show image
        if (objectName == "ViewImage")
        {
            viewImageRend.color = Color.yellow;
            imageRend.sortingLayerName = "ViewImage";
        }
        
        // show locked
        success = checkSuccess();
        if (objectName == "14" && !success)
        {            
            lockedObject.GetComponent<SpriteRenderer>().sortingLayerName = "WinBackground";
        }

    }

    public void OnMouseExit()
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

        // hide image
        if (objectName == "ViewImage")
        {
            viewImageRend.color = viewImageColor;
            imageRend.sortingLayerName = imageLayer;
        }

        // hide locked
        if (objectName == "14")
        {            
            lockedObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
        }
    }

    public void OnMouseDown()
    {
        mouseDown = true;
    }

    public void OnMouseUp()
    {
        mouseDown = false;
    }

    public void OnTriggerEnter2D(Collider2D puzzlePiece)
    {

        if (puzzlePiece.gameObject.name == "14")
        {
            //Debug.Log("trigger on");
            finalOn = true;
        }

    }

    public void OnTriggerExit2D(Collider2D puzzlePiece)
    {

        if (puzzlePiece.gameObject.name == "14")
        {
            //Debug.Log("trigger off");
            finalOn = false;
        }

    }

    void Update()
    {
        
        // disable tiles
        if (FindObjectOfType<TileActions>().DisableTiles == true )
        {
            return;
        }

        spriteName = rend.sprite.name;

        if (Input.GetMouseButtonDown(0) && onTile && validMove)
        {
            //Debug.Log("Pressed primary button on " + spriteName);

            // get current tile and blank tile positions
            tilePosition = rend.transform.position;
            blank = GameObject.Find("blank");
            blankPosition = blank.transform.position;

            // swap current tile and blank tile positons
            rend.transform.position = blankPosition;
            blank.transform.position = tilePosition;

            success = checkSuccess();
            hideFinalInstructions();
            if (success)
            {
                showFinalInstructions();
            }
            
        }

        if (mouseDown && spriteName == finalPiece && success)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }

        if (finalOn && !Input.GetMouseButton(0))
        {
            //Debug.Log("final");         

            hideFinalInstructions();

            float[] finalWinPos = { 0.0F, 0.0F };
            int x = 0;
            int y = 1;
            finalWinPos = FindObjectOfType<ComGameData>().getWinPosition(0);
            finalPieceObject.transform.position = new Vector3(finalWinPos[x], finalWinPos[y]);

            // disable tiles
            FindObjectOfType<TileActions>().DisableTiles = true;            

            // disable canvas objects
            imageTextBox = GameObject.Find("ImageTextBox");
            imageTextBox.GetComponent<Text>().enabled = false;
            imageBox = GameObject.Find("ViewImage");
            imageBox.GetComponent<SpriteRenderer>().enabled = false;

            instructionsTextBox = GameObject.Find("InstructionsTextBox");
            instructionsTextBox.GetComponent<Text>().enabled = false;

            instructions = GameObject.Find("Instructions");
            instructions.GetComponent<SpriteRenderer>().enabled = false;

            easyModeBackground = GameObject.Find("EasyModeBackground");
            easyModeBackground.GetComponent<Image>().enabled = false;

            easyModeLabel = GameObject.Find("EasyModeLabel");
            easyModeLabel.GetComponent<Text>().enabled = false;

            easyModeBox = GameObject.Find("EasyModeBox");
            easyModeBox.GetComponent<SpriteRenderer>().enabled = false;

            solveButton = GameObject.Find("Solve");
            solveButton.GetComponent<Image>().enabled = false;

            solveText = GameObject.Find("SolveText");
            solveText.GetComponent<Text>().enabled = false;

            StartCoroutine(WinScene());
        }

        success = checkSuccess();
        if (!success)
        {
            finalPieceObject.transform.position = finalPiecePosition;
        }
        

    }

    public bool DisableTiles
    {
        get { return disable; }
        set { disable = value; }
    }

    IEnumerator WinScene()
    {
        // add 3 second delay
        yield return new WaitForSeconds(3f);

        // update background and board layer to the front
        background = GameObject.Find("Background");
        background.GetComponent<SpriteRenderer>().sortingLayerName = "WinBackground";

        board = GameObject.Find("board");
        board.GetComponent<SpriteRenderer>().sortingLayerName = "WinBoard";

        // display success comments
        successObject = GameObject.Find("Success");
        successObject.GetComponent<Text>().color = Color.yellow;

        // display complete image
        imageRend.sortingLayerName = "WinImage";

        // display continue button
        continueButton.GetComponent<Button>().enabled = true;
        continueButton.GetComponent<Image>().enabled = true;
        continueText.GetComponent<Text>().enabled = true;
    }


    public void showFinalInstructions()
    {
        finalInstructionsObject.GetComponent<Text>().color = Color.yellow;
        finalInstructionsRend.sortingLayerName = "Numbers";
    }

    public void hideFinalInstructions()
    {
        finalInstructionsObject.GetComponent<Text>().color = finalInstructionsColor;
        finalInstructionsRend.sortingLayerName = "Hidden";
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

        if (yDiff == 1.9F)
        {
            validMove = true;
        }

        // check if current tile and blank tile in same row
        if (validMove == false && tilePosition.y == blankPosition.y)
        {
            xDiff = Math.Abs(tilePosition.x - blankPosition.x);
            //Debug.Log(xDiff);
        }

        if (xDiff == 2.75F)
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