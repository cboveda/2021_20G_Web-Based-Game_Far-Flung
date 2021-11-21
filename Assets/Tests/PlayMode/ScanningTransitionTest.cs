using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

public class ScanningTransitionTest {

    GameObject satellite;
    GameObject neutronSignal;
    GameObject alt_needle;
    GameObject miss_needle;

    FadeDriver fadeDriver;
    FadeBanner fadeBanner;
    Animator animator;
    AnimatorController controller;

    // A Test behaves as an ordinary method
    [SetUp]
    public void SetUp() {

        animator = new Animator();
        controller = new AnimatorController();
        controller.AddParameter( "FadeIn", AnimatorControllerParameterType.Trigger );

        animator.runtimeAnimatorController = controller;

        fadeDriver = AssetDatabase.LoadAssetAtPath<FadeDriver>("Assets/Scanning/Animations/FadeEffect.prefab");
        fadeBanner = new GameObject().AddComponent<FadeBanner>();
        fadeBanner.GetComponent<FadeBanner>().animator = animator;

        satellite = new GameObject();
        neutronSignal = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scanning/Prefabs/Signal_Variant.prefab");

        alt_needle = new GameObject();
        miss_needle = new GameObject();

        satellite.AddComponent<FlightControl>();
        satellite.GetComponent<FlightControl>().limit = 3;
        satellite.GetComponent<FlightControl>().fadeDriver = fadeDriver;
        satellite.GetComponent<FlightControl>().bannerFader = fadeBanner;
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
        yield return new WaitForSeconds( 2f );

        int index2 = SceneManager.GetActiveScene().buildIndex;

        Debug.Log( index1 );
        Debug.Log( index2 );

    }
}
