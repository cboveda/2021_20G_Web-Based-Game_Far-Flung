using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor;

public class ScanningTests {

    GameObject satellite;
    GameObject neutronSignal;
    GameObject HUD1;
    GameObject HUD2;

    GameObject terrainController;
    GameObject terrainSatellite;

    Gradient surfaceGrad;
    GameObject sigSpawner;
    AnimationCurve basePerlinCurve;

    [SetUp]
    public void SetUp() {

        // Setup for flight control
        satellite = new GameObject();
        neutronSignal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scanning/Prefabs/Signal_Variant.prefab");

        HUD1 = new GameObject();
        HUD1.AddComponent<Text>();

        HUD2 = new GameObject();   
        HUD2.AddComponent<Text>();

        satellite.AddComponent<FlightControl>();
        satellite.GetComponent<FlightControl>().hozSlerpSpped  = 0.2f;
        satellite.GetComponent<FlightControl>().vertSlerpSpeed = 0.2f;
        //satellite.GetComponent<FlightControl>().altitude = HUD1.GetComponent<Text>();
        //satellite.GetComponent<FlightControl>().signals = HUD2.GetComponent<Text>();

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

        //terrainSatellite.GetComponent<FlightControl>().altitude = HUD1.GetComponent<Text>();
        //terrainSatellite.GetComponent<FlightControl>().signals = HUD2.GetComponent<Text>();

        terrainSatellite.transform.position = new Vector3( 0, 200, 0 );

        sigSpawner = new GameObject();
        sigSpawner.AddComponent<SignalSpawner>();

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
    }

    [UnityTest]
    public IEnumerator Test_FlightControl() {

        var satelliteInstance = Object.Instantiate(satellite, new Vector3(0, 0, 0), Quaternion.identity);
        var neutronInst1 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 5), Quaternion.identity);
        var neutronInst2 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 10), Quaternion.identity);

        yield return new WaitForSeconds(2.0f);
       
        //Assert.True( satellite.GetComponent<FlightControl>().signals.text.Equals("2/10") );
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
}