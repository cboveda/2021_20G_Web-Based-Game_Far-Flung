using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPuzzleParams : IPuzzleParams
{
    private float frequency;
    private float amplitude;

    public float Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }

    public float Amplitude
    {
        get { return amplitude; }
        set { amplitude = value; }
    }


}
