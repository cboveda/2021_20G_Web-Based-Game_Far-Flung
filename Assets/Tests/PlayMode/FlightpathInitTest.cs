using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Flightpath;

public class FlightpathInitTest
{
    [UnityTest]
    public IEnumerator Test_FlightpathControllerInits()
    {
        SceneManager.LoadScene("2_Flightpath");
        yield return new WaitForSeconds(2f);

        Controller c = Object.FindObjectOfType<Controller>();
        Assert.AreNotEqual(null, c.earth);
        Assert.AreNotEqual(null, c.mars);
        Assert.AreNotEqual(null, c.asteroid);
        Assert.AreNotEqual(null, c.sun);
        Assert.AreNotEqual(null, c.satellite);
        Assert.AreNotEqual(null, c.earth.GetComponent<PathFollower>().Path);
        Assert.AreNotEqual(null, c.mars.GetComponent<PathFollower>().Path);
        Assert.AreNotEqual(null, c.asteroid.GetComponent<PathFollower>().Path);
        Assert.AreEqual(c.eventSystem.GetComponent<LaunchManager>().Satellite, c.satellite);
    }
}