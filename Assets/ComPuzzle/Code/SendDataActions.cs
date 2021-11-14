using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SendDataActions : MonoBehaviour
{

    int start = 0;
    string buttonName = "";
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public int buttonStart
    {
        get { return start; }
        set { start = value; }
    }

    public void SendData()
    {
        //int buttonStart = 0;
        int letterCount = 0;
        int wordRow = 0;
        bool wordUpdated = false;

        buttonName = EventSystem.current.currentSelectedGameObject.name;               
        
        DisableDataButtons();
        
        if (buttonName == "ImagerButton")
        {            
            FindObjectOfType<SendDataActions>().buttonStart = 1;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word1ColorUpdated;
            wordRow = 1;
            letterCount = 7;
        }
        if (buttonName == "SpectometerButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 8;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word2ColorUpdated;
            wordRow = 2;
            letterCount = 11;
        }
        if (buttonName == "MagnetometerButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 19;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word3ColorUpdated;
            wordRow = 3;
            letterCount = 8;
        }
        if (buttonName == "RadioButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 27;
            wordUpdated = FindObjectOfType<ComUnscrambleMain>().word4ColorUpdated;
            wordRow = 4;
            letterCount = 7;
        }

        if (!wordUpdated)
        {
            FindObjectOfType<ComUnscrambleMain>().HideWord(wordRow);
        }

 
        //buttonStart = FindObjectOfType<SendDataActions>().buttonStart;
        StartCoroutine(SignalAnimation(letterCount, wordUpdated));

    }

    IEnumerator SignalAnimation(int letterCount, bool wordUpdated)
    {

        for (int letter = 0; letter < letterCount; letter++)
        {
            StartCoroutine(SendSignal("signal_1", wordUpdated));

            yield return new WaitForSeconds(0.06f);

            StartCoroutine(SendSignal("signal_2", wordUpdated));

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(SendSignal("signal_3", wordUpdated));

            yield return new WaitForSeconds(0.6f);
        }

        EnableDataButtons();

    }

    IEnumerator SendSignal(string signal, bool wordUpdated)
    {
        float xSignal = 4.9f;
        float ySignal = 3.2f;
        float xIncrement = -0.55474f;
        float yIncrement = 0.012632f;
        Color letterColor = new Color32(246, 34, 250, 255);
        Color hiddenColor = new Color32(246, 34, 250, 0);
        Color winColorText = new Color32(0, 0, 0, 0); 
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
                letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = letterColor;
                yield return new WaitForSeconds(0.1f);
                letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = winColorText;
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
