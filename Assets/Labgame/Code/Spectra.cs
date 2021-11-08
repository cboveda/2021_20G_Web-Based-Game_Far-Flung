using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectra
{
    private string elementName;
    private int[] compositionArray;

    public const int SPECTRA_ARRAY_SIZE = 20;

    public Spectra(string name, int[] compositionArray)
    {
        if(compositionArray.Length != SPECTRA_ARRAY_SIZE)
        {
            Debug.LogAssertion("Spectra was not of the right size.");
        }
        else
        {
            elementName = name;
            this.compositionArray = compositionArray;
        }
    }

    public int[] GetSpectraArray()
    {
        return compositionArray;
    }

    public string GetSpectraName()
    {
        return elementName;
    }
}
