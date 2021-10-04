using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPuzzle : LabPuzzle
{
    private float frequency;
    private float amplitude;
    //private SineWave wave;
    public GameObject grapher;

    public RadioPuzzle(string name, RadioPuzzleParams radioParams)
    {
        //TODO:  Implement some boundry checks on puzzle attributes.
        this.Name = name;
        this.frequency = radioParams.Frequency;
        this.amplitude = radioParams.Amplitude;
        
        

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
            return (paramsToCheck.Frequency == frequency && paramsToCheck.Amplitude == amplitude);
        }
 
    }


}
