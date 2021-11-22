using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Flightpath;

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
        pathFollowerComponent.Path = path;
        pathFollowerComponent.Speed = 1.0f;
        pathFollowerComponent.Start();

        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual((Vector3) Vector2.right, pathFollower.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_SceneTransitionIntro() 
    {
        SceneManager.LoadScene("1_FlightpathIntro", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.4f);
        var controller = GameObject.FindObjectOfType<TimelineController>();
        controller.Director_Played(null);
        controller.Director_Stopped(null);
        yield return new WaitForSeconds(0.4f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "2_Flightpath");
    }

    [UnityTest]
    public IEnumerator Test_SceneTransitionToOutro() 
    {
        SceneManager.LoadScene("2_Flightpath", LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
        var manager = GameObject.FindObjectOfType<LaunchManager>();
        manager.OnAsteroidCollisionDetected();
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "3_FlightpathOutro");
        var controller = GameObject.FindObjectOfType<TimelineControllerOutro>();
        controller.Director_Stopped(null);
        yield return new WaitForSeconds(0.4f);
    }

    [UnityTest]
    public IEnumerator Test_SceneTransitionOutroToNext() 
    {
        SceneManager.LoadScene("3_FlightpathOutro", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.4f);
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        var controller = GameObject.FindObjectOfType<TimelineControllerOutro>();
        controller.Director_Stopped(null);
        yield return new WaitForSeconds(0.4f);
        Assert.AreEqual(SceneManager.GetActiveScene().buildIndex, next);
    }

    [UnityTest]
    public IEnumerator Test_SatelliteAsteroidCollision()
    {
        SceneManager.LoadScene("2_Flightpath", LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
        var manager = GameObject.FindObjectOfType<LaunchManager>();
        manager.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        var asteroid = GameObject.FindWithTag("FlightpathAsteroid");
        var satellite = GameObject.FindWithTag("FlightpathSatellite");
        satellite.transform.position = asteroid.transform.position;
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "3_FlightpathOutro");
        var controller = GameObject.FindObjectOfType<TimelineControllerOutro>();
        controller.Director_Stopped(null);
        yield return new WaitForSeconds(0.4f);
    }

    [UnityTest]
    public IEnumerator Test_SatelliteBoundaryCollision()
    {
        SceneManager.LoadScene("2_Flightpath", LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
        var manager = GameObject.FindObjectOfType<LaunchManager>();
        manager.OnLaunchButtonClicked();
        yield return new WaitForSeconds(1.0f);
        var satellite = GameObject.FindWithTag("FlightpathSatellite");
        satellite.transform.position = new Vector3(3000, 3000, 3000);
        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "2_Flightpath");
        Assert.AreEqual(false, manager.SatellitePath.Active);
        Assert.AreEqual(true, manager.LoseText.enabled);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerResetButton()
    {
        SceneManager.LoadScene("2_Flightpath", LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
        var manager = GameObject.FindObjectOfType<LaunchManager>();
        var satellite = GameObject.FindWithTag("FlightpathSatellite");
        Vector3 originalPosition = satellite.transform.position;
        manager.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        Vector3 currentPosition = satellite.transform.position;
        Assert.AreNotEqual(currentPosition, originalPosition);
        manager.OnResetButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(originalPosition, satellite.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_PathFader()
    {
        SceneManager.LoadScene("2_Flightpath", LoadSceneMode.Single);
        yield return new WaitForSeconds(1.0f);
        var manager = GameObject.FindObjectOfType<LaunchManager>();
        var pathDrawing = GameObject.FindObjectOfType<SatellitePathDrawing>();
        manager.OnLaunchButtonClicked();
        yield return new WaitForSeconds(1.0f);
        manager.OnResetButtonClicked();
        yield return new WaitForFixedUpdate();
        GameObject child = pathDrawing.transform.GetChild(0).gameObject;
        manager.OnLaunchButtonClicked();
        yield return new WaitForSeconds(5.0f);
        Assert.False(child);
    }
}
 