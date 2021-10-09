using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPuzzle : LabPuzzle
{
    private float frequencyAnswer;
    private float amplitudeAnswer;
    private float frequencyStart;
    private float amplitudeStart;
    private float frequencyCurrentGuess;
    private float amplitudeCurrentGuess;
    public bool solved;

    public GameObject radioSolver;
    public GameObject radioReference;
    public SineWave waveReference;
    public SineWave waveSolver;
    

    const float INCREMENT_AMOUNT = 0.1f;
    const float MAX_RADIO_VALUE = 2.0f;
    const float MIN_RADIO_VALUE = 0.2f;
    
    public void InitializeRadioPuzzle(string name, RadioPuzzleParams radioParams)
    {
        
        this.Name = name;
        this.frequencyAnswer = radioParams.Frequency;
        this.amplitudeAnswer = radioParams.Amplitude;
        solved = false;

        RadioPuzzleParams start = new RadioPuzzleParams();
        RandomizeSolverStart();
        start.Amplitude = amplitudeStart;
        start.Frequency = frequencyStart;


        GameObject radioReference = GameObject.Find("RadioReference");
        GameObject radioSolver = GameObject.Find("RadioSolver");

        waveReference = radioReference.AddComponent<SineWave>();
        waveReference.SetParameters(radioParams);
        waveSolver = radioSolver.AddComponent<SineWave>();
        waveSolver.SetParameters(start);
        waveReference.InitializeSineWave();
        waveSolver.InitializeSineWave();

    }

    public void IncrementFrequency()
    {

        float newValue = frequencyCurrentGuess + INCREMENT_AMOUNT;
        newValue = Mathf.Round(newValue * 10.0f) * 0.1f;
        if (newValue >= MIN_RADIO_VALUE && newValue <= MAX_RADIO_VALUE)
        {
            frequencyCurrentGuess = newValue;
            waveSolver.frequency = frequencyCurrentGuess;
            CheckSolution();
        }
    }

    public void DecrementFrequency()
    {

        float newValue = frequencyCurrentGuess - INCREMENT_AMOUNT;
        newValue = Mathf.Round(newValue * 10.0f) * 0.1f;
        if (newValue >= MIN_RADIO_VALUE && newValue <= MAX_RADIO_VALUE)
        {
            frequencyCurrentGuess = newValue;
            waveSolver.frequency = frequencyCurrentGuess;
            CheckSolution();
        }
    }

    public void IncrementAmplitude()
    {

        float newValue = amplitudeCurrentGuess + INCREMENT_AMOUNT;
        newValue = Mathf.Round(newValue * 10.0f) * 0.1f;
        if (newValue >= MIN_RADIO_VALUE && newValue <= MAX_RADIO_VALUE)
        {
            amplitudeCurrentGuess = newValue;
            waveSolver.amplitude = amplitudeCurrentGuess;
            CheckSolution();
        }
    }

    public void DecrementAmplitude()
    {

        float newValue = amplitudeCurrentGuess - INCREMENT_AMOUNT;
        newValue = Mathf.Round(newValue * 10.0f) * 0.1f;
        if (newValue >= MIN_RADIO_VALUE && newValue <= MAX_RADIO_VALUE)
        {
            amplitudeCurrentGuess = newValue;
            waveSolver.amplitude = amplitudeCurrentGuess;
            CheckSolution();
        }
    }


    public override bool CheckSolution()
    {
       if (frequencyCurrentGuess == frequencyAnswer && amplitudeCurrentGuess == amplitudeAnswer)
        {
            solved = true;
            waveReference.DestroyWave();
            waveSolver.DestroyWave();
            return true;
        }
        else
        {
            return false;
        }
 
    }

    private void RandomizeSolverStart()
    {
        frequencyStart = Random.Range(2, 21)/10.0f;
        amplitudeStart = Random.Range(2, 21)/10.0f;
        frequencyCurrentGuess = frequencyStart;
        amplitudeCurrentGuess = amplitudeStart;
    }


}
