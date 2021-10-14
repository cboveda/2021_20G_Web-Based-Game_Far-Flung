using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FlightpathTest
{
    [UnityTest]
    public IEnumerator Test_AttractorsAttractAffectedAttractors()
    {
        GameObject gameObject1 = new GameObject();
        gameObject1.AddComponent<Rigidbody>();
        gameObject1.GetComponent<Rigidbody>().useGravity = false;
        gameObject1.GetComponent<Rigidbody>().mass = 100f;
        gameObject1.AddComponent<Attractor>();
        gameObject1.GetComponent<Attractor>().Affected = true;
        gameObject1.GetComponent<Attractor>().Start();

        GameObject gameObject2 = new GameObject();
        gameObject2.AddComponent<Rigidbody>();
        gameObject2.GetComponent<Rigidbody>().useGravity = false;
        gameObject2.GetComponent<Rigidbody>().mass = 100f;
        gameObject2.AddComponent<Attractor>();
        gameObject2.GetComponent<Attractor>().Affected = true;
        gameObject2.GetComponent<Attractor>().Start();

        gameObject1.transform.position = Vector3.left;
        gameObject2.transform.position = Vector3.right;
        Vector3 initialDistance = gameObject1.transform.position - gameObject2.transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 distance = gameObject1.transform.position - gameObject2.transform.position;
        Assert.Less(distance.magnitude, initialDistance.magnitude);
    }

    [UnityTest]
    public IEnumerator Test_AttractorsDoNotAttractUnaffectedAttractors()
    {
        GameObject gameObject1 = new GameObject();
        gameObject1.AddComponent<Rigidbody>();
        gameObject1.GetComponent<Rigidbody>().useGravity = false;
        gameObject1.GetComponent<Rigidbody>().mass = 100f;
        gameObject1.AddComponent<Attractor>();
        gameObject1.GetComponent<Attractor>().Affected = true;
        gameObject1.GetComponent<Attractor>().Start();

        GameObject gameObject2 = new GameObject();
        gameObject2.AddComponent<Rigidbody>();
        gameObject2.GetComponent<Rigidbody>().useGravity = false;
        gameObject2.GetComponent<Rigidbody>().mass = 100f;
        gameObject2.AddComponent<Attractor>();
        gameObject2.GetComponent<Attractor>().Affected = false;
        gameObject2.GetComponent<Attractor>().Start();

        gameObject2.transform.position = Vector3.right;
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(Vector3.right, gameObject2.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_PathFollowerBeginsAtStartPoint()
    {
        GameObject path = new GameObject();
        path.AddComponent<Path>();
        var pathComponent = path.GetComponent<Path>();
        
        GameObject startPoint = new GameObject();
        GameObject startDirection = new GameObject();
        GameObject endPoint = new GameObject();
        GameObject endDirection = new GameObject();

        startPoint.transform.position = Vector2.right;
        startDirection.transform.position = Vector2.down;
        endPoint.transform.position = Vector2.left;
        endDirection.transform.position = Vector2.up;

        startPoint.transform.parent = path.transform;
        startDirection.transform.parent = path.transform;
        endPoint.transform.parent = path.transform;
        endDirection.transform.parent = path.transform;

        pathComponent.StartPoint = startPoint.transform;
        pathComponent.StartDirection = startDirection.transform;
        pathComponent.EndPoint = endPoint.transform;
        pathComponent.EndDirection = endDirection.transform;

        GameObject pathFollower = new GameObject();
        pathFollower.AddComponent<PathFollower>();
        var pathFollowerComponent = pathFollower.GetComponent<PathFollower>();
        pathFollowerComponent.Path = path.transform;
        pathFollowerComponent.Speed = 1.0f;
        pathFollowerComponent.Start();

        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual((Vector3) Vector2.right, pathFollower.transform.position);
    }

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
    //     yield return new WaitForSeconds(2f);
    //     Debug.Log(Vector2.left);
    //     Debug.Log((Vector2) pathFollower.transform.position);
    //     Assert.AreEqual(Vector2.left, (Vector2) pathFollower.transform.position);
    // }
}
 