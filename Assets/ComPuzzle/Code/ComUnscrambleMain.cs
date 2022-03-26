using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class ComUnscrambleMain : MonoBehaviour
{

    int start = 0;
    bool word1 = false;
    bool word2 = false;
    bool word3 = false;
    bool word4 = false;
    bool wordsAll = false;

    bool word1Color = false;
    bool word2Color = false;
    bool word3Color = false;
    bool word4Color = false;

    bool wordFinal = false;
    bool wordFinalColor = false;

    GameObject continueButton;
    GameObject continueText;
    GameObject successObject;

    AudioSource audioSource;
    GameObject volumeSlider;
    bool showingScore = false;
    bool showingGameScore = false;


    public bool word1ColorUpdated
    {
        get { return word1Color; }
        set { word1Color = value; }
    }

    public bool word2ColorUpdated
    {
        get { return word2Color; }
        set { word2Color = value; }
    }

    public bool word3ColorUpdated
    {
        get { return word3Color; }
        set { word3Color = value; }
    }

    public bool word4ColorUpdated
    {
        get { return word4Color; }
        set { word4Color = value; }
    }

    public bool wordFinalColorUpdated
    {
        get { return wordFinalColor; }
        set { wordFinalColor = value; }
    }


    public int buttonStart
    {
        get { return start; }
        set { start = value; }
    }

    public bool word1win
    {
        get { return word1; }
        set { word1 = value; }
    }

    public bool word2win
    {
        get { return word2; }
        set { word2 = value; }
    }

    public bool word3win
    {
        get { return word3; }
        set { word3 = value; }
    }

    public bool word4win
    {
        get { return word4; }
        set { word4 = value; }
    }

    public bool wordsAllWin
    {
        get { return wordsAll; }
        set { wordsAll = value; }
    }

    public bool wordFinalWin
    {
        get { return wordFinal; }
        set { wordFinal = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        HideLetters();
        DisableAllWords();

        HideContinueButton();

        //UpdateFinalWord();

        audioSource = GetComponent<AudioSource>();
        volumeSlider = GameObject.Find("VolumeSlider");
        audioSource.Play();

        // start score of 0
        //Debug.Log("unscramble");
        Scoring.Instance.initialize(0, "");
        

    }


    public void HideContinueButton()
    {
        
        continueButton = GameObject.Find("Continue");

        continueButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
        continueButton.GetComponent<UnityEngine.UI.Image>().enabled = false;

        continueText = GameObject.Find("ContinueText");
        continueText.GetComponent<UnityEngine.UI.Text>().enabled = false;
    }

    public void EnableContinueButton()
    {

        continueButton = GameObject.Find("Continue");
       
        continueButton.GetComponent<UnityEngine.UI.Button>().enabled = true;
        continueButton.GetComponent<UnityEngine.UI.Image>().enabled = true;

        continueText = GameObject.Find("ContinueText");
        continueText.GetComponent<UnityEngine.UI.Text>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        bool winWord1 = false;
        bool winWord2 = false;
        bool winWord3 = false;
        bool winWord4 = false;
        bool winWordFinal = false;
        bool winWordsAll = false;
        int row1 = 1;
        int row2 = 2;
        int row3 = 3;
        int row4 = 4;
        int row5 = 5;

        audioSource.volume = volumeSlider.GetComponent<Slider>().value;

        winWordsAll = FindObjectOfType<ComUnscrambleMain>().wordsAllWin;
        if (!winWordsAll)
        {
            //Debug.Log(checkWinAllWords());
            checkWinAllWords();
        }

        winWord1 = FindObjectOfType<ComUnscrambleMain>().word1win;        
        if (!winWord1)
        {
            //Debug.Log("winword1");
            //Debug.Log(checkWordWin(row1));
            checkWordWin(row1);
        }

        winWord2 = FindObjectOfType<ComUnscrambleMain>().word2win;
        if (!winWord2)
        {
            //Debug.Log(checkWordWin(row2));
            checkWordWin(row2);
        }

        winWord3 = FindObjectOfType<ComUnscrambleMain>().word3win;
        if (!winWord3)
        {
            //Debug.Log(checkWordWin(row3));
            checkWordWin(row3);
        }

        winWord4 = FindObjectOfType<ComUnscrambleMain>().word4win;
        if (!winWord4)
        {
            //Debug.Log(checkWordWin(row4));
            checkWordWin(row4);
        }

        winWordFinal = FindObjectOfType<ComUnscrambleMain>().wordFinalWin;
        if (!winWordFinal)
        {
            //Debug.Log(checkWordWin(row4));
            checkWordWin(row5);
        }

    }


    public void HideLetters()
    {
        //Debug.Log("hide");
        Color hiddenColor = new Color32(246, 34, 250, 0);
        
        int backgroundChild = 0;
        int textChild = 0;
        int buttonLastIndex = 49;

        GameObject buttons = GameObject.Find("ButtonCanvas");

        for (int buttonIndex = 9; buttonIndex < buttonLastIndex; buttonIndex++)
        {
            buttons.transform.GetChild(buttonIndex).GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = hiddenColor;

        }
    }

    public void HideWord(int row)
    {
        //Debug.Log("hide word");
        int backgroundChild = 0;
        int textChild = 0;       
        int letterCount = 0;
        int buttonNumber = 0;     
        GameObject letterObject;
        Color hiddenColor = new Color32(246, 34, 250, 0);


        if (row == 1)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 1;
            letterCount = 7;
        }
        if (row == 2)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 8;
            letterCount = 11;
        }
        if (row == 3)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 19;
            letterCount = 8;
        }
        if (row == 4)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 27;
            letterCount = 7;
        }

        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {

            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");

            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = hiddenColor;

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }
    }


    public bool checkWinAllWords()
    {
        bool win = true;
        int wordRow = 0;
        int finalWordRow = 5;

        int wordCount = 4;
        for (int word = 0; word < wordCount; word++)
        {
            wordRow = word + 1;
            win = checkWordWin(wordRow);
            if (win == false)
            {
                return win;
            }
        }

        FindObjectOfType<ComUnscrambleMain>().wordsAllWin = true;

        UpdateFinalWord();

        EnableWord(finalWordRow);

        GameObject hint5 = GameObject.Find("Hint5");
        hint5.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;

        return win;

    }

    public void UpdateFinalWord()
    {
        //Debug.Log("update final");

        int backgroundChild = 0;
        int textChild = 0;
        int buttonNumber = 0;
        GameObject letterObject; 
        int letterCount = 7;
        Color winColorBackground = new Color32(0, 255, 255, 13);
        
        Color winLetterText = new Color32(250, 35, 250, 255);
        FindObjectOfType<ComUnscrambleMain>().buttonStart = 34;
        GameObject letterbox;

        letterbox = GameObject.Find("letterbox5");
        letterbox.GetComponent<SpriteRenderer>().sortingLayerName = "Puzzle";



        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {

            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");
            //Debug.Log(buttonNumber.ToString() + "_Button");
            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winLetterText;
            letterObject.transform.GetChild(backgroundChild).GetComponent<UnityEngine.UI.Image>().color = winColorBackground;

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }


    }


    public bool checkWordWin(int row)
    {
        int backgroundChild = 0;
        int textChild = 0;
        //int buttonStart = 0;
        int letterCount = 0;
        int buttonNumber = 0;
        bool wordWin = true;
        string wordLetter = "";
        GameObject letterObject;
        int wordRow = 0;
        string winLetter = "";
        bool wordUpdated = false;

        GameObject hint1 = GameObject.Find("Hint1");
        GameObject hint2 = GameObject.Find("Hint2");
        GameObject hint3 = GameObject.Find("Hint3");
        GameObject hint4 = GameObject.Find("Hint4");
        GameObject hint5 = GameObject.Find("Hint5");



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

            if (wordLetter != winLetter )
            {
                wordWin = false;
                if (wordWin == false)
                {
                    return wordWin;
                }
            }

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }

        switch (row)
        {
            case 1:
                FindObjectOfType<ComUnscrambleMain>().word1win = true;
                bool win = FindObjectOfType<ComUnscrambleMain>().word1win;
                wordUpdated = FindObjectOfType<ComUnscrambleMain>().word1ColorUpdated;
                //Debug.Log("wordWin");
                if (!wordUpdated)
                {
                    UpdateWinColor(row);
                    DisableWord(row);
                    FindObjectOfType<ComUnscrambleMain>().word1ColorUpdated = true;
                    Scoring.Instance.addToScore(250, "ComObjective8");
                    hint1.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
                break;
            case 2:
                FindObjectOfType<ComUnscrambleMain>().word2win = true;
                wordUpdated = FindObjectOfType<ComUnscrambleMain>().word2ColorUpdated;
                if (!wordUpdated)
                {
                    UpdateWinColor(row);
                    DisableWord(row);
                    FindObjectOfType<ComUnscrambleMain>().word2ColorUpdated = true;
                    Scoring.Instance.addToScore(250, "ComObjective9");
                    hint2.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }                
                break;
            case 3:
                FindObjectOfType<ComUnscrambleMain>().word3win = true;
                wordUpdated = FindObjectOfType<ComUnscrambleMain>().word3ColorUpdated;
                if (!wordUpdated)
                {
                    UpdateWinColor(row);
                    DisableWord(row);
                    FindObjectOfType<ComUnscrambleMain>().word3ColorUpdated = true;
                    Scoring.Instance.addToScore(250, "ComObjective10");
                    hint3.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }                
                break;
            case 4:
                FindObjectOfType<ComUnscrambleMain>().word4win = true;
                wordUpdated = FindObjectOfType<ComUnscrambleMain>().word4ColorUpdated;
                if (!wordUpdated)
                {
                    UpdateWinColor(row);
                    DisableWord(row);
                    FindObjectOfType<ComUnscrambleMain>().word4ColorUpdated = true;
                    Scoring.Instance.addToScore(250, "ComObjective11");
                    hint4.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
                }                
                break;
            case 5:
                FindObjectOfType<ComUnscrambleMain>().wordFinalWin = true;
                wordUpdated = FindObjectOfType<ComUnscrambleMain>().wordFinalColorUpdated;
                if (!wordUpdated)
                {
                    UpdateWinColor(row);
                    DisableWord(row);
                    FindObjectOfType<ComUnscrambleMain>().wordFinalColorUpdated = true;
                    FindObjectOfType<SendDataActions>().DisableDataButtons();
                    Scoring.Instance.addToScore(500, "ComObjective12");
                    hint5.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    StartCoroutine(WinScene());
                }                
                break;
        }

        

        return wordWin;

    }

    IEnumerator WinScene()
    {

        // display success comments
        Color letterColor = new Color32(249, 160, 0, 255);
        successObject = GameObject.Find("Success");
        successObject.GetComponent<UnityEngine.UI.Text>().color = letterColor;

        // add 3 second delay
        yield return new WaitForSeconds(3f);
        //Debug.Log("win scene");

        
        EnableContinueButton();



    }

    public void UpdateWinColor(int row)
    {

        int backgroundChild = 0;
        int textChild = 0;
        int letterCount = 0;
        int buttonNumber = 0;
        GameObject letterObject;

        Color winColorBackground = new Color32(0, 90, 0, 255);
        Color winColorText = new Color32(0, 0, 0, 255);
        

        if (row == 1)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 1;
            letterCount = 7;            
        }
        if (row == 2)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 8;
            letterCount = 11;            
        }
        if (row == 3)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 19;
            letterCount = 8;            
        }
        if (row == 4)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 27;
            letterCount = 7;            
        }
        if (row == 5)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 34;
            letterCount = 7;
        }

        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {

            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");
            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winColorText;
            letterObject.transform.GetChild(backgroundChild).GetComponent<UnityEngine.UI.Image>().color = winColorBackground;

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }

        HighlightLetters(row);

    }

    public void HighlightLetters(int row)
    {
        string[] specialLetterButtons = { };
        int backgroundChild = 0;
        int textChild = 0;
        GameObject letterObject;
        //Color winLetterText = new Color32(75, 255, 35, 255);
        Color winLetterText = new Color32(250, 35, 250, 255);

        specialLetterButtons = FindObjectOfType<ComGameData>().getSpecialLetters(row);
        foreach (string button in specialLetterButtons)
        {
            //Debug.Log(button);
            letterObject = GameObject.Find(button.ToString() + "_Button");
            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winLetterText;
        }

    }


    public void DisableAllWords()
    {
        int wordCount = 5;
        int wordRow = 0;

        for (int word = 0; word < wordCount; word++)
        {
            wordRow += 1;
            DisableWord(wordRow);
        }
    }

    public void DisableWord(int row)
    {
        //Debug.Log("disable word");
        int letterCount = 0;
        int buttonNumber = 0;
        GameObject letterObject;

        if (row == 1)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 1;
            letterCount = 7;
        }

        if (row == 2)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 8;
            letterCount = 11;
        }

        if (row == 3)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 19;
            letterCount = 8;
        }

        if (row == 4)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 27;
            letterCount = 7;
        }

        if (row == 5)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 34;
            letterCount = 7;
        }

        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {
           
            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");
            //Debug.Log(buttonNumber.ToString() + "_Button");
            letterObject.GetComponent<UnityEngine.UI.Toggle>().interactable = false;

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }

    }

    public void EnableWord(int row)
    {
        //Debug.Log("enable word");

        int letterCount = 0;
        int buttonNumber = 0;
        GameObject letterObject;

        if (row == 1)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 1;
            letterCount = 7;
        }

        if (row == 2)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 8;
            letterCount = 11;
        }

        if (row == 3)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 19;
            letterCount = 8;
        }

        if (row == 4)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 27;
            letterCount = 7;
        }

        if (row == 5)
        {
            FindObjectOfType<ComUnscrambleMain>().buttonStart = 34;
            letterCount = 7;
        }

        for (int letterPos = 0; letterPos < letterCount; letterPos++)
        {

            buttonNumber = FindObjectOfType<ComUnscrambleMain>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");

            letterObject.GetComponent<UnityEngine.UI.Toggle>().interactable = true;

            buttonNumber++;
            FindObjectOfType<ComUnscrambleMain>().buttonStart = buttonNumber;
        }
    }

    public void OnMouseEnter()
    {
        showingScore = FindObjectOfType<Scoring>().getShowingScore;
        showingGameScore = FindObjectOfType<Scoring>().getShowingGameScore;
        wordsAll = FindObjectOfType<ComUnscrambleMain>().wordsAllWin;
        if (showingScore || showingGameScore || wordsAll)
        {
            return;
        }

            SpriteRenderer rend;
        string objectName = "";
        GameObject instructions;
        Color highlightedColor = new Color32(89, 38, 81, 255);
        Color letterColor = new Color32(249, 160, 0, 255);

        rend = GetComponent<SpriteRenderer>();
        objectName = rend.transform.name;
        //Debug.Log(objectName);

        if (objectName == "InstructionsBox")
        {            

            instructions = GameObject.Find("InstructionsText");
            instructions.GetComponent<UnityEngine.UI.Text>().color = letterColor;
                        
            rend.color = highlightedColor;

        }


    }

    public void OnMouseExit()
    {
        SpriteRenderer rend;
        string objectName = "";
        GameObject instructions;
        Color hiddenColor = new Color32(255, 255, 255, 0);
        Color currentColor = new Color32(48 , 33, 68, 255);

        rend = GetComponent<SpriteRenderer>();
        objectName = rend.transform.name;
        //Debug.Log(objectName);

        if (objectName == "InstructionsBox")
        {
            instructions = GameObject.Find("InstructionsText");
            instructions.GetComponent<UnityEngine.UI.Text>().color = hiddenColor;

            rend.color = currentColor;

        }


    }
}