using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
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

        // yield return new WaitUntil(() => {return (game_driver.transform.eulerAngles == new Vector3(1, 1, 0)); });

        Assert.AreEqual(base_transform, game_driver.transform);

        yield return null;
    }


    public IEnumerator Test_AssemblyTransition() {


        yield return null;

    }
}

class DialogGenerator_Stub : DialogGenerator {

    public int calls_counter;

    public void init() {
        calls_counter = 0;
    }

    new public void BeginPlayingDialog() {
        calls_counter++;
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