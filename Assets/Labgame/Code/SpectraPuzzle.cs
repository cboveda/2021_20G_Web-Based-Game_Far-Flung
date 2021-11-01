using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectraPuzzle : LabPuzzle
{

    public Spectra iron = new Spectra("Iron", new int[] { 0, 4, 4, 0, 0, 0, 4, 4, 4, 4, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
    public Spectra nickel = new Spectra("Nickel", new int[] { 0, 0, 0, 1, 0, 2, 4, 4, 0, 1, 0, 2, 2, 0, 2, 0, 0, 0, 0, 0 });
    public Spectra aluminum = new Spectra("Aluminum", new int[] { 0, 0, 0, 0, 2, 0, 3, 4, 0, 0, 2, 0, 1, 0, 1, 0, 0, 1, 0, 0 });
    public Spectra gold = new Spectra("Gold", new int[] { 0, 0, 0, 0, 2, 0, 3, 4, 0, 0, 2, 0, 1, 0, 1, 0, 0, 1, 0, 0 });
    public Spectra silver = new Spectra("Silver", new int[] { 0, 1, 0, 0, 0, 2, 0, 0, 2, 0, 0, 0, 2, 0, 0, 2, 0, 1, 0, 0 });
    public Spectra none = new Spectra("None", new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

    private Spectra primaryElement;
    private Spectra secondaryElement;
    private Spectra traceElement;
    private int[] solution;
    private int[] combinedSpectraDisplay;

    int puzzleDifficulty;


    public void InitializeSpectraPuzzle(string name, int difficulty)
    {

        this.Name = name;
        //Placeholder.
        this.puzzleDifficulty = difficulty;

        primaryElement = none;
        secondaryElement = none;
        traceElement = none;

        //Spectra library definitions
        


        solution = CombineSpectra(iron, gold, silver);
    }

    public void SetPrimarySpectra(Spectra primary)
    {
        primaryElement = primary;
        UpdateDisplaySpectra();
    }

    public void SetSecondarySpectra(Spectra secondary)
    {
        secondaryElement = secondary;
        UpdateDisplaySpectra();
    }

    public void SetTraceSpectra(Spectra trace)
    {
        traceElement = trace;
        UpdateDisplaySpectra();
    }

    public void UpdateDisplaySpectra()
    {
        CombineSpectra(primaryElement, secondaryElement, traceElement);
    }

    public int[] CombineSpectra(Spectra primary, Spectra secondary, Spectra trace)
    {
        int[] result = new int[Spectra.SPECTRA_ARRAY_SIZE];


        return result;
    }

    public override bool CheckSolution()
    {

        return false;
    }
}
