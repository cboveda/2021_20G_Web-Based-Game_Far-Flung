using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LabSpectraTest
{
    [UnityTest]
    public IEnumerator Test_SpectraCreation()
    {
        
        Spectra spectraFail1 = new Spectra("Fail1", new int[] { 0, 0, 0 });
        LogAssert.Expect(LogType.Assert, "Spectra was not of the right size.");
        Assert.AreNotEqual(spectraFail1.GetSpectraArray(), Spectra.SPECTRA_ARRAY_SIZE);

        Spectra spectra1 = new Spectra("Iron", new int[] { 0, 4, 4, 0, 0, 0, 4, 4, 4, 4, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0 });

        Assert.AreEqual(spectra1.GetSpectraArray().Length, Spectra.SPECTRA_ARRAY_SIZE);
        Assert.AreEqual(spectra1.GetSpectraName(), "Iron");

        yield return new WaitForSeconds(0.1f);



    }

    [UnityTest]
    public IEnumerator Test_SpectraPuzzleCreation()
    {
        GameObject spectraSolution = new GameObject("SpectraSolution");
        GameObject spectraAttempt = new GameObject("SpectraAttempt");
        GameObject spectraExample = new GameObject("SpectraExample");
        GameObject spectraPrimary = new GameObject("SpectraPrimary");
        GameObject spectraSecondary = new GameObject("SpectraSecondary");
        GameObject spectraTrace = new GameObject("SpectraTrace");
        GameObject elementName = new GameObject("ElementName");
        elementName.gameObject.AddComponent<Text>();
        GameObject puzzleHolder1 = new GameObject();
        GameObject spectraResponse = new GameObject("ResponseIndicator");
        spectraResponse.gameObject.AddComponent<Text>();

        elementName.gameObject.AddComponent<Text>();
        SpectraPuzzle puzzle1 = puzzleHolder1.AddComponent<SpectraPuzzle>();

        puzzle1.InitializeSpectraPuzzle("Puzzle1", 1);
        
        int[] puzzle1CombinedArraysAnswer = { 0, 10, 8, 0, 0, 12, 8, 8, 18, 8, 4, 0, 14, 2, 0, 10, 0, 4, 0, 0 };
        
        Assert.AreEqual(puzzle1CombinedArraysAnswer, puzzle1.solution);

        puzzle1.SetPrimarySpectra(SpectraPuzzle.iron);
        puzzle1.SetSecondarySpectra(SpectraPuzzle.gold);
        puzzle1.SetTraceSpectra(SpectraPuzzle.silver);
        puzzle1.UpdateDisplaySpectra();

        Assert.IsTrue(puzzle1.CheckSolution());

        yield return new WaitForSeconds(0.1f);
    }


}
