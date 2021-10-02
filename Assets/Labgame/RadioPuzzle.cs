using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPuzzle : LabPuzzle
{
    private float frequency;
    private float amplitude;

    public RadioPuzzle(string name, float frequency, float amplitude)
    {
        this.Name = name;
        this.frequency = frequency;
        this.amplitude = amplitude;
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
