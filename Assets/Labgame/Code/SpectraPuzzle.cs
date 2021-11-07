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
    private int[] combinedSpectra;

    

    //IDictionary<int, Color32> spectraColorBySlot = new Dictionary<int, Color32>();
    Color32[] spectraColorBySlot = new Color32[Spectra.SPECTRA_ARRAY_SIZE];
    

    int puzzleDifficulty;


    public void InitializeSpectraPuzzle(string name, int difficulty)
    {

        this.Name = name;
        /*spectraColorBySlot.Add(0, new Color32(0, 0, 255, 0));
        spectraColorBySlot.Add(1, new Color32(26, 78, 201, 0));
        spectraColorBySlot.Add(2, new Color32(23, 130, 212, 0));
        spectraColorBySlot.Add(3, new Color32(26, 152, 221, 0));
        spectraColorBySlot.Add(4, new Color32(9, 204, 204, 0));
        spectraColorBySlot.Add(5, new Color32(27, 231, 180, 0));
        spectraColorBySlot.Add(6, new Color32(27, 235, 126, 0));
        spectraColorBySlot.Add(7, new Color32(24, 227, 75, 0));
        spectraColorBySlot.Add(8, new Color32(33, 228, 19, 0));
        spectraColorBySlot.Add(9, new Color32(60, 232, 27, 0));
        spectraColorBySlot.Add(10, new Color32(141, 231, 2, 0));
        spectraColorBySlot.Add(11, new Color32(198, 220, 12, 0));
        spectraColorBySlot.Add(12, new Color32(218, 203, 18, 0));
        spectraColorBySlot.Add(13, new Color32(223, 181, 28, 0));
        spectraColorBySlot.Add(14, new Color32(231, 155, 4, 0));
        spectraColorBySlot.Add(15, new Color32(223, 108, 22, 0));
        spectraColorBySlot.Add(16, new Color32(228, 91, 10, 0));
        spectraColorBySlot.Add(17, new Color32(229, 65, 5, 0));
        spectraColorBySlot.Add(18, new Color32(224, 50, 28, 0));
        spectraColorBySlot.Add(19, new Color32(255, 0, 0, 0));*/

        spectraColorBySlot[0] = new Color32(0, 0, 255, 0);
        spectraColorBySlot[1] = new Color32(26, 78, 201, 0);
        spectraColorBySlot[2] = new Color32(23, 130, 212, 0);
        spectraColorBySlot[3] = new Color32(26, 152, 221, 0);
        spectraColorBySlot[4] = new Color32(9, 204, 204, 0);
        spectraColorBySlot[5] = new Color32(27, 231, 180, 0);
        spectraColorBySlot[6] = new Color32(27, 235, 126, 0);
        spectraColorBySlot[7] = new Color32(24, 227, 75, 0);
        spectraColorBySlot[8] = new Color32(33, 228, 19, 0);
        spectraColorBySlot[9] = new Color32(60, 232, 27, 0);
        spectraColorBySlot[10] = new Color32(141, 231, 2, 0);
        spectraColorBySlot[11] = new Color32(198, 220, 12, 0);
        spectraColorBySlot[12] = new Color32(218, 203, 18, 0);
        spectraColorBySlot[13] = new Color32(223, 181, 28, 0);
        spectraColorBySlot[14] = new Color32(231, 155, 4, 0);
        spectraColorBySlot[15] = new Color32(223, 108, 22, 0);
        spectraColorBySlot[16] = new Color32(228, 91, 10, 0);
        spectraColorBySlot[17] = new Color32(229, 65, 5, 0);
        spectraColorBySlot[18] = new Color32(224, 50, 28, 0);
        spectraColorBySlot[19] = new Color32(255, 0, 0, 0);


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
        combinedSpectra = CombineSpectra(primaryElement, secondaryElement, traceElement);
    }

    public int[] CombineSpectra(Spectra primary, Spectra secondary, Spectra trace)
    {
        int[] result = new int[Spectra.SPECTRA_ARRAY_SIZE];

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            result[i] += primary.GetSpectraArray()[i] * 3;
            result[i] += secondary.GetSpectraArray()[i] * 2;
            result[i] += trace.GetSpectraArray()[i];
        }


        return result;
    }

    public Color32 GetSpectraDisplayColor(bool solutionSpectraDesired, int slot)
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;
        byte a = 0;

        if(slot < Spectra.SPECTRA_ARRAY_SIZE)
        {
            r = spectraColorBySlot[slot].r;
            g = spectraColorBySlot[slot].g;
            b = spectraColorBySlot[slot].b;

            if (solutionSpectraDesired)
            {
                a = (byte)(solution[slot] * 10);
            }
            else
            {
                a = (byte)(combinedSpectra[slot] * 10);
            }

            if(a > 0)
            {
                a += 15;
            }
            //a = (byte)(combinedSpectra[slot] * 10 + 15);
            //a = 255;
        }

        return new Color32(r, g, b, a);
    }

    public override bool CheckSolution()
    {
        if (solution == combinedSpectra)
        {
            return true;
        }

        return false;
    }
}
