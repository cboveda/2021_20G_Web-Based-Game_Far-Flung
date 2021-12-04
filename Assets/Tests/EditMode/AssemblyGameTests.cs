using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AssemblyGameTests
{
    // [Test]
    // public void SlotDropTest()
    // {

        // // Setup a basic framework for the drop slot
        // GameObject dropSlotGameObject = new GameObject("DropSlot");
        // DropSlot dropSlot = dropSlotGameObject.AddComponent<DropSlot>();

        // GameObject dropGameObject = new GameObject("DropObject");
        // Collider dropGameObjectCollider = dropGameObject.AddComponent<BoxCollider>();

        // dropSlot.slotMatch = dropGameObject;
        // dropSlot.offset = Vector3.zero;
        // dropSlot.completionTextAsset = new TextAsset("Test");

        // GameObject textPanelGameObject = new GameObject("TextPanel");
        // TextPanel textPanel = textPanelGameObject.AddComponent<TextPanel>();
        // textPanel.textPanelButton = new GameObject("button").AddComponent<TextPanelButton>();
        // textPanel.mainText = (new GameObject()).AddComponent<Text>();
        // textPanelGameObject.SetActive(false);
        // Assert.NotNull(dropGameObject);
        // Assert.NotNull(dropSlot);
        // Assert.NotNull(textPanel);

        // //check that the text box shows up after dropping the correct object
        // dropSlot.OnTriggerStay(dropGameObjectCollider);
        // Assert.IsTrue(textPanel.gameObject.activeSelf);

        // //check that the text box does not show up for incorrect object
        // textPanel.gameObject.SetActive(false);
        // Collider random = new GameObject("Random Object").AddComponent<BoxCollider>();
        // dropSlot.OnTriggerStay(random);
        // Assert.IsFalse(textPanel.gameObject.activeSelf);

        // //check that the text box does not show up if no object is dropped
        // textPanel.gameObject.SetActive(false);
        // Collider nullCollider = null;
        // dropSlot.OnTriggerStay(nullCollider);
        // Assert.IsFalse(textPanel.gameObject.activeSelf);

        // //check IsCompleted shows true when the correct object is placed correctly
        // Assert.IsTrue(dropSlot.IsCompleted());

        // //check IsCompleted shows false when the correct object is not placed correctly
        // dropGameObject.transform.SetPositionAndRotation(Vector3.right, Quaternion.identity);
        // Assert.IsFalse(dropSlot.IsCompleted());

    // }


    // [Test]
    // public void TextPanelTest()
    // {
        // GameObject textPanelGameObject = new GameObject("TextPanel");
        // TextPanel textPanel = textPanelGameObject.AddComponent<TextPanel>();
        // textPanel.textPanelButton = new GameObject("button").AddComponent<TextPanelButton>();
        // textPanel.mainText = (new GameObject()).AddComponent<Text>();
        // textPanelGameObject.SetActive(false);

        // textPanel.ShowText(new TextAsset("Test Me"), () => { });
        // Assert.IsTrue(textPanel.gameObject.activeSelf);//textPanel should be set active
        // Assert.IsTrue(textPanel.mainText.text=="Test Me");//Make sure the text in text asset is shown correctly

        // textPanel.ShowText(null,() => { });
        // Assert.IsTrue(textPanel.mainText.text== "Error Text not found");//check error message
    // }
}
