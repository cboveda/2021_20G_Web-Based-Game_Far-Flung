using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using DialogMaker;

public class AssemblyTest {

    GameObject game_driver;
    DialogGenerator_Stub counter;

    [SetUp]
    public void SetUp() {

        game_driver = new GameObject();
        game_driver.AddComponent<AssemblyGameDriver>();

        game_driver.GetComponent<AssemblyGameDriver>().RotationSpeed = 5;
        game_driver.GetComponent<AssemblyGameDriver>().TotalComponents = 2;

        counter = new DialogGenerator_Stub();

    }

    [UnityTest]
    public IEnumerator Test_GameDriver() {

        Transform base_transform = game_driver.transform;

        yield return new WaitUntil(() => {return (game_driver.transform.eulerAngles == new Vector3(1, 1, 0)); });

        Assert.AreEqual(base_transform, game_driver.transform);

        yield return null;
    }


    public IEnumerator Test_AssemblyTransition() {


        yield return null;

    }
}

class DialogGenerator_Stub : DialogGenerator {

    public int calls_counter;

    void init() {
        calls_counter = 0;
    }

    new public void BeginPlayingDialog() {
        calls_counter++;
    }
}
