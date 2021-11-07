using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMain : MonoBehaviour
{
    private ComputerUIMainPane mainComputerUI;
    GameObject main;
    RadioPuzzle currentRadioPuzzle;
    SpectraPuzzle currentSpectraPuzzle;
    bool isCurrentRadioPuzzleSolved;
    bool isCurrentSpectraPuzzleSolved;
    bool radioPuzzleActive;
    bool spectraPuzzleActive;
    int levelRadio;
    int levelSpectra;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("LabGameStart");
        mainComputerUI = GameObject.Find("ComputerUIMainPane").GetComponent<ComputerUIMainPane>();
        levelRadio = 0;
        radioPuzzleActive = true;
        spectraPuzzleActive = false;
        isCurrentSpectraPuzzleSolved = false;
        GetNewRadioPuzzle();








    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentRadioPuzzleSolved && radioPuzzleActive)
        {
            
            if(levelRadio < 2)
            {
                mainComputerUI.DisplayComputerText("Congrats! You've solved " + levelRadio.ToString() + " waves!");
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

        

        if(isCurrentSpectraPuzzleSolved && spectraPuzzleActive)
        {
            if(levelSpectra < 2)
            {
                mainComputerUI.DisplayComputerText("Congrats! You've solved Spectra " + levelSpectra.ToString());
                GetNewSpectraPuzzle();
            }
            else
            {
                mainComputerUI.DisplayComputerText("Congrats, you solved all spectra puzzles.  Well done!");
                radioPuzzleActive = false;
                spectraPuzzleActive = false;
            }
        }

        if (isCurrentRadioPuzzleSolved && spectraPuzzleActive && levelSpectra == 0)
        {
            GetNewSpectraPuzzle();
        }

        if (spectraPuzzleActive && currentSpectraPuzzle.solved && !isCurrentSpectraPuzzleSolved)
        {
            isCurrentSpectraPuzzleSolved = true;
            

        }


    }

    public void GetNewRadioPuzzle()
    {
        if (currentRadioPuzzle != null)
        {
            //Destroy(currentPuzzle.gameObject);
        }
        levelRadio++;
        RadioPuzzleParams radioPuzzleSettings = new RadioPuzzleParams();
        radioPuzzleSettings.Amplitude = 1;
        radioPuzzleSettings.Frequency = 1;

        RadioPuzzle myRadioPuzzle = main.AddComponent<RadioPuzzle>();
        myRadioPuzzle.InitializeRadioPuzzle("Puzzle " + levelRadio.ToString(), radioPuzzleSettings);

        currentRadioPuzzle = myRadioPuzzle;
        isCurrentRadioPuzzleSolved = false;
    }

    public void GetNewSpectraPuzzle()
    {
        if(currentSpectraPuzzle != null)
        {

        }
        levelSpectra++;
        SpectraPuzzle mySpectraPuzzle = main.AddComponent<SpectraPuzzle>();
        currentSpectraPuzzle = mySpectraPuzzle;
        isCurrentSpectraPuzzleSolved = false;

        mySpectraPuzzle.InitializeSpectraPuzzle("Puzzle " + levelSpectra.ToString(), 0);

        
        

    }

    public void SetRadioPuzzleSolved()
    {
        isCurrentRadioPuzzleSolved = true;
    }

    public void SetSpectraPuzzleSolved()
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

    public void InsertSpectra(Spectra insertedElement)
    {
        currentSpectraPuzzle.AddSpectraToTest(insertedElement);
    }

    public void CheckSpectraAnswer()
    {
        currentSpectraPuzzle.CheckSolution();
    }



}
