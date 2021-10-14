using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class FlightpathLaunchTest
{
    private GameObject satellite;
    private GameObject launchManager;

    [SetUp]
    public void Setup()
    {
        satellite = new GameObject();
        satellite.AddComponent<Rigidbody>();
        satellite.GetComponent<Rigidbody>().useGravity = false;
        satellite.AddComponent<Launch>();
        satellite.AddComponent<Attractor>();
        satellite.GetComponent<Launch>().Start();
        satellite.GetComponent<Launch>().SetAngle(0f);
        satellite.GetComponent<Launch>().SetPower(1.0f);

        launchManager = new GameObject();
        launchManager.AddComponent<LaunchManager>();
        var launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.Satellite = satellite;

        GameObject angleSlider = new GameObject();
        angleSlider.AddComponent<Slider>();
        angleSlider.GetComponent<Slider>().minValue = 10f;
        GameObject powerSlider = new GameObject();
        powerSlider.AddComponent<Slider>();
        powerSlider.GetComponent<Slider>().minValue = 10f;
        launchManagerComponent.AngleSlider = angleSlider.GetComponent<Slider>();
        launchManagerComponent.PowerSlider = angleSlider.GetComponent<Slider>();

        launchManagerComponent.Start();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(satellite);
        Object.Destroy(launchManager);
    }


    [UnityTest]
    public IEnumerator Test_LaunchComponentDoesNotLaunchOnFirstFrame()
    {
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(0, satellite.GetComponent<Rigidbody>().velocity.magnitude);
    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesWithDoLaunch()
    {
        satellite.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(0, satellite.GetComponent<Rigidbody>().velocity.magnitude);

    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesAtSetAngle()
    {
        float targetAngle = 45f;
        satellite.GetComponent<Launch>().SetAngle(targetAngle);
        satellite.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        float angle = Vector3.Angle(satellite.GetComponent<Rigidbody>().velocity, Vector3.right);
        Assert.AreEqual(targetAngle, angle, 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesAtSetPower()
    {
        float targetPower = 45f;
        satellite.GetComponent<Launch>().SetPower(targetPower);
        satellite.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        float power = satellite.GetComponent<Rigidbody>().velocity.magnitude;
        Assert.AreEqual(targetPower, power, 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerInitializesSatelliteAngle()
    {
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(10f, satellite.GetComponent<Launch>().GetAngle(), 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerInitializesSatellitePower()
    {
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(10f, satellite.GetComponent<Launch>().GetPower(), 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerLaunchesSatellite()
    {
        var launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(0, satellite.GetComponent<Rigidbody>().velocity.magnitude);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerChangesSatelliteAngle()
    {
        var launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.OnAngleSliderChanged(20f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(20f, satellite.GetComponent<Launch>().GetAngle(), 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerChangesSatellitePower()
    {
        var launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.OnPowerSliderChanged(20f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(20f, satellite.GetComponent<Launch>().GetPower(), 0.1f);
    }
}
