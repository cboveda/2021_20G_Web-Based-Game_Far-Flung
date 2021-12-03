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

        //GameObject sineObject1 = new GameObject();
        //sineObject1.AddComponent<SineWave>();
        //Assert.False(sineObject1.GetComponent<SineWave>().isInitialized);
        //sineObject1.GetComponent<SineWave>().InitializeSineWave();
        //Assert.True(sineObject1.GetComponent<SineWave>().isInitialized);
        //Assert.Zero(sineObject1.GetComponent<SineWave>().frequency);
        //Assert.Zero(sineObject1.GetComponent<SineWave>().amplitude);

        //RadioPuzzleParams myParams1 = new RadioPuzzleParams();
        //float testFrequency = 1.9f;
        //float testAmplitude = 0.5f;
        //myParams1.Frequency = testFrequency;
        //myParams1.Amplitude = testAmplitude;

        //sineObject1.GetComponent<SineWave>().SetParameters(myParams1);

        //Material myMat = (Material)Resources.Load("CRTDarkGreen", typeof(Material));

        //sineObject1.GetComponent<SineWave>().SetMaterial(myMat);


        //Assert.AreEqual(sineObject1.GetComponent<SineWave>().frequency, testFrequency);
        //Assert.AreEqual(sineObject1.GetComponent<SineWave>().amplitude, testAmplitude);

        //float sampledDisplayFrequency = sineObject1.GetComponent<SineWave>().displayFrequency;
        //float sampledDisplayAmplitude = sineObject1.GetComponent<SineWave>().displayAmplitude;

        //yield return new WaitForSeconds(0.1f);

        //Assert.Greater(sineObject1.GetComponent<SineWave>().displayFrequency, sampledDisplayFrequency);
        //Assert.Greater(sineObject1.GetComponent<SineWave>().displayAmplitude, sampledDisplayAmplitude);

        //yield return new WaitForSeconds(4.0f);

        //sampledDisplayFrequency = Mathf.Round(sineObject1.GetComponent<SineWave>().displayFrequency * 10) * 0.1f;
        //sampledDisplayAmplitude = Mathf.Round(sineObject1.GetComponent<SineWave>().displayAmplitude * 10) * 0.1f;

        //Assert.AreEqual(sampledDisplayFrequency, testFrequency);
        //Assert.AreEqual(sampledDisplayAmplitude, testAmplitude);

        //RadioPuzzleParams myParams2 = new RadioPuzzleParams();
        //float testFrequency2 = 0.2f;
        //float testAmplitude2 = 0.2f;
        //myParams2.Frequency = testFrequency2;
        //myParams2.Amplitude = testAmplitude2;

        //sineObject1.GetComponent<SineWave>().SetParameters(myParams2);

        //yield return new WaitForSeconds(4.0f);

        //sampledDisplayFrequency = Mathf.Round(sineObject1.GetComponent<SineWave>().displayFrequency * 10) * 0.1f;
        //sampledDisplayAmplitude = Mathf.Round(sineObject1.GetComponent<SineWave>().displayAmplitude * 10) * 0.1f;

        //Assert.AreEqual(sampledDisplayFrequency, testFrequency2);
        //Assert.AreEqual(sampledDisplayAmplitude, testAmplitude2);

        //sineObject1.GetComponent<SineWave>().DestroyWave();

        //yield return new WaitForSeconds(0.1f);

        //Assert.IsNull(sineObject1.GetComponent<SineWave>());



    }

    [UnityTest]
    public IEnumerator Test_SpectraPuzzleCreation()
    {
        GameObject spectraSolution = new GameObject("SpectraSolution");
        GameObject spectraAttempt = new GameObject("SpectraAttempt");
        GameObject puzzleHolder1 = new GameObject();
        GameObject spectraExample = new GameObject("SpectraExample");
        GameObject spectraPrimary = new GameObject("SpectraPrimary");
        GameObject spectraSecondary = new GameObject("SpectraSecondary");
        GameObject spectraTrace = new GameObject("SpectraTrace");
        GameObject elementName = new GameObject("ElementName");
        elementName.gameObject.AddComponent<Text>();
        SpectraPuzzle puzzle1 = puzzleHolder1.AddComponent<SpectraPuzzle>();

        puzzle1.InitializeSpectraPuzzle("Puzzle1", 0);

        int[] puzzle1CombinedArraysAnswer = { 0, 13, 12, 0, 4, 2, 18, 20, 14, 12, 10, 0, 7, 0, 2, 2, 0, 3, 0, 0 };

        Assert.AreEqual(puzzle1.solution, puzzle1CombinedArraysAnswer);

        puzzle1.SetPrimarySpectra(SpectraPuzzle.iron);
        puzzle1.SetSecondarySpectra(SpectraPuzzle.gold);
        puzzle1.SetTraceSpectra(SpectraPuzzle.silver);

        Assert.IsTrue(puzzle1.CheckSolution());

        yield return new WaitForSeconds(0.1f);
    }


}
