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
        yield return new WaitForFixedUpdate();  // Load scene

        DialogController dc = Object.FindObjectOfType<DialogController>();
        yield return new WaitForSeconds(3f);

        dc.dg.BeginPlayingDialog();  // Speed up display
        yield return new WaitForSeconds(3f);

        dc.dg.BeginPlayingDialog();  // Advance to dg position 2
        yield return new WaitForSeconds(3f);

        Assert.AreEqual(true, dc.angleSlider.interactable);
        dc.angleSlider.value = 42;
        yield return new WaitForSeconds(3f);

        Assert.AreEqual(false, dc.angleSlider.interactable);
        Assert.AreEqual(true, dc.powerSlider.interactable);
        dc.powerSlider.value = 84;
        yield return new WaitForSeconds(3f);

        Assert.AreEqual(true, dc.launchButton.interactable);
        dc.launchManager.OnLaunchButtonClicked();
        while (!dc.launchManager.hasStopped())  // Travelling to planet
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(3f);  // Wait for Mars explosion

        Assert.AreEqual(false, dc.launchButton.interactable);
        dc.dg.BeginPlayingDialog();  // Speed up display
        yield return new WaitForSeconds(3f);

        dc.dg.BeginPlayingDialog();  // Advance to dg position 2
        yield return new WaitForSeconds(3f);  // Unlock reset button

        Assert.AreEqual(true, dc.resetButton.interactable);
        dc.launchManager.OnResetButtonClicked();
        yield return new WaitForSeconds(3f);  // Unlock all

        Assert.AreEqual(true, dc.resetButton.interactable);
        Assert.AreEqual(true, dc.launchButton.interactable);
        Assert.AreEqual(true, dc.angleSlider.interactable);
        Assert.AreEqual(true, dc.powerSlider.interactable);
        yield return new WaitForSeconds(3f);
        dc.launchManager.OnLaunchButtonClicked();

        yield return new WaitForSeconds(3f);
        dc.launchManager.OnResetButtonClicked();
    }
}