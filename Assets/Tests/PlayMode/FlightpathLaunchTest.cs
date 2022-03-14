using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Flightpath;
using DialogMaker;

public class FlightpathLaunchTest
{
    private const string DG_PREFAB_PATH = "Prefabs/DialogMaker-GlobalClick";
    private GameObject satellite;
    private GameObject planet;
    private GameObject asteroid;
    private GameObject launchManager;
    private LaunchManager launchManagerComponent;
    private GameObject path;
    private GameObject pathFollower;
    private GameObject angleSlider;
    private GameObject powerSlider;
    private GameObject pathDrawing;
    private GameObject arrow;
    private GameObject startPoint;
    private GameObject startDirection;
    private GameObject endPoint;
    private GameObject endDirection;
    private GameObject boundaryLeft;
    private GameObject boundaryRight;
    private GameObject boundaryTop;
    private GameObject boundaryBottom;


    [SetUp]
    public void Setup()
    {
        setupSatellite();
        setupPlanet();
        setupAsteroid();
        setupBoundaries();
        setupLaunchManager();
        setupSliders();
        setupPaths();
        setupArrow();
        setupPathFollower();


    }

    private void setupArrow()
    {
        arrow = new GameObject();
        arrow.AddComponent<Trajectory>();
        arrow.GetComponent<Trajectory>().Satellite = satellite.GetComponent<Launch>();
        arrow.AddComponent<SpriteRenderer>();
        launchManagerComponent.TrajectoryArrow = arrow.GetComponent<Trajectory>();
        launchManagerComponent.Start();
    }

    private void setupPathFollower()
    {
        pathFollower = new GameObject();
        pathFollower.AddComponent<PathFollower>();
        var pathFollowerComponent = pathFollower.GetComponent<PathFollower>();
        pathFollowerComponent.Path = path;
        pathFollowerComponent.Speed = 1.0f;
        pathFollowerComponent.Start();
    }

    private void setupPaths()
    {
        pathDrawing = new GameObject();
        pathDrawing.AddComponent<SatellitePathDrawing>();
        pathDrawing.GetComponent<SatellitePathDrawing>().Start();
        launchManagerComponent.SatellitePath = pathDrawing.GetComponent<SatellitePathDrawing>();

        path = new GameObject();
        path.AddComponent<Path>();
        var pathComponent = path.GetComponent<Path>();

        startPoint = new GameObject();
        startDirection = new GameObject();
        endPoint = new GameObject();
        endDirection = new GameObject();

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
    }

    private void setupSliders()
    {
        angleSlider = new GameObject();
        angleSlider.AddComponent<Slider>();
        angleSlider.GetComponent<Slider>().minValue = 10f;

        powerSlider = new GameObject();
        powerSlider.AddComponent<Slider>();
        powerSlider.GetComponent<Slider>().minValue = 10f;
        launchManagerComponent.AngleSlider = angleSlider.GetComponent<Slider>();
        launchManagerComponent.PowerSlider = angleSlider.GetComponent<Slider>();
    }

    private void setupLaunchManager()
    {
        launchManager = new GameObject();
        launchManager.AddComponent<LaunchManager>();
        launchManagerComponent = launchManager.GetComponent<LaunchManager>();
        launchManagerComponent.Satellite = satellite;
        launchManagerComponent.enableMarsDialog();
        satellite.GetComponent<SatelliteCollision>().launchManager = launchManagerComponent;
        launchManagerComponent.DialogGeneratorPrefab = Resources.Load<GameObject>(DG_PREFAB_PATH);
        var dialogSO = ScriptableObject.CreateInstance<DialogScriptableObject>();
        dialogSO.dialogs = new Dialog[] { new Dialog("test", DialogMaker.RobotCharacter.None) };
        launchManagerComponent.BotScripts = new DialogScriptableObject[] { dialogSO };
        launchManagerComponent.TopScripts = new DialogScriptableObject[] { dialogSO };
        launchManagerComponent.RightScripts = new DialogScriptableObject[] { dialogSO };
        launchManagerComponent.LeftScripts = new DialogScriptableObject[] { dialogSO };
        launchManagerComponent.MarsScripts = new DialogScriptableObject[] { dialogSO };
    }

