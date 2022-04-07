using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LabMainTest
{
    [UnityTest]
    public IEnumerator Test_LabFlow()
    {

        yield return new WaitForSeconds(0.1f);



    }

    //[UnityTest]
    //public IEnumerator Test_SpectraPuzzleCreation()
    //{
    //    GameObject spectraSolution = new GameObject("SpectraSolution");
    //    GameObject spectraAttempt = new GameObject("SpectraAttempt");
    //    GameObject spectraExample = new GameObject("SpectraExample");
    //    GameObject spectraPrimary = new GameObject("SpectraPrimary");
    //    GameObject spectraSecondary = new GameObject("SpectraSecondary");
    //    GameObject spectraTrace = new GameObject("SpectraTrace");
    //    GameObject elementName = new GameObject("ElementName");
    //    elementName.gameObject.AddComponent<Text>();
    //    GameObject puzzleHolder1 = new GameObject();
    //    GameObject spectraResponse = new GameObject("ResponseIndicator");
    //    spectraResponse.gameObject.AddComponent<Text>();

    //    elementName.gameObject.AddComponent<Text>();
    //    SpectraPuzzle puzzle1 = puzzleHolder1.AddComponent<SpectraPuzzle>();

    //    puzzle1.InitializeSpectraPuzzle("Puzzle1", 0);

    //    int[] puzzle1CombinedArraysAnswer = { 0, 13, 12, 0, 4, 2, 18, 20, 14, 12, 10, 0, 7, 0, 2, 2, 0, 3, 0, 0 };

    //    Assert.AreEqual(puzzle1.solution, puzzle1CombinedArraysAnswer);

    //    puzzle1.SetPrimarySpectra(SpectraPuzzle.iron);
    //    puzzle1.SetSecondarySpectra(SpectraPuzzle.gold);
    //    puzzle1.SetTraceSpectra(SpectraPuzzle.silver);
    //    puzzle1.UpdateDisplaySpectra();

    //    Assert.IsTrue(puzzle1.CheckSolution());

    //    yield return new WaitForSeconds(0.1f);
    //}


}
