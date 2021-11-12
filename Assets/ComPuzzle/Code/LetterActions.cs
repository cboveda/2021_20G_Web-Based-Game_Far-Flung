using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterActions : MonoBehaviour
{

    Text letterText;
    GameObject textObject;
    string buttonName = "";
    string letter = "None";
    string button = "None";
    bool mouseDown = false;
    bool finalOn = false;
    SpriteRenderer rend;
    string spriteName = "";

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       // spriteName = rend.sprite.name;
       // if (spriteName == "pointer" )
       // {
        //    Debug.Log(spriteName);
        //}
        
    }

    public void OnMouseDown()
    {
        mouseDown = true;
    }

    public void OnMouseUp()
    {
        mouseDown = false;
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

    public void SwapLetter()
    {
        Debug.Log("here");
        Debug.Log(mouseDown);
        if (mouseDown)
        {
            Debug.Log("down");
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    public void SwapLetters()
    {
        


        //Debug.Log("swap");
        int textChild = 0;
        string prevLetter = "";
        string prevButton = "";
        string letterSelected = "";

        buttonName = EventSystem.current.currentSelectedGameObject.name;
        textObject = GameObject.Find(buttonName);

        prevLetter = FindObjectOfType<LetterActions>().selectedLetter;
        prevButton = FindObjectOfType<LetterActions>().selectedButton;

        Debug.Log(prevLetter);
        Debug.Log(prevButton);

        letterSelected = textObject.transform.GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
        if (prevLetter == "None")
        {            
            FindObjectOfType<LetterActions>().selectedLetter = letterSelected;
            FindObjectOfType<LetterActions>().selectedButton = buttonName;
            //Debug.Log(FindObjectOfType<LetterActions>().selectedLetter);
            //Debug.Log(FindObjectOfType<LetterActions>().selectedButton);
        }
        else
        {
            // swap letter with previous letter selected
            textObject.transform.GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = prevLetter;
            EventSystem.current.SetSelectedGameObject(null);

            // swap previous letter with current letter selected
            textObject = GameObject.Find(prevButton);
            textObject.transform.GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = letterSelected;

            // update previous selected letter and button to None
            FindObjectOfType<LetterActions>().selectedLetter = "None";
            FindObjectOfType<LetterActions>().selectedButton = "None";
        }

         


        //letterText = textObject.transform.GetChild(textChild).GetComponent<UnityEngine.UI.Text>();
        //FindObjectOfType<LetterActions>().selectedLetter = letterText.text;
        //FindObjectOfType<LetterActions>().selectedButton = buttonName;
        

        //Debug.Log(FindObjectOfType<LetterActions>().selectedLetter);




        //FindObjectOfType<LetterActions>().selected = buttonName;

        //Debug.Log(FindObjectOfType<LetterActions>().selected);

        //textObject = GameObject.Find(buttonName);
        //letterText = textObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        //letterText.text = "B";


        //textObject = GameObject.Find("1_Text");
        //letterText = textObject.GetComponent<UnityEngine.UI.Text>();
        //letterText.text = "B";
        //letterText<UnityEngine.UI.Text>().text = "B";
    }


}
