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

    public int AssemblyIntroDialogClickCount = 11;

    [UnityTest]
    public IEnumerator Test_AssemblyGamePlay() {

        SceneManager.LoadScene("Assembly");
        yield return new WaitForSeconds(0.5f);

        GameObject intro_dialog = GameObject.Find("AssemblyIntro");

        Assert.NotNull(intro_dialog);

        // opening clicks for dialog
        for (int i = 0; i < AssemblyIntroDialogClickCount; i++) {

            intro_dialog.GetComponent<DialogGenerator>().BtnDialogButton.onClick.Invoke();
        }

        MouseLook.HideMenu();

        // grab object
        GameObject active_body = GameObject.Find("BodyTexture Variant(Clone)");
        yield return new WaitForSeconds(0.5f);

        Assert.NotNull(active_body);

        GameObject player = GameObject.Find("Assembly Camera");
        Assert.NotNull(player);

        while (!active_body.activeInHierarchy) {
            Debug.Log("waiting for body...");
            yield return new WaitForSeconds(1f);
        }

        Vector3 diff = active_body.transform.position - player.transform.position;

        Vector3 start = active_body.transform.position;

        RaycastHit hit;
                
        Assert.True(Physics.Raycast(player.transform.position, diff, out hit, 50f));

        player.GetComponent<ConveyorPickup>().TestHook_PickupObject(hit);

        Assert.NotNull(player.GetComponent<ConveyorPickup>().heldObj);

        yield return new WaitForSeconds(3f);

        // object now at cursor position

        Assert.True(start != active_body.transform.position);

        // drop object and pickup object

        player.GetComponent<ConveyorPickup>().TestHook_DropObject();

        Assert.Null(player.GetComponent<ConveyorPickup>().heldObj);
        
        Vector3 diff_two = active_body.transform.position - player.transform.position;

        RaycastHit hit_two;
                
        Assert.True(Physics.Raycast(player.transform.position, diff_two, out hit_two, 50f));

        player.GetComponent<ConveyorPickup>().TestHook_PickupObject(hit);

        Assert.NotNull(player.GetComponent<ConveyorPickup>().heldObj);

        // drop the object again so it can be moved around

        player.GetComponent<ConveyorPickup>().TestHook_DropObject();

        // simulate pickup

        active_body.GetComponent<ConveyorObject>().OnPickUp();

        // insert object into ghost
        GameObject ghost_body = GameObject.Find("GhostBody");

        yield return new WaitForSeconds(0.5f);

        Assert.NotNull(ghost_body);

        active_body.transform.position = ghost_body.transform.position;

        yield return new WaitForSeconds(1f); // allow interaction

        Assert.False(active_body.GetComponent<ConveyorObject>().PlaceInModel());

    }
}