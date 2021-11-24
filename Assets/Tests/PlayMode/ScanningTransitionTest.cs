using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class ScanningTransitionTest {

    GameObject satellite;
    GameObject neutronSignal;
    GameObject alt_needle;
    GameObject miss_needle;

    FadeDriverStub fdStub;
    FadeBannerStub fbStubWin;
    FadeBannerStub fbStubLose;

    // A Test behaves as an ordinary method
    [SetUp]
    public void SetUp() {

        fdStub = new FadeDriverStub();
        fbStubWin = new FadeBannerStub();
        fbStubLose = new FadeBannerStub();

        neutronSignal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scanning/Prefabs/Signal_Variant.prefab");

        alt_needle = new GameObject();
        miss_needle = new GameObject();
        satellite = new GameObject();

        satellite.AddComponent<FlightControl>();
        satellite.GetComponent<FlightControl>().limit = 3;

        satellite.GetComponent<FlightControl>().fadeDriver = fdStub;
        satellite.GetComponent<FlightControl>().winFadeBanner = fbStubWin;
        satellite.GetComponent<FlightControl>().winFadeBanner = fbStubLose;

        satellite.GetComponent<FlightControl>().altitudeNeedle = alt_needle;
        satellite.GetComponent<FlightControl>().signalsNeedle = miss_needle;

        satellite.AddComponent<BoxCollider>();
        satellite.GetComponent<BoxCollider>().isTrigger = true;
        satellite.GetComponent<BoxCollider>().size = new Vector3(4.62987995f,1.10379791f,0.544322073f);
        satellite.GetComponent<BoxCollider>().center = new Vector3(-0.1f,0.2f,0.2f);

        satellite.AddComponent<Rigidbody>();
        satellite.GetComponent<Rigidbody>().isKinematic = true;

        satellite.transform.position = new Vector3( 0, 0, 0 );
    }

    [UnityTest]
    public IEnumerator Test_ScanninTransitionForFlightControl()
    {
        int index1 = SceneManager.GetActiveScene().buildIndex;

        var sig1 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 3), Quaternion.identity);
        var sig2 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 4), Quaternion.identity);
        var sig3 = Object.Instantiate(neutronSignal, new Vector3(0, 0, 5), Quaternion.identity);
        yield return new WaitForSeconds( 4f );

        //LogAssert.Expect(LogType.Error, new Regex(".*") ); // if this test is run in isolation use to prevent scene transition errors

        Assert.True( fdStub.testSet() );
        Assert.True( fbStubWin.testSet() );
        Assert.False( fbStubLose.testSet() );   
    }
}

public class FadeDriverStub : FadeDriver {

    bool isSet;

    public FadeDriverStub() {
        isSet = false;
    }

    public override void TriggerFade() {
        isSet = true;
    }

    public bool testSet() {
        return isSet;
    }
}

public class FadeBannerStub : FadeBanner {

    bool isSet;

    public FadeBannerStub() {
        isSet = false;
    }

    public override void TriggerFade() {
        isSet = true;
    }

    public bool testSet() {
        return isSet;
    }
}