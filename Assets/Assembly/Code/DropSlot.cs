using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour
{

    public Vector3 offset;
    public GameObject slotMatch; // set to premanently lock the object when placed
    public TextAsset completionTextAsset;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter slot");
        if (other.GetComponent<DragObject>())
        {
            other.GetComponent<DragObject>().currentSlot = this;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit slot");
        if (other.GetComponent<DragObject>())
        {
            other.GetComponent<DragObject>().currentSlot = null;
        }
    }

    public void placeObjectInSlot(GameObject gameObject)
    {
        // Debug.Log("triggered");
        if (gameObject.GetComponent<DragObject>())
        {
            bool defined = true;
            //for unspecified slots set the current object being placed to 
            if (!slotMatch)
            {
                Debug.Log("undefined slot");
                slotMatch = gameObject;
                defined = false;
            }
            if (gameObject == slotMatch)
            {
                Debug.Log("Drop in SLot");
                gameObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
                if (gameObject.GetComponent<Rigidbody>())
                {
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }


                //try to find the text panel for a popup and check for set texdt if found show the appropriate text
                if (Resources.FindObjectsOfTypeAll<TextPanel>().Length > 0 && completionTextAsset)
                {
                    Resources.FindObjectsOfTypeAll<TextPanel>()[0].ShowText(completionTextAsset, CallParentCompletion);
                }
                else
                {
                    CallParentCompletion();
                }
                Debug.Log("defined:" + defined);
                //if the slot is defined make it so the object can't be moved again and the slot is invisible
                if (defined)
                {
                    Debug.Log("defined slot running");
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                //make the slot invisible
                GetComponent<Renderer>().forceRenderingOff = true;

                if(GetComponent<CompletionSlot>()) GetComponent<CompletionSlot>().completeSlot();

            }
        }
    }

    public void CallParentCompletion()
    {
        if (transform.parent.GetComponent<Completion>() != null)
        {
            if(transform.parent.GetComponent<Completion>().IsCompleted()){
                transform.parent.GetComponent<Completion>().nextScene.Invoke();
            }
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
