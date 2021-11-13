using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterActionsTest : MonoBehaviour
{

    Text letterText;
    GameObject textObject;
    Toggle buttonObject;
    ColorBlock toggleColor;
    string buttonName = "";
    string letter = "None";
    string button = "None";
    string row = "None";
    bool switched = false;
    int switchCount = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public string selectedLetter
    {
        get { return letter; }
        set { letter = value; }
    }

    public string selectedButton
    {
        get { return button; }
        set { button = value; }
    }

    public string selectedRow
    {
        get { return row; }
        set { row = value; }
    }

    public bool switchedLetters
    {
        get { return switched; }
        set { switched = value; }
    }

    public int switchedCount
    {
        get { return switchCount; }
        set { switchCount = value; }
    }


    public void SwapLetters()
    {
        int textChild = 0;
        int backgroundChild = 0;
        string prevLetter = "";
        string prevButton = "";
        string prevRow = "";
        string letterSelected = "";
        string rowSelected = "";
        
        
        switched = FindObjectOfType<LetterActions>().switchedLetters;
        Debug.Log(switched);

        if (switched)
        {
            switchCount = FindObjectOfType<LetterActions>().switchedCount;
            if (switchCount < 1)
            {
                //Debug.Log("switched");
                FindObjectOfType<LetterActions>().switchedCount += 1;
            }
            else
            {
                FindObjectOfType<LetterActions>().switchedLetters = false;
                FindObjectOfType<LetterActions>().switchedCount = 0;
            }         
            return;
        }


        buttonName = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log(buttonName);

        textObject = GameObject.Find(buttonName);

        buttonObject = textObject.GetComponent<Toggle>();

        if (buttonObject.isOn)
        {
            //Debug.Log("on");
            textObject.transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        }
        else
        {
            textObject.transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);
        }

        prevLetter = FindObjectOfType<LetterActions>().selectedLetter;
        prevButton = FindObjectOfType<LetterActions>().selectedButton;
        prevRow = FindObjectOfType<LetterActions>().selectedRow;

        //Debug.Log(buttonNumber);
        //Debug.Log(prevLetter);
        //Debug.Log(prevButton);
        //Debug.Log(prevRow);
        //Debug.Log(rowSelected);

        rowSelected = getSelectedRow(buttonName);
        letterSelected = textObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
        if (prevLetter == "None")
        {
            FindObjectOfType<LetterActions>().selectedLetter = letterSelected;
            FindObjectOfType<LetterActions>().selectedButton = buttonName;
            FindObjectOfType<LetterActions>().selectedRow = rowSelected;

            //Debug.Log(FindObjectOfType<LetterActions>().selectedLetter);
            //Debug.Log(FindObjectOfType<LetterActions>().selectedButton);
        }
        else
        {
            if (prevRow == rowSelected)
            {

                // swap letter with previous letter selected
                textObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = prevLetter;
                //EventSystem.current.SetSelectedGameObject(null);

                // swap previous letter with current letter selected
                textObject = GameObject.Find(prevButton);
                textObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = letterSelected;
            }
            else
            {
                Debug.Log("not correct row");

            }

            //Debug.Log(prevButton);
            //Debug.Log(buttonName);

            FindObjectOfType<LetterActions>().selectedLetter = "None";
            FindObjectOfType<LetterActions>().selectedButton = "None";
            FindObjectOfType<LetterActions>().selectedRow = "None";

            FindObjectOfType<LetterActions>().switchedLetters = true;
            GameObject.Find(prevButton).GetComponent<Toggle>().isOn = false;
            GameObject.Find(prevButton).transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);
            GameObject.Find(buttonName).GetComponent<Toggle>().isOn = false;
            GameObject.Find(buttonName).transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);

        }

    }



    public void SwapLetters1()
    {
        int textChild = 0;
        string prevLetter = "";
        string prevButton = "";
        string prevRow = "";
        string letterSelected = "";
        string rowSelected = "";

        


        buttonName = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log(buttonName);
        textObject = GameObject.Find(buttonName);

        //buttonObject = textObject.GetComponent<Toggle>();
        //toggleColor = buttonObject.colors;
        //Debug.Log(toggleColor);

        Debug.Log(textObject.transform.GetChild(0).GetComponent<Image>().color);

        if (buttonObject.isOn)
        {
            Debug.Log("on");
            textObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
            //buttonObject.isOn = false;
        }
        else
        {
            Debug.Log("off");
            textObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0.85f, 1.0f, 1.0f, 1.0f);
        }

        //buttonObject.Select();

        prevLetter = FindObjectOfType<LetterActions>().selectedLetter;
        prevButton = FindObjectOfType<LetterActions>().selectedButton;
        prevRow = FindObjectOfType<LetterActions>().selectedRow;

        //Debug.Log(rowSelected);
        //Debug.Log(buttonNumber);
        //Debug.Log(prevLetter);
        //Debug.Log(prevButton);



        rowSelected = getSelectedRow(buttonName);
        letterSelected = textObject.transform.GetChild(textChild).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
        if (prevLetter == "None")
        {
            FindObjectOfType<LetterActions>().selectedLetter = letterSelected;
            FindObjectOfType<LetterActions>().selectedButton = buttonName;
            FindObjectOfType<LetterActions>().selectedRow = rowSelected;

            //Debug.Log(FindObjectOfType<LetterActions>().selectedLetter);
            //Debug.Log(FindObjectOfType<LetterActions>().selectedButton);
        }
        else
        {

            Debug.Log(prevRow);
            Debug.Log(rowSelected);
            if (prevRow == rowSelected)
            {

                // swap letter with previous letter selected
                textObject.transform.GetChild(textChild).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = prevLetter;
                //EventSystem.current.SetSelectedGameObject(null);

                // swap previous letter with current letter selected
                textObject = GameObject.Find(prevButton);
                textObject.transform.GetChild(textChild).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = letterSelected;


            }
            else
            {
                Debug.Log("not correct row");

            }

            //EventSystem.current.SetSelectedGameObject(null);
            // update previous selected letter and button to None
            FindObjectOfType<LetterActions>().selectedLetter = "None";
            FindObjectOfType<LetterActions>().selectedButton = "None";
            FindObjectOfType<LetterActions>().selectedRow = "None";

            textObject = GameObject.Find(prevButton);
            textObject.GetComponent<Toggle>().isOn = false;
            //textObject = GameObject.Find(buttonName);
            //textObject.GetComponent<Toggle>().isOn = false;
        }


    }

    public string getSelectedRow(string button)
    {

        string dash = "_";
        string buttonIndex1 = "";
        string buttonIndex = "";
        int buttonNumber = 0;
        string row = "";

        buttonIndex1 = buttonName.Substring(1, 1);
        if (buttonIndex1 == dash)
        {
            buttonIndex = buttonName.Substring(0, 1);
            buttonNumber = int.Parse(buttonIndex);
        }
        else
        {
            buttonIndex = buttonName.Substring(0, 2);
            buttonNumber = int.Parse(buttonIndex);
        }

        if (buttonNumber < 8)
        {
            FindObjectOfType<LetterActions>().selectedRow = "1";
        }
        else if (buttonNumber < 19)
        {
            FindObjectOfType<LetterActions>().selectedRow = "2";
        }
        else if (buttonNumber < 27)
        {
            FindObjectOfType<LetterActions>().selectedRow = "3";
        }
        else
        {
            FindObjectOfType<LetterActions>().selectedRow = "4";
        }

        row = FindObjectOfType<LetterActions>().selectedRow;

        return row;
    }



}