    private void setupBoundaries()
    {
        boundaryLeft = new GameObject();
        boundaryLeft.name = "LeftBoundary";
        boundaryLeft.tag = "FlightpathBounds";
        boundaryLeft.AddComponent<BoxCollider>();
        boundaryLeft.GetComponent<BoxCollider>().isTrigger = true;
        boundaryLeft.transform.position = Vector3.left * 1000;

        boundaryRight = new GameObject();
        boundaryRight.name = "RightBoundary";
        boundaryRight.tag = "FlightpathBounds";
        boundaryRight.AddComponent<BoxCollider>();
        boundaryRight.GetComponent<BoxCollider>().isTrigger = true;
        boundaryRight.transform.position = Vector3.right * 1000;

        boundaryTop = new GameObject();
        boundaryTop.name = "TopBoundary";
        boundaryTop.tag = "FlightpathBounds";
        boundaryTop.AddComponent<BoxCollider>();
        boundaryTop.GetComponent<BoxCollider>().isTrigger = true;
        boundaryTop.transform.position = Vector3.up * 1000;

        boundaryBottom = new GameObject();
        boundaryBottom.name = "BottomBoundary";
        boundaryBottom.tag = "FlightpathBounds";
        boundaryBottom.AddComponent<BoxCollider>();
        boundaryBottom.GetComponent<BoxCollider>().isTrigger = true;
        boundaryBottom.transform.position = Vector3.down * 1000;
    }

    private void setupAsteroid()
    {
        asteroid = new GameObject();
        asteroid.tag = "FlightpathAsteroid";
        asteroid.AddComponent<BoxCollider>();
        asteroid.GetComponent<BoxCollider>().isTrigger = true;
        asteroid.transform.position = Vector3.left * 100;

    }

    private void setupPlanet()
    {
        planet = new GameObject();
        planet.AddComponent<Rigidbody>();
        planet.GetComponent<Rigidbody>().useGravity = false;
        planet.GetComponent<Rigidbody>().mass = 100f;
        planet.AddComponent<Attractor>();
        planet.GetComponent<Attractor>().Affected = false;
        planet.tag = "FlightpathMars";
        planet.AddComponent<BoxCollider>();
        planet.GetComponent<BoxCollider>().isTrigger = true;
        planet.transform.position = Vector3.right * 100;
        planet.GetComponent<Attractor>().Start();
    }

    private void setupSatellite()
    {
        satellite = new GameObject();
        satellite.AddComponent<Rigidbody>();
        satellite.GetComponent<Rigidbody>().useGravity = false;
        satellite.AddComponent<Launch>();
        satellite.AddComponent<Attractor>();
        satellite.AddComponent<BoxCollider>();
        satellite.GetComponent<BoxCollider>().isTrigger = true;
        satellite.AddComponent<SatelliteCollision>();
        satellite.AddComponent<ParticleSystem>();
        satellite.GetComponent<Launch>().Start();
        satellite.GetComponent<Launch>().SetAngle(0f);
        satellite.GetComponent<Launch>().SetPower(1.0f);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(satellite);
        Object.Destroy(planet);
        Object.Destroy(asteroid);
        Object.Destroy(launchManager);
        Object.Destroy(path);
        Object.Destroy(pathFollower);
        Object.Destroy(angleSlider);
        Object.Destroy(powerSlider);
        Object.Destroy(pathDrawing);
        Object.Destroy(arrow);
        Object.Destroy(startPoint);
        Object.Destroy(startDirection);
        Object.Destroy(endPoint);
        Object.Destroy(endDirection);
        Object.Destroy(boundaryLeft);
        Object.Destroy(boundaryRight);
        Object.Destroy(boundaryTop);
        Object.Destroy(boundaryBottom);
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
        Vector3 expectedPosition = new Vector3(expectedX, expectedY, 0);
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
        Vector3 expectedPosition = new Vector3(expectedX, expectedY, 0);
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
        Assert.AreEqual((Vector3)Vector2.right, pathFollower.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_AttractorsDoNotAttractUnaffectedAttractors()
    {
        Vector3 expected = planet.transform.position;
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(expected, planet.transform.position);
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionLeftBoundary()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        //yield return new WaitForFixedUpdate();
        satellite.transform.position = boundaryLeft.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionRightBoundary()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        satellite.transform.position = boundaryRight.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionTopBoundary()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        satellite.transform.position = boundaryTop.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionBottomBoundary()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        satellite.transform.position = boundaryBottom.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionPlanet()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        launchManagerComponent.enableMarsDialog();
        yield return new WaitForFixedUpdate();
        satellite.transform.position = planet.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }

    [UnityTest]
    public IEnumerator Test_SatelliteCollisionAsteroid()
    {
        launchManagerComponent.OnLaunchButtonClicked();
        yield return new WaitForFixedUpdate();
        satellite.transform.position = asteroid.transform.position;
        satellite.GetComponent<Rigidbody>().velocity = Vector3.zero;
        satellite.GetComponent<Attractor>().Affected = false;
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(true, launchManagerComponent.hasStopped());
    }
}
