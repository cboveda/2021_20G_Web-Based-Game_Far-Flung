using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour
{

    public Vector3 offset;
    void OnTriggerStay(Collider other)
    {
        if(!Input.GetMouseButton(0))
        {
            Debug.Log("Collision");
            other.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
            // other.enabled = false;
        }
    }

    // public Vector2 slotOffset;
    // public GameObject slotMatch;
    // public TextAsset completionTextAsset;

    // public void OnDrop(PointerEventData eventData)
    // {
    //     if (eventData.pointerDrag != null && eventData.pointerDrag.gameObject == slotMatch)
    //     {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
    //             GetComponent<RectTransform>().anchoredPosition + slotOffset;
    //         Debug.Log("User has placed " + slotMatch.name);
    //         Resources.FindObjectsOfTypeAll<TextPanel>()[0].ShowText(completionTextAsset, CallParentCompletion);
            

    //     }
    // }

    // public void CallParentCompletion()
    // {
    //     if (transform.parent.GetComponent<Completion>() != null)
    //     {
    //         transform.parent.GetComponent<Completion>().CheckCompletion();
    //     }
    //     else
    //     {
    //         Debug.Log("Parent does not have a completion script");
    //     }
    // }

    // public bool IsCompleted()
    // {
    //     return (slotMatch.GetComponent<RectTransform>().anchoredPosition ==
    //            GetComponent<RectTransform>().anchoredPosition + slotOffset);
    // }
}
