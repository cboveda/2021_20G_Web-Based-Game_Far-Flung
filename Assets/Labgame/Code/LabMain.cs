using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DialogMaker;
using UnityEngine.SceneManagement;


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
    public int levelRadio;
    int levelSpectra;

    GameObject sineUIElements;
    GameObject spectraUIElements;
    GameObject spectraUIElements2;

    DialogGenerator Diagen;
    
    

    Camera lcdCamera1;
    // Start is called before the first frame update
    void Start()
    {
        sineUIElements = GameObject.Find("SineUI");
        spectraUIElements = GameObject.Find("SpectraUI");
        spectraUIElements2 = GameObject.Find("SpectraUIElements2");
        main = GameObject.Find("LabGameStart");
        mainComputerUI = GameObject.Find("ComputerUIMainPane").GetComponent<ComputerUIMainPane>();
        levelRadio = 0;
        radioPuzzleActive = true;
        spectraPuzzleActive = false;
        isCurrentSpectraPuzzleSolved = false;

        sineUIElements.SetActive(true);
        spectraUIElements.SetActive(false);
        spectraUIElements2.SetActive(false);
        
        GetNewRadioPuzzle();
        lcdCamera1 = GameObject.Find("ComputerScreen1Camera").GetComponent<Camera>();


        GameObject.Find("DialogSine").GetComponent<DialogGenerator>().BeginPlayingDialog();

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
                GameObject.Find("DialogSpectra").GetComponent<DialogGenerator>().BeginPlayingDialog();
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
                SceneManager.LoadScene("Hub");
                
            }
        }

        if (isCurrentRadioPuzzleSolved && spectraPuzzleActive && levelSpectra == 0)
        {
            sineUIElements.SetActive(false);
            spectraUIElements.SetActive(true);
            spectraUIElements2.SetActive(true);
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

        mySpectraPuzzle.InitializeSpectraPuzzle("Puzzle " + levelSpectra.ToString(), levelSpectra);


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
        currentSpectraPuzzle.AddSpectraToTest();
    }

    public void CheckSpectraAnswer()
    {
        currentSpectraPuzzle.CheckSolution();
    }

    public void GetNextSpectra()
    {
        currentSpectraPuzzle.GetNextExampleSpectra();
    }
    public void GetPrevSpectra()
    {
        currentSpectraPuzzle.GetPrevExampleSpectra();
    }
    public void AddSpectra()
    {
        currentSpectraPuzzle.AddSpectraToTest();
    }
    public void RemoveSpectra()
    {
        currentSpectraPuzzle.RemoveSpectra();
    }

    public RadioPuzzle GetCurrentRadioPuzzle()
    {
        return currentRadioPuzzle;
    }

}
