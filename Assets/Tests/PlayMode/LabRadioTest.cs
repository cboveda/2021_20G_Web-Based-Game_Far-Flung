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

    //[UnityTest]
    //public IEnumerator Test_AttractorsDoNotAttractUnaffectedAttractors()
    //{
    //    GameObject gameObject1 = new GameObject();
    //    gameObject1.AddComponent<Rigidbody>();
    //    gameObject1.GetComponent<Rigidbody>().useGravity = false;
    //    gameObject1.GetComponent<Rigidbody>().mass = 100f;
    //    gameObject1.AddComponent<Attractor>();
    //    gameObject1.GetComponent<Attractor>().Affected = true;
    //    gameObject1.GetComponent<Attractor>().Start();

    //    GameObject gameObject2 = new GameObject();
    //    gameObject2.AddComponent<Rigidbody>();
    //    gameObject2.GetComponent<Rigidbody>().useGravity = false;
    //    gameObject2.GetComponent<Rigidbody>().mass = 100f;
    //    gameObject2.AddComponent<Attractor>();
    //    gameObject2.GetComponent<Attractor>().Affected = false;
    //    gameObject2.GetComponent<Attractor>().Start();

    //    gameObject2.transform.position = Vector3.right;
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.AreEqual(Vector3.right, gameObject2.transform.position);
    //}

    //[UnityTest]
    //public IEnumerator Test_PathFollowerBeginsAtStartPoint()
    //{
    //    GameObject path = new GameObject();
    //    path.AddComponent<Path>();
    //    var pathComponent = path.GetComponent<Path>();

    //    GameObject startPoint = new GameObject();
    //    GameObject startDirection = new GameObject();
    //    GameObject endPoint = new GameObject();
    //    GameObject endDirection = new GameObject();

    //    startPoint.transform.position = Vector2.right;
    //    startDirection.transform.position = Vector2.down;
    //    endPoint.transform.position = Vector2.left;
    //    endDirection.transform.position = Vector2.up;

    //    startPoint.transform.parent = path.transform;
    //    startDirection.transform.parent = path.transform;
    //    endPoint.transform.parent = path.transform;
    //    endDirection.transform.parent = path.transform;

    //    pathComponent.StartPoint = startPoint.transform;
    //    pathComponent.StartDirection = startDirection.transform;
    //    pathComponent.EndPoint = endPoint.transform;
    //    pathComponent.EndDirection = endDirection.transform;

    //    GameObject pathFollower = new GameObject();
    //    pathFollower.AddComponent<PathFollower>();
    //    var pathFollowerComponent = pathFollower.GetComponent<PathFollower>();
    //    pathFollowerComponent.Path = path.transform;
    //    pathFollowerComponent.Speed = 1.0f;
    //    pathFollowerComponent.Start();

    //    yield return new WaitForSeconds(0.1f);
    //    Assert.AreEqual((Vector3)Vector2.right, pathFollower.transform.position);
    //}

    // [UnityTest]
    // public IEnumerator Test_PathFollowerEndsAtEndPoint()
    // {
    //     GameObject path = new GameObject();
    //     path.AddComponent<Path>();
    //     var pathComponent = path.GetComponent<Path>();

    //     GameObject startPoint = new GameObject();
    //     GameObject startDirection = new GameObject();
    //     GameObject endPoint = new GameObject();
    //     GameObject endDirection = new GameObject();

    //     startPoint.transform.position = Vector2.right;
    //     startDirection.transform.position = Vector2.down;
    //     endPoint.transform.position = Vector2.left;
    //     endDirection.transform.position = Vector2.up;

    //     startPoint.transform.parent = path.transform;
    //     startDirection.transform.parent = path.transform;
    //     endPoint.transform.parent = path.transform;
    //     endDirection.transform.parent = path.transform;

    //     pathComponent.StartPoint = startPoint.transform;
    //     pathComponent.StartDirection = startDirection.transform;
    //     pathComponent.EndPoint = endPoint.transform;
    //     pathComponent.EndDirection = endDirection.transform;

    //     GameObject pathFollower = new GameObject();
    //     pathFollower.AddComponent<PathFollower>();
    //     var pathFollowerComponent = pathFollower.GetComponent<PathFollower>();
    //     pathFollowerComponent.Path = path.transform;
    //     pathFollowerComponent.Speed = 100.0f;
    //     pathFollowerComponent.Start();
    //     pathFollowerComponent.BeginMovement();
    //     while(!pathFollowerComponent.IsUnlocked()) 
    //     {
    //         yield return new WaitForFixedUpdate();
    //     }    
    //     Assert.AreEqual(Vector2.left, (Vector2) pathFollower.transform.position);
    // }
}
