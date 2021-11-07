using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMain : MonoBehaviour
{
    private ComputerUIMainPane mainComputerUI;
    GameObject main;
    RadioPuzzle currentRadioPuzzle;
    bool isCurrentRadioPuzzleSolved;
    bool isCurrentSpectraPuzzleSolved;
    bool radioPuzzleActive;
    bool spectraPuzzleActive;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("LabGameStart");
        mainComputerUI = GameObject.Find("ComputerUIMainPane").GetComponent<ComputerUIMainPane>();
        level = 0;
        radioPuzzleActive = true;
        spectraPuzzleActive = false;
        GetNewRadioPuzzle();








    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentRadioPuzzleSolved && radioPuzzleActive)
        {
            
            if(level < 2)
            {
                mainComputerUI.DisplayComputerText("Congrats! You've solved " + level.ToString() + " waves!");
                GetNewRadioPuzzle();
            }
            else
            {
                mainComputerUI.DisplayComputerText("Congrats, you solved all radio puzzles.  Solve the spectra puzzle to the right.");
                radioPuzzleActive = false;
                spectraPuzzleActive = true;
            }
            
        }
        if (currentRadioPuzzle.solved && !isCurrentRadioPuzzleSolved)
        {
            isCurrentRadioPuzzleSolved = true;
            
        }

        if (isCurrentRadioPuzzleSolved && spectraPuzzleActive)
        {

        }


    }

    public void GetNewRadioPuzzle()
    {
        if (currentRadioPuzzle != null)
        {
            //Destroy(currentPuzzle.gameObject);
        }
        level++;
        RadioPuzzleParams radioPuzzleSettings = new RadioPuzzleParams();
        radioPuzzleSettings.Amplitude = 1;
        radioPuzzleSettings.Frequency = 1;

        RadioPuzzle myRadioPuzzle = main.AddComponent<RadioPuzzle>();
        myRadioPuzzle.InitializeRadioPuzzle("Puzzle " + level.ToString(), radioPuzzleSettings);

        currentRadioPuzzle = myRadioPuzzle;
        isCurrentRadioPuzzleSolved = false;
    }

    public void SetPuzzleSolved()
    {
        isCurrentRadioPuzzleSolved = true;
    }

    public void IncrementFrequency()
    {
        currentRadioPuzzle.IncrementFrequency();
    }

    public void DecrementFrequency()
    {
        currentRadioPuzzle.DecrementFrequency();
    }

    public void IncrementAmplitude()
    {
        currentRadioPuzzle.IncrementAmplitude();
    }

    public void DecrementAmplitude()
    {
        currentRadioPuzzle.DecrementAmplitude();
    }
}
