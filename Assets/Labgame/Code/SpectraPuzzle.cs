using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectraPuzzle : LabPuzzle
{

    public static Spectra iron = new Spectra("Iron", new int[]          { 0, 4, 4, 0, 0, 0, 4, 4, 4, 4, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
    public static Spectra nickel = new Spectra("Nickel", new int[]      { 0, 0, 0, 1, 0, 2, 4, 4, 0, 1, 0, 2, 2, 0, 2, 0, 0, 0, 0, 0 });
    public static Spectra aluminum = new Spectra("Aluminum", new int[]  { 0, 0, 0, 0, 2, 0, 3, 4, 0, 0, 2, 0, 1, 0, 1, 0, 0, 1, 0, 0 });
    public static Spectra gold = new Spectra("Gold", new int[]          { 0, 0, 0, 0, 0, 4, 0, 0, 3, 0, 0, 0, 4, 1, 0, 3, 0, 1, 0, 0 });
    public static Spectra silver = new Spectra("Silver", new int[]      { 0, 1, 0, 0, 0, 2, 0, 0, 2, 0, 0, 0, 2, 0, 0, 2, 0, 1, 0, 0 });
    public static Spectra none = new Spectra("None", new int[]          { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

    public Spectra primaryElement;
    public Spectra secondaryElement;
    public Spectra traceElement;
    private Spectra selectedElement;
    public int[] solution;
    public int[] example = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] combinedSpectra = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public int[] primary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] secondary = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] trace = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public List<Spectra> spectraList;
    public int selectedElementSlot;

    public bool solved;

    private SpectraPuzzleDisplay mySpectraPuzzleDisplay;

    private int numInserted;

    
    Color32[] spectraColorBySlot = new Color32[Spectra.SPECTRA_ARRAY_SIZE];
    

    int puzzleDifficulty;


    public void InitializeSpectraPuzzle(string name, int difficulty)
    {

        this.Name = name;
        mySpectraPuzzleDisplay = gameObject.AddComponent<SpectraPuzzleDisplay>();
        solved = false;

        spectraList = new List<Spectra>();
        spectraList.Add(iron);
        spectraList.Add(nickel);
        spectraList.Add(aluminum);
        spectraList.Add(gold);
        spectraList.Add(silver);
        spectraList.Add(none);

        selectedElementSlot = 3;
        selectedElement = spectraList[selectedElementSlot];


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

        numInserted = 0;

        
        if(difficulty == 0)
        {
            solution = CombineSpectra(iron, gold, silver);
        }
        else
        {
            solution = CombineSpectra(nickel, aluminum, gold);
        }


        
        
        mySpectraPuzzleDisplay.SetSpectraPuzzleToDisplay(this);
        mySpectraPuzzleDisplay.UpdateSolutionDisplay();
        UpdateExampleSpectra();
    }

    public void SetPrimarySpectra(Spectra primary)
    {
        primaryElement = primary;
        
    }

    public void SetSecondarySpectra(Spectra secondary)
    {
        secondaryElement = secondary;
        
    }

    public void SetTraceSpectra(Spectra trace)
    {
        traceElement = trace;
        
    }

    public void UpdateDisplaySpectra()
    {
        combinedSpectra = CombineSpectra(primaryElement, secondaryElement, traceElement);
        mySpectraPuzzleDisplay.UpdateAttemptDisplay();
        mySpectraPuzzleDisplay.UpdateElementalDisplay(1);
        mySpectraPuzzleDisplay.UpdateElementalDisplay(2);
        mySpectraPuzzleDisplay.UpdateElementalDisplay(3);
    }

    public void UpdateExampleSpectra()
    {
        int[] result = new int[Spectra.SPECTRA_ARRAY_SIZE];

        for(int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            result[i] += spectraList[selectedElementSlot].GetSpectraArray()[i] * 3;
        }

        example = result;

        mySpectraPuzzleDisplay.UpdateExampleDisplay();
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
            
        }

        return new Color32(r, g, b, a);
    }

    public Color32 GetSpectraExampleColor(int slot)
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;
        byte a = 0;

        if (slot < Spectra.SPECTRA_ARRAY_SIZE)
        {
            r = spectraColorBySlot[slot].r;
            g = spectraColorBySlot[slot].g;
            b = spectraColorBySlot[slot].b;

            a = (byte)(example[slot] * 10);
            
            
            

            if (a > 0)
            {
                a += 15;
            }

        }

        return new Color32(r, g, b, a);
    }

    public Color32 GetAddedElementColor(int elementPosition, int slot)
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;
        byte a = 0;

        int[] elementalSpectraArray;

        switch (elementPosition)
        {
            case 1:
                elementalSpectraArray = primaryElement.GetSpectraArray();
                break;
            case 2:
                elementalSpectraArray = secondaryElement.GetSpectraArray();
                break;
            case 3:
                elementalSpectraArray = traceElement.GetSpectraArray();
                break;
            default:
                elementalSpectraArray = primaryElement.GetSpectraArray(); //this is a terrible hack.
                break;
        }

        if (slot < Spectra.SPECTRA_ARRAY_SIZE)
        {
            r = spectraColorBySlot[slot].r;
            g = spectraColorBySlot[slot].g;
            b = spectraColorBySlot[slot].b;

            a = (byte)(elementalSpectraArray[slot] * 10);




            if (a > 0)
            {
                a += 15;
            }

        }

        return new Color32(r, g, b, a);
    }

    public Spectra GetNextExampleSpectra()
    {
        if (selectedElementSlot < spectraList.Count - 1)
        {
            selectedElementSlot++;
        }
        else
        {
            selectedElementSlot = 0;
        }
        //mySpectraPuzzleDisplay.UpdateExampleDisplay(spectraList[selectedElementSlot]);
        UpdateExampleSpectra();
        GetAFeelForSpectraToTest();
        return spectraList[selectedElementSlot];
    }

    public Spectra GetPrevExampleSpectra()
    {
        if (selectedElementSlot > 0)
        {
            selectedElementSlot--;
        }
        else
        {
            selectedElementSlot = spectraList.Count - 1;
        }
        //mySpectraPuzzleDisplay.UpdateExampleDisplay(spectraList[selectedElementSlot]);
        UpdateExampleSpectra();
        GetAFeelForSpectraToTest();
        return spectraList[selectedElementSlot];
    }

    public override bool CheckSolution()
    {
        if (Enumerable.SequenceEqual(solution, combinedSpectra))
        {
            solved = true;
            Debug.Log("Puzzle Solved.");
            mySpectraPuzzleDisplay.ClearPuzzleDisplay();
            
            return true;
        }
        // Call error display here.
        mySpectraPuzzleDisplay.DisplayIncorrectGuess();
        Debug.Log("Puzzle not Solved.");
        return false;
    }

    public void AddSpectraToTest()
    {
        Spectra insertedElement = spectraList[selectedElementSlot];
        switch (numInserted)
        {
            case 0:
                SetPrimarySpectra(insertedElement);
                numInserted++;
                UpdateDisplaySpectra();
                break;
            case 1:
                SetSecondarySpectra(insertedElement);
                numInserted++;
                UpdateDisplaySpectra();
                break;
            case 2:
                SetTraceSpectra(insertedElement);
                numInserted++;
                UpdateDisplaySpectra();
                break;
            default:
                break;
        }
        GetAFeelForSpectraToTest();

    }

    // We don't want to commit right now...just get a feel for a solution.
    public void GetAFeelForSpectraToTest()
    {
        Spectra insertedElement = spectraList[selectedElementSlot];
        switch (numInserted)
        {
            case 0:
                SetPrimarySpectra(insertedElement);
                UpdateDisplaySpectra();
                break;
            case 1:
                SetSecondarySpectra(insertedElement);
                UpdateDisplaySpectra();
                break;
            case 2:
                SetTraceSpectra(insertedElement);
                UpdateDisplaySpectra();
                break;
            default:
                break;
        }
    }

    public void RemoveSpectra()
    {
        
        switch (numInserted)
        {
            case 0:
                break;
            case 1:
                numInserted -= 1;
                selectedElementSlot = spectraList.IndexOf(primaryElement);
                SetPrimarySpectra(none);
                SetSecondarySpectra(none);
                SetTraceSpectra(none);
                
                break;
            case 2:
                numInserted -= 1;
                selectedElementSlot = spectraList.IndexOf(secondaryElement);
                SetSecondarySpectra(none);
                SetTraceSpectra(none);
                break;
            case 3:
                numInserted -= 1;
                selectedElementSlot = spectraList.IndexOf(traceElement);
                SetTraceSpectra(none);
                break;
            default:
                break;
        }
        UpdateExampleSpectra();
        GetAFeelForSpectraToTest();
    }
}
