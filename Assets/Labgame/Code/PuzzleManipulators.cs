using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManipulators : MonoBehaviour
{

    GameObject main;
    string buttonName;
    // Start is called before the first frame update
    void Awake()
    {
        main = GameObject.Find("LabGameStart");

    }

    public void CheckButton()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;

        if (buttonName == "FrequencyInc")
        {
            main.GetComponent<LabMain>().IncrementFrequency();
        }
        else if (buttonName == "FrequencyDec")
        {
            main.GetComponent<LabMain>().DecrementFrequency();
        }
        else if (buttonName == "AmplitudeInc")
        {
            main.GetComponent<LabMain>().IncrementAmplitude();
        }
        else if (buttonName == "AmplitudeDec")
        {
            main.GetComponent<LabMain>().DecrementAmplitude();
        }
    }

    public void IncrementFrequency()
    {
        
    }
}
