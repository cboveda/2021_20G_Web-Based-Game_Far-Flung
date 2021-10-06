using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Flightpath
{
    [UnityTest]
    public IEnumerator Test_LaunchComponentLaunchesOnFirstFrame()
    {
        GameObject gameObject = new GameObject();
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<Launch>();
        yield return null;
        Assert.True(gameObject.GetComponent<Launch>().hasLaunched());
    }
}
