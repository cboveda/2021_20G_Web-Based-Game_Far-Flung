using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FlightpathLaunchTest
{
    private GameObject gameObject;
    
    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<Launch>();
        gameObject.AddComponent<Attractor>();
        gameObject.GetComponent<Launch>().Start();
        gameObject.GetComponent<Launch>().SetAngle(0f);
        gameObject.GetComponent<Launch>().SetPower(1.0f);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameObject);
    }

    
    [UnityTest]
    public IEnumerator Test_LaunchComponentDoesNotLaunchOnFirstFrame()
    {
        yield return new WaitForFixedUpdate();
        Assert.True(gameObject.GetComponent<Rigidbody>().velocity.magnitude == 0);
    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesWithDoLaunch()
    {
        gameObject.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        Assert.True(gameObject.GetComponent<Rigidbody>().velocity.magnitude != 0);

    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesAtSetAngle()
    {
        float targetAngle = 45f;
        gameObject.GetComponent<Launch>().SetAngle(targetAngle);
        gameObject.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        float angle = Vector3.Angle(gameObject.GetComponent<Rigidbody>().velocity, Vector3.right);
        Assert.AreEqual(targetAngle, angle, 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesAtSetPower()
    {
        float targetPower = 45f;
        gameObject.GetComponent<Launch>().SetPower(targetPower);
        gameObject.GetComponent<Launch>().DoLaunch();
        yield return new WaitForFixedUpdate();
        float power = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        Assert.AreEqual(targetPower, power, 0.1f);
    }
}
