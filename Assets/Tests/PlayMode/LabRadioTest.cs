using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LabRadioTest
{
    [UnityTest]
    public IEnumerator Test_SineWaveCreation()
    {
        GameObject sineObject1 = new GameObject();
        sineObject1.AddComponent<SineWave>();
        Assert.False(sineObject1.GetComponent<SineWave>().isInitialized);
        sineObject1.GetComponent<SineWave>().InitializeSineWave();
        Assert.True(sineObject1.GetComponent<SineWave>().isInitialized);
        Assert.Zero(sineObject1.GetComponent<SineWave>().frequency);
        Assert.Zero(sineObject1.GetComponent<SineWave>().amplitude);

        RadioPuzzleParams myParams1 = new RadioPuzzleParams();
        float testFrequency = 1.9f;
        float testAmplitude = 0.5f;
        myParams1.Frequency = testFrequency;
        myParams1.Amplitude = testAmplitude;

        sineObject1.GetComponent<SineWave>().SetParameters(myParams1);

        Material myMat = (Material)Resources.Load("CRTDarkGreen", typeof(Material));

        sineObject1.GetComponent<SineWave>().SetMaterial(myMat);
        

        Assert.AreEqual(sineObject1.GetComponent<SineWave>().frequency, testFrequency);
        Assert.AreEqual(sineObject1.GetComponent<SineWave>().amplitude, testAmplitude);

        float sampledDisplayFrequency = sineObject1.GetComponent<SineWave>().displayFrequency;
        float sampledDisplayAmplitude = sineObject1.GetComponent<SineWave>().displayAmplitude;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(sineObject1.GetComponent<SineWave>().displayFrequency, sampledDisplayFrequency);
        Assert.Greater(sineObject1.GetComponent<SineWave>().displayAmplitude, sampledDisplayAmplitude);

        yield return new WaitForSeconds(4.0f);

        sampledDisplayFrequency = Mathf.Round(sineObject1.GetComponent<SineWave>().displayFrequency * 10) * 0.1f;
        sampledDisplayAmplitude = Mathf.Round(sineObject1.GetComponent<SineWave>().displayAmplitude * 10) * 0.1f;

        Assert.AreEqual(sampledDisplayFrequency, testFrequency);
        Assert.AreEqual(sampledDisplayAmplitude, testAmplitude);

        RadioPuzzleParams myParams2 = new RadioPuzzleParams();
        float testFrequency2 = 0.2f;
        float testAmplitude2 = 0.2f;
        myParams2.Frequency = testFrequency2;
        myParams2.Amplitude = testAmplitude2;

        sineObject1.GetComponent<SineWave>().SetParameters(myParams2);

        yield return new WaitForSeconds(4.0f);

        sampledDisplayFrequency = Mathf.Round(sineObject1.GetComponent<SineWave>().displayFrequency * 10) * 0.1f;
        sampledDisplayAmplitude = Mathf.Round(sineObject1.GetComponent<SineWave>().displayAmplitude * 10) * 0.1f;

        Assert.AreEqual(sampledDisplayFrequency, testFrequency2);
        Assert.AreEqual(sampledDisplayAmplitude, testAmplitude2);

        sineObject1.GetComponent<SineWave>().DestroyWave();

        yield return new WaitForSeconds(0.1f);

        Assert.IsNull(sineObject1.GetComponent<SineWave>());



    }

    
}
