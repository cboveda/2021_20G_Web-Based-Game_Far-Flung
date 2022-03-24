using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SendDataActions : MonoBehaviour
{

    int start = 0;
    string buttonName = "";
    AudioSource audioSource;
    GameObject volumeSlider;
    float volumeStart = 0.5f;
    bool imagePressed = false;
    bool spectroPressed = false;
    bool magnetoPressed = false;
    bool radioPressed = false;
    bool showingScore = false;
    bool showingGameScore = false;
    GameObject hint1;
    GameObject hint2;
    GameObject hint3;
    GameObject hint4;
    int backgroundChild = 0;
    int textChild = 0;
    Color originalColor;
    Color hiddenColor;
    ColorBlock hintBlock;
    Color originalText;


    // Start is called before the first frame update
    void Start()
    {
        
        hiddenColor = new Color32(0, 0, 0, 0);

        audioSource = GetComponent<AudioSource>();
        volumeSlider = GameObject.Find("VolumeSlider");
        volumeSlider.GetComponent<Slider>().value = volumeStart;

        hint1 = GameObject.Find("Hint1");
        hint2 = GameObject.Find("Hint2");
        hint3 = GameObject.Find("Hint3");
        hint4 = GameObject.Find("Hint4");

        hint1.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        hint2.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        hint3.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;
        hint4.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeSlider.GetComponent<Slider>().value;
    }


    public int buttonStart
    {
        get { return start; }
        set { start = value; }
    }

    public void SendData()
    {
        showingScore = FindObjectOfType<Scoring>().getShowingScore;
        showingGameScore = FindObjectOfType<Scoring>().getShowingGameScore;
        if (showingScore || showingGameScore)
        {
            return;
        }

        //int buttonStart = 0;
        int letterCount = 0;
        int wordRow = 0;
        bool wordUpdated = false;
        string hintButton = "";

        buttonName = EventSystem.current.currentSelectedGameObject.name;               
        
        DisableDataButtons();

        
        if (buttonName == "ImagerButton")
        {
            if (imagePressed == false)
            {
                Scoring.Instance.addToScore(100, "ComObjective6");
                imagePressed = true;
            }
            else
            {
                Scoring.Instance.addToScore(-10, "ComObjective6");
            }

            FindObjectOfType<SendDataActions>().buttonStart = 1;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word1ColorUpdated;
            wordRow = 1;
            letterCount = 7;

            hintButton = "Hint1";
            hint1.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;

        }
        if (buttonName == "SpectometerButton")
        {
            if (spectroPressed == false)
            {
                Scoring.Instance.addToScore(100, "ComObjective6");
                spectroPressed = true;
            }
            else
            {
                Scoring.Instance.addToScore(-10, "ComObjective6");
            }

            FindObjectOfType<SendDataActions>().buttonStart = 8;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word2ColorUpdated;
            wordRow = 2;
            letterCount = 11;

            hintButton = "Hint2";
            hint2.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;

        }
        if (buttonName == "MagnetometerButton")
        {
            if (magnetoPressed == false)
            {
                Scoring.Instance.addToScore(100, "ComObjective6");
                magnetoPressed = true;
            }
            else
            {
                Scoring.Instance.addToScore(-10, "ComObjective6");
            }

            FindObjectOfType<SendDataActions>().buttonStart = 19;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word3ColorUpdated;
            wordRow = 3;
            letterCount = 8;

            hintButton = "Hint3";
            hint3.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;

        }
        if (buttonName == "RadioButton")
        {
            if (radioPressed == false)
            {
                Scoring.Instance.addToScore(100, "ComObjective6");
                radioPressed = true;
            }
            else
            {
                Scoring.Instance.addToScore(-10, "ComObjective6");
            }

            FindObjectOfType<SendDataActions>().buttonStart = 27;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word4ColorUpdated;
            wordRow = 4;
            letterCount = 7;

            hintButton = "Hint4";
            hint4.transform.GetComponent<UnityEngine.UI.Button>().interactable = false;

        }

        FindObjectOfType<ComUnscrambleMain>().DisableWord(wordRow);

        if (!wordUpdated)
        {
            FindObjectOfType<ComUnscrambleMain>().HideWord(wordRow);
        }

        
        //buttonStart = FindObjectOfType<SendDataActions>().buttonStart;
        StartCoroutine(SignalAnimation(letterCount, wordUpdated, wordRow, hintButton));

    }

    IEnumerator SignalAnimation(int letterCount, bool wordUpdated, int wordRow, string hintButton)
    {
        
        for (int letter = 0; letter < letterCount; letter++)
        {
            if (letter % 2 == 0)
            {
                audioSource.Play();
            }            

            StartCoroutine(SendSignal("signal_1", wordUpdated, hintButton));            

            yield return new WaitForSeconds(0.06f);

            StartCoroutine(SendSignal("signal_2", wordUpdated, hintButton));

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(SendSignal("signal_3", wordUpdated, hintButton));

            yield return new WaitForSeconds(0.6f);
        }

        if (!wordUpdated)
        {
            FindObjectOfType<ComUnscrambleMain>().EnableWord(wordRow);
            EnableHintButton(hintButton);
        }    

        EnableDataButtons();        

    }

    public void EnableHintButton(string hintButton)
    {
        GameObject hint = GameObject.Find(hintButton);
        hint.transform.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }


    IEnumerator SendSignal(string signal, bool wordUpdated, string hintButton)
    {
        float xSignal = 4.9f;
        float ySignal = 3.2f;
        float xIncrement = -0.55474f;
        float yIncrement = 0.012632f;
        Color letterColor = new Color32(246, 34, 250, 255);
        Color hiddenColor = new Color32(246, 34, 250, 0);
        Color winColorText = new Color32(0, 0, 0, 0);
        Color winColorBackground = new Color32(0, 90, 0, 255);
        Color winLetterText = new Color32(75, 255, 35, 255);
        int backgroundChild = 0;
        int textChild = 0;
        GameObject letterObject;
        GameObject signalObject;
        int buttonNumber = 0;
                
        signalObject = GameObject.Find(signal);
        signalObject.GetComponent<SpriteRenderer>().sortingLayerName = "Puzzle";
        int moveCount = 20;
        for (int move = 0; move < moveCount; move++)
        {
            signalObject.GetComponent<SpriteRenderer>().transform.position = new Vector3(xSignal, ySignal);
            yield return new WaitForSeconds(0.03f);
            xSignal += xIncrement;
            ySignal += yIncrement;
        }
        signalObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";

        if (signal == "signal_3")
        {

            buttonNumber = FindObjectOfType<SendDataActions>().buttonStart;
            letterObject = GameObject.Find(buttonNumber.ToString() + "_Button");

            if (wordUpdated)
            {
                winColorText = letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color;
                //winColorBackground = letterObject.transform.GetChild(backgroundChild).GetComponent<UnityEngine.UI.Image>().color;
                letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winLetterText;
                yield return new WaitForSeconds(0.1f);
                letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winColorText;
                letterObject.transform.GetChild(backgroundChild).GetComponent<UnityEngine.UI.Image>().color = winColorBackground;
            }
            else
            {
                letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = letterColor;
            }            
            
            buttonNumber++;
            FindObjectOfType<SendDataActions>().buttonStart= buttonNumber;
        }
                
    }

    public void DisableDataButtons()
    {
        GameObject dataButtonObject;        
        string[] dataButtons = { "ImagerButton", "SpectometerButton", "MagnetometerButton", "RadioButton" };

        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            dataButtonObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

    }


    public void EnableDataButtons()
    {
        GameObject dataButtonObject;
        string[] dataButtons = { "ImagerButton", "SpectometerButton", "MagnetometerButton", "RadioButton" };

        foreach (string button in dataButtons)
        {
            dataButtonObject = GameObject.Find(button);
            dataButtonObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

    }
}
