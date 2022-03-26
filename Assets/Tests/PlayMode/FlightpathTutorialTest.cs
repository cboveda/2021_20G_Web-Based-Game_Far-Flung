using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Flightpath;
using DialogMaker;

public class FlightpathTutorialTest
{
    [UnityTest]
    public IEnumerator Test_UIElementsActivatedPerTutorialPhase()
    {
        SceneManager.LoadScene("2_Flightpath");
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        DialogController Dc = Object.FindObjectOfType<DialogController>();

        Assert.AreEqual(false, Dc.AngleSlider.interactable);
        Dc.Dg.BeginPlayingDialog();
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(true, Dc.AngleSlider.interactable);
        Dc.AngleSlider.value = 42;
        yield return new WaitForFixedUpdate();

        Assert.AreEqual(false, Dc.AngleSlider.interactable);
        Assert.AreEqual(false, Dc.PowerSlider.interactable);
        Dc.PowerSlider.value = 84;
        yield return new WaitForFixedUpdate();




    }
}