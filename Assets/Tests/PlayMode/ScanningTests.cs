using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class ScanningTests {

    GameObject satellite;
    GameObject neutronSignal;
    GameObject alt_needle;
    GameObject miss_needle;

    GameObject terrainController;
    GameObject terrainSatellite;

    Gradient surfaceGrad;
    GameObject sigSpawner;
    AnimationCurve basePerlinCurve;

    GameObject camera;

    [SetUp]
    public void SetUp() {

        // Setup for flight control
        satellite = new GameObject();
        neutronSignal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scanning/Prefabs/Signal_Variant.prefab");

        alt_needle = new GameObject();
        miss_needle = new GameObject();

        satellite.AddComponent<FlightControl>();
        satellite.GetComponent<FlightControl>().hozSlerpSpped  = 0.2f;
        satellite.GetComponent<FlightControl>().vertSlerpSpeed = 0.2f;
        satellite.GetComponent<FlightControl>().altitudeNeedle = alt_needle;
        satellite.GetComponent<FlightControl>().signalsNeedle = miss_needle;

        satellite.AddComponent<BoxCollider>();
        satellite.GetComponent<BoxCollider>().isTrigger = true;
        satellite.GetComponent<BoxCollider>().size = new Vector3(4.62987995f,1.10379791f,0.544322073f);
        satellite.GetComponent<BoxCollider>().center = new Vector3(-0.1f,0.2f,0.2f);

        satellite.AddComponent<Rigidbody>();
        satellite.GetComponent<Rigidbody>().isKinematic = true;

        satellite.transform.position = new Vector3( 100, 100, 100 );

        // Setup for terraincontroller
        terrainSatellite = new GameObject();
        terrainSatellite.AddComponent<FlightControl>();
        terrainSatellite.GetComponent<FlightControl>().altitudeNeedle = alt_needle;
        terrainSatellite.GetComponent<FlightControl>().signalsNeedle = miss_needle;

        terrainSatellite.transform.position = new Vector3( 0, 200, 0 );

        sigSpawner = new GameObject();
        sigSpawner.AddComponent<SignalSpawner>();
        sigSpawner.GetComponent<SignalSpawner>().spawnFrequency = 3;
        sigSpawner.GetComponent<SignalSpawner>().prefabSignal = neutronSignal;

        basePerlinCurve = AnimationCurve.Constant(0f, 1f, 1f);
        surfaceGrad = new Gradient();

        terrainController = new GameObject();
        terrainController.SetActive(false);
        terrainController.AddComponent<TerrainController>();
        
        terrainController.GetComponent<TerrainController>().surfaceGrad = surfaceGrad;
        terrainController.GetComponent<TerrainController>().sigSpawner = sigSpawner.GetComponent<SignalSpawner>();
        terrainController.GetComponent<TerrainController>().basePerlinCurve = basePerlinCurve;
        terrainController.GetComponent<TerrainController>().satellite = terrainSatellite;
        
        terrainController.GetComponent<TerrainController>().tileDim = 10;
        terrainController.GetComponent<TerrainController>().renderDistZ = 40;
        terrainController.GetComponent<TerrainController>().renderDistX = 20;

        terrainController.SetActive(true);

        // set up camera
        camera = new GameObject();
        camera.AddComponent<CameraFollow>();
        camera.GetComponent<CameraFollow>().lead = terrainSatellite;
        camera.GetComponent<CameraFollow>().smoothTime = 0f;

    }

    [UnityTest]
    public IEnumerator Test_FlightControl() {

        var satelliteInstance = Object.Instantiate(satellite, new Vector3(0, 0, 0), Quaternion.identity);
        var neutronInst1 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 5), Quaternion.identity);
        var neutronInst2 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 10), Quaternion.identity);

        yield return new WaitForSeconds(2.0f);
       
        Assert.AreEqual( satelliteInstance.GetComponent<FlightControl>().signals_collected, 2 );
    } 

    [UnityTest]
    public IEnumerator Test_TerrainConroller() {

        terrainSatellite.GetComponent<FlightControl>().speed = 0f;

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual( 25, terrainController.GetComponent<TerrainController>().tileDict.Count );

        terrainSatellite.transform.position = new Vector3( 0, 100, 200 );

        yield return new WaitForSeconds(1.0f);

        Assert.AreEqual( 25, terrainController.GetComponent<TerrainController>().tileDict.Count );
    }
    
    [UnityTest]
    public IEnumerator Test_TestCameraFollow() {

        Vector3 delta = camera.transform.position - ( terrainSatellite.transform.position + new Vector3( 0, 4, -10 ) );
        Assert.AreEqual( delta.x, 0, 1.0f );
        Assert.AreEqual( delta.y, 0, 1.0f );
        Assert.AreEqual( delta.z, 0, 1.0f );
        yield return null;
    }
}