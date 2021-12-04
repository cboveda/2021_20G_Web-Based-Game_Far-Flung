using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionSlot : MonoBehaviour
{
    public void completeSlot(){
        DropSlot slot = GetComponent<DropSlot>();
        GameObject slotMatch = slot.slotMatch;
        slotMatch.GetComponent<Collider>().enabled = false;
        slotMatch.GetComponent<Renderer>().forceRenderingOff = true;
        GetComponent<Renderer>().forceRenderingOff = true;
    }
}
