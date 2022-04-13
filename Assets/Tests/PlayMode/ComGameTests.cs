using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class ComGameTests
{

    [UnityTest]
    public IEnumerator TestSceneSwitching()
    {
        // Verify scenes are switching correctly.

        Dictionary<string, string> testScenes = new Dictionary<string, string>()
        {
              {"comGame", "comGameWin"},
              {"comGameWin", "comUnscrambleInfo"},
              {"comUnscrambleInfo", "comUnscrambleIntro"},
              {"comUnscrambleIntro", "comUnscramble"}
              //{"comUnscrambleWin", "Hub"}
        };

        GameObject ob = new GameObject();
        ob.AddComponent<SceneActions>();
        SceneActions sceneObject = GameObject.FindObjectOfType<SceneActions>();

        string sceneName = "";
        string nextScene = "";
        Scene scene;
        foreach (KeyValuePair<string, string> testScene in testScenes)
        {
            // set the test scene
            SceneManager.LoadScene(testScene.Key);
            yield return new WaitForSeconds(0.2f); // delay to load

            // verify the test scene was loaded
            scene = SceneManager.GetActiveScene();
            yield return new WaitForSeconds(0.2f); // delay to get active scene
            sceneName = scene.name;
            Assert.AreEqual(testScene.Key, sceneName);

            // switch to the next scene
            sceneObject.StartGameScene();
            yield return new WaitForSeconds(0.2f); // delay to switch scene

            // verify the next scene was loaded
            scene = SceneManager.GetActiveScene();
            yield return new WaitForSeconds(0.2f); // delay to get active scene
            nextScene = scene.name;
            Assert.AreEqual(testScene.Value, nextScene);

        }
    }

    [UnityTest]
    public IEnumerator TestSuccessConditions()
    {
        // Verify tiles start in the correct positions and solve puzzle results in a success.

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable tile object to use
        GameObject scriptObject = GameObject.Find("11");

        // confirm default tile positions is not a success
        VerifySuccess(scriptObject, false);

        GameObject mode = new GameObject();
        mode.AddComponent<AudioSource>();
        mode.AddComponent<ComGameModes>();
        ComGameModes modeObject = GameObject.FindObjectOfType<ComGameModes>();

        // solve puzzle
        modeObject.SolvePuzzle();
        yield return new WaitForSeconds(0.2f); // delay to solve

        VerifySuccess(scriptObject, true);

        GameObject main = new GameObject();
        main.AddComponent<ComGameMain>();
        main.AddComponent<AudioSource>();
        ComGameMain mainObject = GameObject.FindObjectOfType<ComGameMain>();

        // set the tiles back to start positions 
        mainObject.SetTileStartPositions();
        yield return new WaitForSeconds(0.2f); // delay to reset pieces

        VerifySuccess(scriptObject, false);
    }

    public IEnumerator VerifySuccess(GameObject scriptObject, bool condition)
    {
        bool success = scriptObject.GetComponent<TileActions>().checkSuccess();
        yield return new WaitForSeconds(0.2f); // delay to check
        Assert.AreEqual(success, condition);
    }


    [UnityTest]
    public IEnumerator TestEasyMode()
    {
        // Verify easy mode displays numbers on tiles when on and hides numbers when off.

        // load scene with that has the easy mode button
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // verify easy mode is off
        GameObject easyModeToggle = GameObject.Find("EasyMode");
        Assert.AreEqual(easyModeToggle.GetComponent<Toggle>().isOn, false);

        // verify numbers on tiles are on Default layer
        string[] tileNumbers = { "n1", "n2", "n3", "n4", "n5", "n6", "n7", "n8", "n9", "n10", "n11", "n12" };
        GameObject tileNumberObject;
        foreach (string tileNumber in tileNumbers)
        {
            tileNumberObject = GameObject.Find(tileNumber);
            Assert.AreEqual(tileNumberObject.GetComponent<SpriteRenderer>().sortingLayerName, "Default");
        }

        // turn on easy mode and verify it is on
        easyModeToggle.GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(0.2f); // delay to load scene
        Assert.AreEqual(easyModeToggle.GetComponent<Toggle>().isOn, true);

        // verify numbers on tiles are on Numbers layer and verify n4 is on FinalPieceNumber layer 
        foreach (string tileNumber in tileNumbers)
        {
            tileNumberObject = GameObject.Find(tileNumber);
            if (tileNumber == "n4")
            {
                Assert.AreEqual(tileNumberObject.GetComponent<SpriteRenderer>().sortingLayerName, "FinalPieceNumber");
            }
            else
            {
                Assert.AreEqual(tileNumberObject.GetComponent<SpriteRenderer>().sortingLayerName, "Numbers");
            }
        }
    }

    [UnityTest]
    public IEnumerator TestTileActions()
    {
        // Verify methods for TileActions

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable tile object to use
        GameObject scriptObject = GameObject.Find("11");

        // get scriptable tile default color
        Color scriptObjectDefaultColor = scriptObject.GetComponent<SpriteRenderer>().color;

        // verify tile is highlighted yellow with mouse on
        scriptObject.GetComponent<TileActions>().OnMouseEnter();
        Assert.AreEqual(scriptObject.GetComponent<SpriteRenderer>().color, Color.yellow);

        // verify tile is not highlighted with mouse off
        scriptObject.GetComponent<TileActions>().OnMouseExit();
        Assert.AreEqual(scriptObject.GetComponent<SpriteRenderer>().color, scriptObjectDefaultColor);

        // get default color for show final instructions
        GameObject finalObject = GameObject.Find("FinalInstructionsText");
        Color finalObjectDefaultColor = finalObject.GetComponent<Text>().color;

        // verify color when selecting show final instructions
        Color letterColor = new Color32(249, 160, 0, 255);
        scriptObject.GetComponent<TileActions>().showFinalInstructions();
        Assert.AreEqual(finalObject.GetComponent<Text>().color, letterColor);

        // verify color is back to default when selecting hide final instructions
        scriptObject.GetComponent<TileActions>().hideFinalInstructions();
        Assert.AreEqual(finalObject.GetComponent<Text>().color, finalObjectDefaultColor);

        // verify tiles that have valid moves at default positions
        string[] validMoveTiles = { "13", "31", "32" };
        GameObject tileObject;
        foreach (string tile in validMoveTiles)
        {
            tileObject = GameObject.Find(tile);
            Assert.AreEqual(tileObject.GetComponent<TileActions>().checkValidMove(), true);
        }

        // verify tiles that do not have valid moves at default positions
        string[] invalidMoveTiles = { "11", "12", "14", "21", "22", "23", "24", "33", "34" };
        foreach (string tile in invalidMoveTiles)
        {
            tileObject = GameObject.Find(tile);
            Assert.AreEqual(tileObject.GetComponent<TileActions>().checkValidMove(), false);
        }
    }

    [UnityTest]
    public IEnumerator TestDataButtons()
    {
        // Verify data buttons can be enabled and disabled

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // verify data buttons are enabled at start
        GameObject dataButtonObject;
        string[] dataButtons = { "ImagerButton", "SpectometerButton", "MagnetometerButton", "RadioButton" };
        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            Assert.AreEqual(dataButtonObject.GetComponent<UnityEngine.UI.Button>().interactable, true);
        }

        GameObject ob = new GameObject();
        ob.AddComponent<SendDataActions>();
        ob.AddComponent<AudioSource>();
        SendDataActions sendButtonObject = GameObject.FindObjectOfType<SendDataActions>();

        // disable data buttons
        sendButtonObject.DisableDataButtons();

        // verify data buttons are disabled
        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            Assert.AreEqual(dataButtonObject.GetComponent<UnityEngine.UI.Button>().interactable, false);
        }

        // enable data buttons
        sendButtonObject.EnableDataButtons();

        // verify data buttons are enabled
        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            Assert.AreEqual(dataButtonObject.GetComponent<UnityEngine.UI.Button>().interactable, true);
        }
    }

    [UnityTest]
    public IEnumerator TestScrambledWords()
    {
        // verify scrambled words default properties at start

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene


        // verify all words are disabled at start
        int buttonLastIndex = 49;
        GameObject buttons = GameObject.Find("ButtonCanvas");
        for (int buttonIndex = 9; buttonIndex < buttonLastIndex; buttonIndex++)
        {
            Assert.AreEqual(buttons.transform.GetChild(buttonIndex).GetComponent<UnityEngine.UI.Toggle>().interactable, false);

        }

        GameObject ob = new GameObject();
        ob.AddComponent<ComUnscrambleMain>();
        ob.AddComponent<AudioSource>();
        ComUnscrambleMain unscrambleMainObject = GameObject.FindObjectOfType<ComUnscrambleMain>();

        // verify all words are scrambled at start
        int wordCount = 5;
        for (int word = 0; word < wordCount; word++)
        {
            int wordRow = word + 1;

            Assert.AreEqual(unscrambleMainObject.checkWordWin(wordRow), false);
        }

    }

    [UnityTest]
    public IEnumerator TestTileActionsButtons()
    {

        // verify instructions button and view image button

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get instructions object to use
        GameObject instructions = GameObject.Find("Instructions");

        // get default color
        int canvasChild = 0;
        int textChild = 1;
        Color instructionsDefaultColor = instructions.transform.GetChild(canvasChild).GetChild(textChild).GetComponent<Text>().color;

        // verify instructions are displayed when mouse on
        Color letterColor = new Color32(249, 160, 0, 255);
        instructions.GetComponent<TileActions>().OnMouseEnter();
        Assert.AreEqual(instructions.transform.GetChild(canvasChild).GetChild(textChild).GetComponent<Text>().color, letterColor);

        // verify instructions are hidden when mouse exit
        instructions.GetComponent<TileActions>().OnMouseExit();
        Assert.AreEqual(instructions.transform.GetChild(canvasChild).GetChild(textChild).GetComponent<Text>().color, instructionsDefaultColor);

        // get view image object to use
        GameObject viewImage = GameObject.Find("ViewImage");
        int imageChild = 1;

        // verify image is displayed when mouse on
        string displaySortingLayer = "ViewImage";
        viewImage.GetComponent<TileActions>().OnMouseEnter();
        Assert.AreEqual(viewImage.transform.GetChild(imageChild).GetComponent<SpriteRenderer>().sortingLayerName, displaySortingLayer);

        // verify image is hidden when mouse exit
        string hideSortingLayer = "Hidden";
        viewImage.GetComponent<TileActions>().OnMouseExit();
        Assert.AreEqual(viewImage.transform.GetChild(imageChild).GetComponent<SpriteRenderer>().sortingLayerName, hideSortingLayer);
    }

    [UnityTest]
    public IEnumerator TestLetterActions()
    {

        // verify rows and letter swapping

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable tile object to use
        GameObject button1 = GameObject.Find("1_Button");

        // test values
        Dictionary<string, string> testRows = new Dictionary<string, string>()
        {
              {"1_Button", "1"},
              {"10_Button", "2"},
              {"21_Button", "3"},
              {"32_Button", "4"},
        };

        // verify test values
        string row = "";
        foreach (KeyValuePair<string, string> testRow in testRows)
        {
            row = button1.GetComponent<LetterActions>().getSelectedRow(testRow.Key);
            Assert.AreEqual(row, testRow.Value);

        }

        // set previous row, letter, and button
        LetterActions.FindObjectOfType<LetterActions>().selectedRow = "1";
        LetterActions.FindObjectOfType<LetterActions>().selectedLetter = "E";
        LetterActions.FindObjectOfType<LetterActions>().selectedButton = "1_Button";

        // select button to swap        
        GameObject button2 = GameObject.Find("2_Button");
        EventSystem.current.SetSelectedGameObject(button2);

        // verify button 1 and button 2 is default
        int backgroundChild = 0;
        int textChild = 0;
        string button1letter = "E";
        string button2letter = "F";

        // swap button 1 and button 2
        button1.GetComponent<LetterActions>().SwapLetters();

        // verify button 1 and button 2 switch
        Assert.AreEqual(button1.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text, button2letter);
        Assert.AreEqual(button2.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text, button1letter);


    }

    [UnityTest]
    public IEnumerator TestSendData()
    {
        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        Color hiddenColor = new Color32(246, 34, 250, 0);
        int backgroundChild = 0;
        int textChild = 0;
        GameObject letterObject;
        GameObject dataButtonObject;

        // verify all letters are hidden
        int buttonCount = 33;
        for (int count = 1; count <= buttonCount; count++)
        {
            letterObject = GameObject.Find(count.ToString() + "_Button");
            Assert.AreEqual(letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<Text>().color, hiddenColor);
        }

        // send word data
        string[] dataButtons = { "ImagerButton", "SpectometerButton", "MagnetometerButton", "RadioButton" };
        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            EventSystem.current.SetSelectedGameObject(dataButtonObject);
            dataButtonObject.GetComponent<SendDataActions>().SendData();
            yield return new WaitForSeconds(10.0f); // delay for sending signal
        }

        // verify letters are displayed after sending signal
        Color letterColor = new Color32(246, 34, 250, 255);
        for (int count = 1; count <= buttonCount; count++)
        {
            letterObject = GameObject.Find(count.ToString() + "_Button");
            Assert.AreEqual(letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<Text>().color, letterColor);
        }

    }

    [UnityTest]
    public IEnumerator TestUnscrambleInfo()
    {
        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscrambleInfo");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable image object to use
        GameObject scriptObject = GameObject.Find("imager");

        // get an info object to use
        GameObject infoObject = GameObject.Find("imagerinfo");

        // verify image and info is highlighted and displayed
        Color highlightColor = new Color32(255, 255, 255, 255);
        String displaySortingLayer = "Board";
        scriptObject.GetComponent<ComUnscrambleInfo>().HighlightPicture("imager");
        scriptObject.GetComponent<ComUnscrambleInfo>().DisplayInfo("imager");
        Assert.AreEqual(scriptObject.GetComponent<SpriteRenderer>().color, highlightColor);
        Assert.AreEqual(infoObject.GetComponent<SpriteRenderer>().sortingLayerName, displaySortingLayer);

        // verify image and info is hidden and instructions are displayed
        Color highlightOff = new Color32(255, 255, 255, 150);
        String hiddenSortingLayer = "Hidden";
        scriptObject.GetComponent<ComUnscrambleInfo>().HideInfo("imager");
        GameObject instructions = GameObject.Find("InstructionsText");
        Assert.AreEqual(infoObject.GetComponent<SpriteRenderer>().sortingLayerName, hiddenSortingLayer);
        Assert.AreEqual(instructions.GetComponent<UnityEngine.UI.Text>().enabled, true);

    }

    [UnityTest]
    public IEnumerator TestUnscrambleContinue()
    {
        // verify scrambled words default properties at start

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // verify continue button is disabled start
        GameObject continueButton = GameObject.Find("Continue");
        GameObject continueText = GameObject.Find("ContinueText");

        Assert.AreEqual(continueButton.GetComponent<UnityEngine.UI.Button>().enabled, false);
        Assert.AreEqual(continueButton.GetComponent<UnityEngine.UI.Image>().enabled, false);
        Assert.AreEqual(continueText.GetComponent<UnityEngine.UI.Text>().enabled, false);

        GameObject ob = new GameObject();
        ob.AddComponent<ComUnscrambleMain>();
        ob.AddComponent<AudioSource>();
        ComUnscrambleMain unscrambleMainObject = GameObject.FindObjectOfType<ComUnscrambleMain>();

        // enable the continue button
        unscrambleMainObject.EnableContinueButton();

        // verify continue button is enabled
        Assert.AreEqual(continueButton.GetComponent<UnityEngine.UI.Button>().enabled, true);
        Assert.AreEqual(continueButton.GetComponent<UnityEngine.UI.Image>().enabled, true);
        Assert.AreEqual(continueText.GetComponent<UnityEngine.UI.Text>().enabled, true);


    }

    [UnityTest]
    public IEnumerator TestUnscrambleSpecialLetters()
    {
        // verify special letters are correct in win positions

        // load scene with the scriptable objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // test values
        Dictionary<string, string> testLetters = new Dictionary<string, string>()
        {
              {"6", "C"},
              {"9", "O"},
              {"12", "O"},
              {"18", "N"},
              {"19", "M"},
              {"23", "E"},
              {"32", "T"},
              {"34", "N"},
              {"35", "O"},
              {"36", "C"},
              {"37", "O"},
              {"38", "M"},
              {"39", "E"},
              {"40", "T"}

        };


        string[] specialLetters = { };
        string winLetter = "";
        string specialLetter = "";
        int offset = -1;

        ComGameData comGameData = GameObject.FindObjectOfType<ComGameData>();

        int rows = 5;
        for (int row = 1; row <= rows; row++)
        {
            specialLetters = comGameData.getSpecialLetters(row);
            foreach (string index in specialLetters)
            {
                if (row == 2)
                {
                    offset = -8;
                }
                if (row == 3)
                {
                    offset = -19;
                }
                if (row == 4)
                {
                    offset = -27;
                }
                if (row == 5)
                {
                    offset = -34;
                }

                specialLetter = testLetters[index];
                winLetter = comGameData.getWinLetter(row, int.Parse(index) + offset);
                Assert.AreEqual(specialLetter, winLetter);
            }
        }

    }


    [UnityTest]
    public IEnumerator TestUnscrambleMainGetterSetters()
    {
        // verify getters and setters

        // load scene with the scriptable objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        ComUnscrambleMain unscrambleMainObject = GameObject.FindObjectOfType<ComUnscrambleMain>();
        bool colorUpdated = false;
        bool wordWin = false;

        // verify color updated default
        colorUpdated = unscrambleMainObject.word1ColorUpdated;
        Assert.AreEqual(colorUpdated, false);
        // verify color updated change
        unscrambleMainObject.word1ColorUpdated = true;
        colorUpdated = unscrambleMainObject.word1ColorUpdated;
        Assert.AreEqual(colorUpdated, true);

        // verify color updated default
        colorUpdated = unscrambleMainObject.word2ColorUpdated;
        Assert.AreEqual(colorUpdated, false);
        // verify color updated change
        unscrambleMainObject.word2ColorUpdated = true;
        colorUpdated = unscrambleMainObject.word2ColorUpdated;
        Assert.AreEqual(colorUpdated, true);

        // verify color updated default
        colorUpdated = unscrambleMainObject.word3ColorUpdated;
        Assert.AreEqual(colorUpdated, false);
        // verify color updated change
        unscrambleMainObject.word3ColorUpdated = true;
        colorUpdated = unscrambleMainObject.word3ColorUpdated;
        Assert.AreEqual(colorUpdated, true);

        // verify color updated default
        colorUpdated = unscrambleMainObject.word4ColorUpdated;
        Assert.AreEqual(colorUpdated, false);
        // verify color updated change
        unscrambleMainObject.word4ColorUpdated = true;
        colorUpdated = unscrambleMainObject.word4ColorUpdated;
        Assert.AreEqual(colorUpdated, true);

        // verify color updated default
        colorUpdated = unscrambleMainObject.wordFinalColorUpdated;
        Assert.AreEqual(colorUpdated, false);
        // verify color updated change
        unscrambleMainObject.wordFinalColorUpdated = true;
        colorUpdated = unscrambleMainObject.wordFinalColorUpdated;
        Assert.AreEqual(colorUpdated, true);

        // verify word win default
        wordWin = unscrambleMainObject.word1win;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.word1win = true;
        wordWin = unscrambleMainObject.word1win;
        Assert.AreEqual(wordWin, true);

        // verify word win default
        wordWin = unscrambleMainObject.word2win;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.word2win = true;
        wordWin = unscrambleMainObject.word2win;
        Assert.AreEqual(wordWin, true);

        // verify word win default
        wordWin = unscrambleMainObject.word3win;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.word3win = true;
        wordWin = unscrambleMainObject.word3win;
        Assert.AreEqual(wordWin, true);

        // verify word win default
        wordWin = unscrambleMainObject.word4win;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.word4win = true;
        wordWin = unscrambleMainObject.word4win;
        Assert.AreEqual(wordWin, true);

        // verify word win default
        wordWin = unscrambleMainObject.wordFinalWin;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.wordFinalWin = true;
        wordWin = unscrambleMainObject.wordFinalWin;
        Assert.AreEqual(wordWin, true);

        // verify word win default
        wordWin = unscrambleMainObject.wordsAllWin;
        Assert.AreEqual(wordWin, false);
        // verify word win change
        unscrambleMainObject.wordsAllWin = true;
        wordWin = unscrambleMainObject.wordsAllWin;
        Assert.AreEqual(wordWin, true);


    }

    [UnityTest]
    public IEnumerator TestUnscrambleMainFinalWord()
    {
        // verify unscramble final word win 

        // load scene with the scriptable objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        ComUnscrambleMain unscrambleMainObject = GameObject.FindObjectOfType<ComUnscrambleMain>();

        GameObject letterbox = GameObject.Find("letterbox5");

        string sortingLayer = "";
        sortingLayer = letterbox.GetComponent<SpriteRenderer>().sortingLayerName;
        Assert.AreEqual(sortingLayer, "Default");

        unscrambleMainObject.UpdateFinalWord();

        sortingLayer = letterbox.GetComponent<SpriteRenderer>().sortingLayerName;
        Assert.AreEqual(sortingLayer, "Puzzle");

    }

    [UnityTest]
    public IEnumerator TestUnscrambleWinScene()
    {
        // verify unscramble win scene

        // load scene with the scriptable objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        ComUnscrambleMain unscrambleMainObject = GameObject.FindObjectOfType<ComUnscrambleMain>();
        ComGameData comGameDataObject = GameObject.FindObjectOfType<ComGameData>();
        int backgroundChild = 0;
        int textChild = 0;
        bool finalWordWin = true;
        string winLetter = "";
        int wordRow = 1;
        int index = 0;
        GameObject letterObject;
        int finalWord = 5;

        // verify final word is not solved
        finalWordWin = unscrambleMainObject.checkWordWin(finalWord);
        Assert.AreEqual(finalWordWin, false);

        unscrambleMainObject.EnableWord(1);
        unscrambleMainObject.EnableWord(2);
        unscrambleMainObject.EnableWord(3);
        unscrambleMainObject.EnableWord(4);
        unscrambleMainObject.EnableWord(5);

        // set buttons with win letters
        int letterCount = 40;
        for (int letterPos = 1; letterPos <= letterCount; letterPos++)
        {
            if (letterPos == 8)
            {
                wordRow = 2;
                index = 0;
            }
            if (letterPos == 19)
            {
                wordRow = 3;
                index = 0;
            }
            if (letterPos == 27)
            {
                wordRow = 4;
                index = 0;
            }
            if (letterPos == 34)
            {
                wordRow = 5;
                index = 0;
            }

            letterObject = GameObject.Find(letterPos.ToString() + "_Button");
            winLetter = comGameDataObject.getWinLetter(wordRow, index);

            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text = winLetter;

            index++;
        }

        yield return new WaitForSeconds(4.0f); // delay to load win

        // verify success comments are showing after final word win
        Color winColor = new Color32(0, 0, 0, 255);
        GameObject successObject = successObject = GameObject.Find("Success");
        Color successColor = successObject.GetComponent<UnityEngine.UI.Text>().color;
        Assert.AreEqual(successColor, winColor);

        // verify final word solved
        finalWordWin = unscrambleMainObject.checkWordWin(finalWord);
        Assert.AreEqual(finalWordWin, true);
    }

    [UnityTest]
    public IEnumerator TestUnscrambleHints()
    {
        // verify unscramble hints are displayed

        // load scene with the scriptable objects
        SceneManager.LoadScene("comUnscramble");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        Color originalColor = new Color32(246, 34, 250, 0);
        int backgroundChild = 0;
        int textChild = 0;
        string winLetter = "";
        Color winColor;
        string defaultLetter = "";
        Color defaultColor;
        EventSystem eventSystem = EventSystem.current;
        GameObject hint;
        GameObject letterObject;
        string hintValue = "";
        string buttonValue = "";
        string defaultLetterValue = "";
        string winLetterValue = "";

        string[] hint1 = { "E", "S" };
        string[] hint2 = { "N", "C" };
        string[] hint3 = { "G", "M" };
        string[] hint4 = { "T", "G" };
        string[] hint5 = { "E", "N" };
        string[] hintValues = { };


        // test values
        Dictionary<string, string> testHints = new Dictionary<string, string>()
        {
              {"Hint1", "1_Button" },
              {"Hint2", "8_Button" },
              {"Hint3", "19_Button" },
              {"Hint4", "27_Button" },
              {"Hint5", "34_Button" }

        };


        foreach (KeyValuePair<string, string> hintButton in testHints)
        {

            hintValue = hintButton.Key;
            if (hintValue == "Hint1")
            {
                hintValues = hint1;
            }
            if (hintValue == "Hint2")
            {
                hintValues = hint2;
            }
            if (hintValue == "Hint3")
            {
                hintValues = hint3;
            }
            if (hintValue == "Hint4")
            {
                hintValues = hint4;
            }
            if (hintValue == "Hint5")
            {
                hintValues = hint5;
            }

            buttonValue = hintButton.Value;
            defaultLetterValue = hintValues[0];
            winLetterValue = hintValues[1];

            // verify letter values are default
            letterObject = GameObject.Find(buttonValue);
            defaultLetter = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
            defaultColor = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color;
            Assert.AreEqual(defaultLetter, defaultLetterValue);
            Assert.AreEqual(defaultColor, originalColor);

            // send hints
            hint = GameObject.Find(hintValue);
            eventSystem.SetSelectedGameObject(hint);
            hint.GetComponent<LetterActions>().SendHint();

            // verify letter hint is displayed
            winLetter = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().text;
            winColor = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color;
            yield return new WaitForSeconds(1.0f); // delay for hints
            Assert.AreEqual(winLetter, winLetterValue);
            Assert.AreEqual(winColor, Color.red);

        }

    }

    [UnityTest]
    public IEnumerator TestComPuzzleWinScene()
    {
        // verify com puzzle win scene

        // load scene with the scriptable objects
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable tile object to use
        GameObject scriptObject = GameObject.Find("14");

        GameObject mode = new GameObject();
        mode.AddComponent<AudioSource>();
        mode.AddComponent<ComGameModes>();
        ComGameModes modeObject = GameObject.FindObjectOfType<ComGameModes>();

        // solve puzzle
        modeObject.SolvePuzzle();
        yield return new WaitForSeconds(0.2f); // delay to solve

        // move tile to display win
        scriptObject.transform.position = new Vector3(5.5f, 2.3f);
        yield return new WaitForSeconds(4.0f); // delay to load scene

        // verify win scene is displayed
        GameObject background = GameObject.Find("ScrollBackground");
        string backgroundLayer = background.GetComponent<SpriteRenderer>().sortingLayerName;
        string winLayer = "WinBackground";
        Assert.AreEqual(backgroundLayer, winLayer);

        GameObject board = GameObject.Find("board");
        string boardLayer = board.GetComponent<SpriteRenderer>().sortingLayerName;
        winLayer = "WinBoard";
        Assert.AreEqual(boardLayer, winLayer);

        GameObject successObject = GameObject.Find("Success");
        Color letterColor = new Color32(0, 0, 0, 255);
        Color successColor = successObject.GetComponent<Text>().color;
        Assert.AreEqual(successColor, letterColor);

        GameObject continueButton = GameObject.Find("Continue");
        GameObject continueText = GameObject.Find("ContinueText");
        continueButton.GetComponent<Button>().enabled = true;
        continueButton.GetComponent<Image>().enabled = true;
        continueText.GetComponent<Text>().enabled = true;
        Assert.AreEqual(continueButton.GetComponent<Button>().enabled, true);
        Assert.AreEqual(continueButton.GetComponent<Image>().enabled, true);
        Assert.AreEqual(continueText.GetComponent<Text>().enabled, true);

    }
}