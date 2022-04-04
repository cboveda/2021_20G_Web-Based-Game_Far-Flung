using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DialogMaker;

public class AssemblyTest {

    GameObject game_driver;
    GameObject counter;
    GameObject conveyor;

    [SetUp]
    public void SetUp() {

        // game driver setup
        game_driver = new GameObject();
        game_driver.AddComponent<AssemblyGameDriver>();

        game_driver.GetComponent<AssemblyGameDriver>().RotationSpeed = 5;
        game_driver.GetComponent<AssemblyGameDriver>().TotalComponents = 2;

        // game driver - dialog generator interaction stub
        counter = new GameObject();
        counter.AddComponent<DialogGenerator_Stub>();
        counter.GetComponent<DialogGenerator_Stub>().dialogContainer = ScriptableObject.CreateInstance<DialogScriptableObject>();
        counter.GetComponent<DialogGenerator_Stub>().init();

        game_driver.GetComponent<AssemblyGameDriver>().introDiag = counter.GetComponent<DialogGenerator_Stub>();
        game_driver.GetComponent<AssemblyGameDriver>().outroDiag = counter.GetComponent<DialogGenerator_Stub>();
    
        // game driver - conveyor system interaction
        conveyor = new GameObject();
        conveyor.AddComponent<ConveyorPickup_Stub>();
        conveyor.GetComponent<ConveyorPickup_Stub>().init();
    
        game_driver.GetComponent<AssemblyGameDriver>().DragDropSystem = conveyor.GetComponent<ConveyorPickup_Stub>();

    }

    [UnityTest]
    public IEnumerator Test_GameDriver() {

        Transform base_transform = game_driver.transform;

        yield return new WaitForSeconds(0.5f);

        Assert.AreNotEqual(base_transform, game_driver.transform);

    }

    [UnityTest]
    public IEnumerator Test_AssemblyTransition() {

        yield return new WaitForSeconds(0.5f);

        // test that start dialog is called
        Assert.AreEqual(counter.GetComponent<DialogGenerator_Stub>().calls_counter, 1);

        game_driver.GetComponent<AssemblyGameDriver>().CompleteObject();

        // test that evict is called
        Assert.True(conveyor.GetComponent<ConveyorPickup_Stub>().evit_call_status);

        game_driver.GetComponent<AssemblyGameDriver>().CompleteObject();

        // test outro is called
        Assert.AreEqual(counter.GetComponent<DialogGenerator_Stub>().calls_counter, 2);

    }
}

class DialogGenerator_Stub : DialogGenerator {

    public int calls_counter;

    void Awake() {}

    void Start() {}

    public void init() {
        calls_counter = 0;
    }

    public override bool BeginPlayingDialog() {

        Debug.Log("asdfASDFasdffdsafasd");

        calls_counter++;
        return true;
    }
}

class ConveyorPickup_Stub : ConveyorPickup {

    public bool evit_call_status;

    public void init() {

        evit_call_status = false;

    }

    new public void EvictHeldObject() {

        evit_call_status = true;
    }
}