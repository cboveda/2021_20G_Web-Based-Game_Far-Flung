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

        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "ImagerButton")
        {            
            FindObjectOfType<SendDataActions>().buttonStart = 1;
            letterCount = 7;
        }
        if (buttonName == "SpectometerButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 8;
            letterCount = 11;
        }
        if (buttonName == "MagnetometerButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 19;
            letterCount = 8;
        }
        if (buttonName == "RadioButton")
        {
            FindObjectOfType<SendDataActions>().buttonStart = 27;
            letterCount = 7;
        }

        //buttonStart = FindObjectOfType<SendDataActions>().buttonStart;
        StartCoroutine(SignalAnimation(letterCount));

    }

    IEnumerator SignalAnimation(int letterCount)
    {

        for (int letter = 0; letter < letterCount; letter++)
        {
            StartCoroutine(SendSignal("signal_1"));

            yield return new WaitForSeconds(0.06f);

            StartCoroutine(SendSignal("signal_2"));

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(SendSignal("signal_3"));

            yield return new WaitForSeconds(0.6f);
        }

    }

    IEnumerator SendSignal(string signal)
    {
        float xSignal = 4.9f;
        float ySignal = 3.2f;
        float xIncrement = -0.55474f;
        float yIncrement = 0.012632f;
        Color letterColor = new Color32(246, 34, 250, 255);
        Color hiddenColor = new Color32(246, 34, 250, 0);
        int backgroundChild = 0;
        int textChild = 0;
        GameObject letterObject;
        GameObject signalObject;
        int buttonNumber = 0;
                
        signalObject = GameObject.Find(signal);
        signalObject.GetComponent<SpriteRenderer>().sortingLayerName = "Board";
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
            letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = letterColor;
            buttonNumber++;
            FindObjectOfType<SendDataActions>().buttonStart= buttonNumber;
        }
                
    }
}
