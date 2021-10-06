using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPuzzle : LabPuzzle
{
    private float frequencyAnswer;
    private float amplitudeAnswer;
    private float frequencyGuess;
    private float amplitudeGuess;
    //private SineWave wave;
    //public GameObject grapher;
    public GameObject radioSolver;
    public GameObject radioReference;
    public SineWave waveReference;
    public SineWave waveSolver;

    const float INCREMENT_AMOUNT = 0.2f;
    void Awake()
    {
        //GameObject radioReference = GameObject.Find("RadioReference");
        //GameObject radioSolver = GameObject.Find("RadioSolver");
    }

    public void InitializeRadioPuzzle(string name, RadioPuzzleParams radioParams)
    {
        //TODO:  Implement some boundry checks on puzzle attributes.
        this.Name = name;
        this.frequencyAnswer = radioParams.Frequency;
        this.amplitudeAnswer = radioParams.Amplitude;

        RadioPuzzleParams start = new RadioPuzzleParams();
        RandomizeSolverStart();
        start.Amplitude = amplitudeGuess;
        start.Frequency = frequencyGuess;

        GameObject radioReference = GameObject.Find("RadioReference");
        GameObject radioSolver = GameObject.Find("RadioSolver");

        waveReference = radioReference.AddComponent<SineWave>();
        waveReference.SetParameters(radioParams);
        waveSolver = radioSolver.AddComponent<SineWave>();
        waveSolver.SetParameters(start);

    }


    public override bool CheckSolution(IPuzzleParams puzzleParams)
    {
       if (!(puzzleParams is RadioPuzzleParams))
        {
            return false;
        }
       else
        {
            RadioPuzzleParams paramsToCheck = puzzleParams as RadioPuzzleParams;
            return (paramsToCheck.Frequency == frequencyAnswer && paramsToCheck.Amplitude == amplitudeAnswer);
        }
 
    }

    private void RandomizeSolverStart()
    {
        frequencyGuess = Random.Range(2, 21)/10.0f;
        amplitudeGuess = Random.Range(2, 21)/10.0f;
    }


}
