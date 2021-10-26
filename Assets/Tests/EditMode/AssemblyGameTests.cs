using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AssemblyGameTests
{
    [Test]
    public void SlotDropTest()
    {

        // Setup a basic framework for the drop slot
        GameObject dropSlotGameObject = new GameObject("DropSlot");
        DropSlot dropSlot = dropSlotGameObject.AddComponent<DropSlot>();
        dropSlotGameObject.AddComponent<RectTransform>();

        GameObject dropGameObject = new GameObject("DropObject");
        dropGameObject.AddComponent<RectTransform>();

        dropSlot.slotMatch = dropGameObject;
        dropSlot.slotOffset = Vector2.zero;
        dropSlot.completionTextAsset = new TextAsset("Test");

        GameObject textPanelGameObject = new GameObject("TextPanel");
        TextPanel textPanel = textPanelGameObject.AddComponent<TextPanel>();
        Assert.AreEqual(typeof(TextPanel), textPanel.GetType());
        textPanel.textPanelButton = new GameObject("button").AddComponent<TextPanelButton>();
        textPanel.mainText = (new GameObject()).AddComponent<Text>();
        textPanelGameObject.SetActive(false);
        Assert.NotNull(dropGameObject);
        Assert.NotNull(dropSlot);
        Assert.NotNull(textPanel);

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        //check that the text box shows up after dropping the correct object
        pointerEventData.pointerDrag = dropGameObject;
        dropSlot.OnDrop(pointerEventData);
        Assert.IsTrue(textPanel.gameObject.activeSelf);

        //check that the text box does not show up for incorrect object
        textPanel.gameObject.SetActive(false);
        pointerEventData.pointerDrag = new GameObject("Random Object");
        dropSlot.OnDrop(pointerEventData);
        Assert.IsFalse(textPanel.gameObject.activeSelf);

        //check that the text box does not show up if no object is dropped
        textPanel.gameObject.SetActive(false);
        pointerEventData.pointerDrag = null;
        dropSlot.OnDrop(pointerEventData);
        Assert.IsFalse(textPanel.gameObject.activeSelf);

        //check IsCompleted shows true when the correct object is placed correctly
        Assert.IsTrue(dropSlot.IsCompleted());

        //check IsCompleted shows false when the correct object is not placed correctly
        dropGameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.right;
        Assert.IsFalse(dropSlot.IsCompleted());

    }


    [Test]
    public void TextPanelTest()
    {
        GameObject textPanelGameObject = new GameObject("TextPanel");
        TextPanel textPanel = textPanelGameObject.AddComponent<TextPanel>();
        textPanel.textPanelButton = new GameObject("button").AddComponent<TextPanelButton>();
        textPanel.mainText = (new GameObject()).AddComponent<Text>();
        textPanelGameObject.SetActive(false);

        textPanel.ShowText(new TextAsset("Test Me"), () => { });
        Assert.IsTrue(textPanel.gameObject.activeSelf);//textPanel should be set active
        Assert.IsTrue(textPanel.mainText.text=="Test Me");//Make sure the text in text asset is shown correctly

        textPanel.ShowText(null,() => { });
        Assert.IsTrue(textPanel.mainText.text== "Error Text not found");//check error message
    }
}
