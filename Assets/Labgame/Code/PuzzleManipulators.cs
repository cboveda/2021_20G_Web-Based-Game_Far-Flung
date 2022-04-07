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
        //else if (buttonName == "AmplitudeDec")
        //{
        //    main.GetComponent<LabMain>().DecrementAmplitude();
        //}
        //else if (buttonName == "Insert Iron")
        //{
        //    main.GetComponent<LabMain>().InsertSpectra(SpectraPuzzle.iron);
        //}
        //else if (buttonName == "Insert Nickel")
        //{
        //    main.GetComponent<LabMain>().InsertSpectra(SpectraPuzzle.nickel);
        //}
        //else if (buttonName == "Insert Aluminum")
        //{
        //    main.GetComponent<LabMain>().InsertSpectra(SpectraPuzzle.aluminum);
        //}
        //else if (buttonName == "Insert Gold")
        //{
        //    main.GetComponent<LabMain>().InsertSpectra(SpectraPuzzle.gold);
        //}
        //else if (buttonName == "Insert Silver")
        //{
        //    main.GetComponent<LabMain>().InsertSpectra(SpectraPuzzle.silver);
        //}
        else if (buttonName == "ElementNext")
        {
            main.GetComponent<LabMain>().GetNextSpectra();
        }
        else if (buttonName == "ElementPrev")
        {
            main.GetComponent<LabMain>().GetPrevSpectra();
        }
        else if (buttonName == "ElementAdd")
        {
            main.GetComponent<LabMain>().AddSpectra();
        }
        else if (buttonName == "ElementRemove")
        {
            main.GetComponent<LabMain>().RemoveSpectra();
        }
        else if (buttonName == "ElementCheck")
        {
            main.GetComponent<LabMain>().CheckSpectraAnswer();
        }
    }

}
