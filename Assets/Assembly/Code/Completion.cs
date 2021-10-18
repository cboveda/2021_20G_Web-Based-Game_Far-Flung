using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completion : MonoBehaviour
{
    public TextAsset completionTextAsset;
    public void CheckCompletion()
    {
        foreach (Transform child in transform)
        {
            //Implement completion check
            if (child != null && child.GetComponent<DropSlot>() != null)
            {
                bool slotCompleted = child.GetComponent<DropSlot>().IsCompleted();
                Debug.Log(child.name + " is " + slotCompleted);
                if (!slotCompleted)
                {
                    return; //does nothing if one of the slots is incomplete
                }
            }
            
        } 
        TextPanel textPanel = Resources.FindObjectsOfTypeAll<TextPanel>()[0];
        textPanel.ShowText(completionTextAsset, OpenNextScene);
    }

    public void OpenNextScene()
    {
        Debug.Log("Open Next Scene");
    }
}
