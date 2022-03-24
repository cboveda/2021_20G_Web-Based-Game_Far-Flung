using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterActions : MonoBehaviour
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
    AudioSource audioSource;
    GameObject volumeSlider;
    bool showingScore = false;
    bool showingGameScore = false;
    GameObject hint1;
    GameObject hint2;
    GameObject hint3;
    GameObject hint4;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumeSlider = GameObject.Find("VolumeSlider");

        hint1 = GameObject.Find("Hint1");
        hint2 = GameObject.Find("Hint2");
        hint3 = GameObject.Find("Hint3");
        hint4 = GameObject.Find("Hint4");
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeSlider.GetComponent<Slider>().value;
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

    public void SendHint()
    {
        //Debug.Log("Send hint");

        int letterCount = 0;
        int row = 0;
        int wordRow = 0;
        int buttonNumber = 0;
        GameObject letterObject;
        string wordLetter = "";
        string winLetter = "";
        int backgroundChild = 0;
        int textChild = 0;


        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "Hint1")
        {
            row = 1;
            hint1.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (buttonName == "Hint2")
        {
            row = 2;
            hint2.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (buttonName == "Hint3")
        {
            row = 3;
            hint3.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (buttonName == "Hint4")
        {
            row = 4;
            hint4.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (row == 1)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 1;
            letterCount = 7;
            wordRow = 1;
        }
        if (row == 2)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 8;
            letterCount = 11;
            wordRow = 2;
        }
        if (row == 3)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 19;
            letterCount = 8;
            wordRow = 3;
        }
        if (row == 4)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 27;
            letterCount = 7;
            wordRow = 4;
        }

        if (row == 5)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 34;
            letterCount = 7;
            wordRow = 5;
        }

        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {

            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");
            wordLetter = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
            winLetter = FindObjectOfType<ComGameData>().getWinLetter(wordRow, letterPos);

            //Debug.Log(wordLetter);
            //Debug.Log(winLetter);



            if (wordLetter != winLetter)
            {
                StartCoroutine(ShowLetter(winLetter, wordLetter, wordRow, letterObject));
                return;
            }


            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }

    }

    IEnumerator ShowLetter(string winLetter, string wordLetter, int wordRow, GameObject letterObject)
    {

        //Debug.Log("show letter");
        int backgroundChild = 0;
        int textChild = 0;
        Color originalColor = new Color32(246, 34, 250, 255);

        letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = winLetter;
        letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = wordLetter;
        letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = originalColor;
        
        hint1.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;
        hint2.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;
        hint3.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;
        hint4.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;

    }

  


    public void SwapLetters()
    {
        showingScore = FindObjectOfType<Scoring>().getShowingScore;
        showingGameScore = FindObjectOfType<Scoring>().getShowingGameScore;
        if (showingScore || showingGameScore)
        {
            return;
        }

        int textChild = 0;
        int backgroundChild = 0;
        string prevLetter = "";
        string prevButton = "";
        string prevRow = "";
        string letterSelected = "";
        string rowSelected = "";


        switched = FindObjectOfType<LetterActions>().switchedLetters;
        //Debug.Log(switched);

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
        textObject = GameObject.Find(buttonName);
        buttonObject = textObject.GetComponent<Toggle>();

        if (buttonObject.isOn)
        {
            textObject.transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        }
        else
        {
            textObject.transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);
        }
        
        prevLetter = FindObjectOfType<LetterActions>().selectedLetter;
        prevButton = FindObjectOfType<LetterActions>().selectedButton;
        prevRow = FindObjectOfType<LetterActions>().selectedRow;

        rowSelected = getSelectedRow(buttonName);
        letterSelected = textObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
        if (prevLetter == "None")
        {            
            FindObjectOfType<LetterActions>().selectedLetter = letterSelected;
            FindObjectOfType<LetterActions>().selectedButton = buttonName;
            FindObjectOfType<LetterActions>().selectedRow = rowSelected;

        }
        else
        {            
            if (prevRow == rowSelected)
            {
                if (prevButton != buttonName)
                {
                    audioSource.Play();
                }

                Scoring.Instance.addToScore(-2, "ComObjective7");

                //Debug.Log("swap");
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

            FindObjectOfType<LetterActions>().selectedLetter = "None";
            FindObjectOfType<LetterActions>().selectedButton = "None";
            FindObjectOfType<LetterActions>().selectedRow = "None";

            if (prevButton != buttonName)
            {
                FindObjectOfType<LetterActions>().switchedLetters = true;
            }                
            
            GameObject.Find(prevButton).GetComponent<Toggle>().isOn = false;
            GameObject.Find(prevButton).transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);
            GameObject.Find(buttonName).GetComponent<Toggle>().isOn = false;
            GameObject.Find(buttonName).transform.GetChild(backgroundChild).GetComponent<Image>().color = new Color(0.0f, 1.0f, 1.0f, 0.02f);

        }

    }

    public string getSelectedRow(string button)
    {

        string dash = "_";
        string buttonIndex1 = "";
        string buttonIndex = "";
        int buttonNumber = 0;
        string row = "";

        buttonIndex1 = button.Substring(1, 1);
        if (buttonIndex1 == dash)
        {
            buttonIndex = button.Substring(0, 1);
            buttonNumber = int.Parse(buttonIndex);
        }
        else
        {
            buttonIndex = button.Substring(0, 2);
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
