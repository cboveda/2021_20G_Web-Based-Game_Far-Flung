using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour
{

    public Vector3 offset;
    public GameObject slotMatch;
    public TextAsset completionTextAsset;

    void OnTriggerStay(Collider other)
    {
        Debug.Log("triggered");
        if(other.gameObject.GetComponent<DragObject>() && other.gameObject.GetComponent<DragObject>().isHeld==false)
        {
            if(other.gameObject == slotMatch){
                Debug.Log("Drop in SLot");
                other.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
                Resources.FindObjectsOfTypeAll<TextPanel>()[0].ShowText(completionTextAsset, CallParentCompletion);
                other.enabled = false;
            }
        }
    }

    public void CallParentCompletion()
    {
        if (transform.parent.GetComponent<Completion>() != null)
        {
            transform.parent.GetComponent<Completion>().CheckCompletion();
        }
        else
        {
            Debug.Log("Parent does not have a completion script");
        }
    }

    public bool IsCompleted()
    {
        bool completed = (slotMatch.transform.position == transform.position + offset);
        // Debug.Log(transform.name + " " + completed);
        return completed;
    }
}
