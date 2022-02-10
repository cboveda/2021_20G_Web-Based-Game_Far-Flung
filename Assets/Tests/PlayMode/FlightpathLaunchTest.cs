using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Flightpath;

public class FlightpathLaunchTest
{
    private GameObject satellite;
    private GameObject planet;
    private GameObject launchManager;
    private LaunchManager launchManagerComponent;
    private GameObject path;
    private GameObject pathFollower;
    private GameObject angleSlider;
    private GameObject powerSlider;
    private GameObject pathDrawing;
    private GameObject arrow;
    private GameObject startPoint;

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

        planet = new GameObject();
        planet.AddComponent<Rigidbody>();
        planet.GetComponent<Rigidbody>().useGravity = false;
        planet.GetComponent<Rigidbody>().mass = 100f;
        planet.AddComponent<Attractor>();
        planet.GetComponent<Attractor>().Affected = false;
        planet.transform.position = Vector3.right;
        planet.GetComponent<Attractor>().Start();

        launchManager = new GameObject();
        launchManager.AddComponent<LaunchManager>();
        launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.Satellite = satellite;

        angleSlider = new GameObject();
        angleSlider.AddComponent<Slider>();
        angleSlider.GetComponent<Slider>().minValue = 10f;
        
        powerSlider = new GameObject();
        powerSlider.AddComponent<Slider>();
        powerSlider.GetComponent<Slider>().minValue = 10f;
        launchManagerComponent.AngleSlider = angleSlider.GetComponent<Slider>();
        launchManagerComponent.PowerSlider = angleSlider.GetComponent<Slider>();
        
        pathDrawing = new GameObject();
        pathDrawing.AddComponent<SatellitePathDrawing>();
        pathDrawing.GetComponent<SatellitePathDrawing>().Start();
        launchManagerComponent.SatellitePath = pathDrawing.GetComponent<SatellitePathDrawing>();

        arrow = new GameObject();
        arrow.AddComponent<Trajectory>();
        arrow.GetComponent<Trajectory>().Satellite = satellite.GetComponent<Launch>();
        arrow.AddComponent<SpriteRenderer>();
        launchManagerComponent.TrajectoryArrow = arrow.GetComponent<Trajectory>();
        launchManagerComponent.Start();

        path = new GameObject();
        path.AddComponent<Path>();
        var pathComponent = path.GetComponent<Path>();
        
        startPoint = new GameObject();
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

        pathFollower = new GameObject();
        pathFollower.AddComponent<PathFollower>();
        var pathFollowerComponent = pathFollower.GetComponent<PathFollower>();
        pathFollowerComponent.Path = path;
        pathFollowerComponent.Speed = 1.0f;
        pathFollowerComponent.Start();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(satellite);
        Object.Destroy(planet);
        Object.Destroy(launchManager);
        Object.Destroy(path);
        Object.Destroy(pathFollower);
        Object.Destroy(angleSlider);
        Object.Destroy(powerSlider);
        Object.Destroy(pathDrawing);
        Object.Destroy(arrow);
        Object.Destroy(startPoint);
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
    public IEnumerator Test_LaunchManagerLaunchesAllMovableObjects()
    {
        Vector3 satelliteStart = satellite.transform.position;
        Vector3 pathFollowerStart = pathFollower.transform.position;
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(0, satellite.GetComponent<Rigidbody>().velocity.magnitude);
        Assert.AreNotEqual(satelliteStart, satellite.transform.position);
        Assert.AreNotEqual(pathFollowerStart, pathFollower.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerResetsAllMovableObjects()
    {
        Vector3 satelliteStart = satellite.transform.position;
        Vector3 pathFollowerStart = pathFollower.transform.position;
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        launchManagerComponent.OnResetButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(0, satellite.GetComponent<Rigidbody>().velocity.magnitude);
        Assert.AreEqual(satelliteStart, satellite.transform.position);
        Assert.AreEqual(pathFollowerStart, pathFollower.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerChangesSatelliteAngle()
    {
        launchManagerComponent.OnAngleSliderChanged(20f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(20f, satellite.GetComponent<Launch>().GetAngle(), 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_LaunchManagerChangesSatellitePower()
    {
        launchManagerComponent.OnPowerSliderChanged(20f);
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(20f, satellite.GetComponent<Launch>().GetPower(), 0.1f);
    }

    [UnityTest]
    public IEnumerator Test_SatelliteStartsAtDefinedLocation()
    {
        float expectedX = 15f;
        float expectedY = 30f;
        Vector3 expectedPosition = new Vector3 (expectedX, expectedY, 0);
        satellite.GetComponent<Launch>().StartX = expectedX;
        satellite.GetComponent<Launch>().StartY = expectedY;
        satellite.GetComponent<Launch>().InitializePosition();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(expectedPosition, satellite.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_SatelliteResetsToDefinedLocation()
    {
        float expectedX = 1f;
        float expectedY = 1f;
        Vector3 expectedPosition = new Vector3 (expectedX, expectedY, 0);
        satellite.GetComponent<Launch>().StartX = expectedX;
        satellite.GetComponent<Launch>().StartY = expectedY;
        Vector3 newPosition = new Vector3(15, 30, 0);
        satellite.transform.position = newPosition;
        satellite.GetComponent<Launch>().ResetLaunch();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(expectedPosition, satellite.transform.position);
    }

     [UnityTest]
    public IEnumerator Test_PathFollowerBeginsAtStartPoint()
    {
        yield return new WaitForFixedUpdate();
        Assert.AreEqual((Vector3) Vector2.right, pathFollower.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_AttractorsDoNotAttractUnaffectedAttractors()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(Vector3.right, planet.transform.position);  
    }
}
