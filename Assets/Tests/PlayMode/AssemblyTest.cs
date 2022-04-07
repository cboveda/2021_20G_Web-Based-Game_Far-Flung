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

        

        // move object

        // drop object

        // pickup object

        // insert object into ghost

        yield return null;

    }
}