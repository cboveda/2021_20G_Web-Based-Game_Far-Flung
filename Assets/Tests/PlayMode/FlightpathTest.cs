using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FlightpathTest
{
    [UnityTest]
    public IEnumerator Test_LaunchComponentDoesNotLaunchOnFirstFrame()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<Launch>();
        gameObject.AddComponent<Attractor>();
        yield return null;
        Assert.False(gameObject.GetComponent<Launch>().HasLaunched());
    }

    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesWithDoLaunch()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<Launch>();
        gameObject.AddComponent<Attractor>();
        gameObject.GetComponent<Launch>().DoLaunch();
        yield return null;
        Assert.False(gameObject.GetComponent<Launch>().HasLaunched());
    }
}
