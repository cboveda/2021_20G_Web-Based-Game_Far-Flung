using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComUnscrambleMain : MonoBehaviour
{

    int start = 0;
    bool word1 = false;
    bool word2 = false;
    bool word3 = false;
    bool word4 = false;
    bool wordsAll = false;

    // Start is called before the first frame update
    void Start()
    {
        HideLetters();
        
    }

    // Update is called once per frame
    void Update()
    {
        bool winWord1 = false;
        bool winWord2 = false;
        bool winWord3 = false;
        bool winWord4 = false;
        bool winWordsAll = false;
        int row1 = 1;
        int row2 = 2;
        int row3 = 3;
        int row4 = 4;               

        winWordsAll = FindObjectOfType<ComUnscrambleMain>().wordsAllWin;
        if (!winWordsAll)
        {
            Debug.Log(checkWinAllWords());
        }

        winWord1 = FindObjectOfType<ComUnscrambleMain>().word1win;
        if (!winWord1)
        {
            Debug.Log(checkWordWin(row1));
        }

        winWord2 = FindObjectOfType<ComUnscrambleMain>().word2win;
        if (!winWord2)
        {
            Debug.Log(checkWordWin(row2));
        }

        winWord3 = FindObjectOfType<ComUnscrambleMain>().word3win;
        if (!winWord3)
        {
            Debug.Log(checkWordWin(row3));
        }

        winWord4 = FindObjectOfType<ComUnscrambleMain>().word4win;
        if (!winWord4)
        {
            Debug.Log(checkWordWin(row4));
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

    public bool checkWinAllWords()
    {
        bool win = true;
        int wordRow = 0;

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
        return win;

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
                break;
            case 2:
                FindObjectOfType<ComUnscrambleMain>().word2win = true;
                break;
            case 3:
                FindObjectOfType<ComUnscrambleMain>().word3win = true;
                break;
            case 4:
                FindObjectOfType<ComUnscrambleMain>().word4win = true;
                break;
        }

        UpdateWinColor(row);

        return wordWin;

    }

    public void UpdateWinColor(int row)
    {
        Debug.Log("update win color");

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
        Color winLetterText = new Color32(75, 255, 35, 255);

        specialLetterButtons = FindObjectOfType<ComGameData>().getSpecialLetters(row);
        foreach (string button in specialLetterButtons)
        {
            //Debug.Log(button);
            letterObject = GameObject.Find(button.ToString() + "_Button");
            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winLetterText;
        }

    }

}