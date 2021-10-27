using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMain : MonoBehaviour
{
    private ComputerUIMainPane mainComputerUI;
    GameObject main;
    RadioPuzzle currentPuzzle;
    bool isCurrentPuzzleSolved;
    bool radioPuzzleActive;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("LabGameStart");
        mainComputerUI = GameObject.Find("ComputerUIMainPane").GetComponent<ComputerUIMainPane>();
        level = 0;
        radioPuzzleActive = false;
        GetNewRadioPuzzle();








    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentPuzzleSolved)
        {
            mainComputerUI.DisplayComputerText("Congrats! You've solved " + level.ToString() + " waves!");
            GetNewRadioPuzzle();
        }
        if (currentPuzzle.solved)
        {
            isCurrentPuzzleSolved = true;
            
        }


    }

    public void GetNewRadioPuzzle()
    {
        if (currentPuzzle != null)
        {
            //Destroy(currentPuzzle.gameObject);
        }
        level++;
        RadioPuzzleParams radioPuzzleSettings = new RadioPuzzleParams();
        radioPuzzleSettings.Amplitude = 1;
        radioPuzzleSettings.Frequency = 1;

        RadioPuzzle myRadioPuzzle = main.AddComponent<RadioPuzzle>();
        myRadioPuzzle.InitializeRadioPuzzle("Puzzle " + level.ToString(), radioPuzzleSettings);

        currentPuzzle = myRadioPuzzle;
        isCurrentPuzzleSolved = false;
    }

    public void SetPuzzleSolved()
    {
        isCurrentPuzzleSolved = true;
    }

    public void IncrementFrequency()
    {
        currentPuzzle.IncrementFrequency();
    }

    public void DecrementFrequency()
    {
        currentPuzzle.DecrementFrequency();
    }

    public void IncrementAmplitude()
    {
        currentPuzzle.IncrementAmplitude();
    }

    public void DecrementAmplitude()
    {
        currentPuzzle.DecrementAmplitude();
    }
}
