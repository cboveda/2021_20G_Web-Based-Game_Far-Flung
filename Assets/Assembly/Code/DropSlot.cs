using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, Completion
{

    public Vector3 offset;
    public GameObject slotMatch; // set to premanently lock the object when placed
    public bool undefinedSlot;
    public TextAsset completionTextAsset;

    private GameObject slotObject;

    void Start(){
        undefinedSlot = slotMatch?false: true;
    }

    void Update(){
        if(slotObject == null) return;
        DragObject dragObject = slotObject.GetComponent<DragObject>();
        // make sure the object can still be moved by the player
        if(dragObject==null || dragObject.isHeld) return;

        slotObject.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
    }

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
        if (other.GetComponent<DragObject>() && other.GetComponent<DragObject>().currentSlot==this)
        {
            other.GetComponent<DragObject>().currentSlot = null;
        }
        GetComponent<Renderer>().forceRenderingOff = false;
        slotObject = null;
    }

    public void placeObjectInSlot(GameObject gameObject)
    {
        slotObject =  gameObject;
        // Debug.Log("triggered");
        if (gameObject.GetComponent<DragObject>())
        {
            //for unspecified slots set the current object being placed to 
            if (undefinedSlot)
            {
                // Debug.Log("undefined slot");
                slotMatch = gameObject;
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
                // if the slot is defined make it so the object can't be moved again and the slot is invisible
                if (!undefinedSlot)
                {
                    Debug.Log("defined slot running");
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                //make the slot invisible
                GetComponent<Renderer>().forceRenderingOff = true;

            }
        }
    }

    public void CallParentCompletion()
    {
        if (transform.parent.GetComponent<Completion>() != null)
        {
            Completion parent = transform.parent.GetComponent<Completion>();
            Debug.Log("Parent Called");
            if(parent.IsCompleted()){
                Debug.Log("Parent Completed"+ transform.parent.name);
                parent.OnCompletion();
                Debug.Log("ParentCompletion Called");
            }
        }
        else
        {
            Debug.Log("Parent does not have a completion script");
        }
    }

    public bool IsCompleted()
    {
        if(slotMatch==null) {
            Debug.Log("No match");
            return false;
        }
        bool completed = (slotMatch.transform.position == this.transform.position + offset);
        // Debug.Log(transform.name + " " + completed);
        return completed;
    }

    public void OnCompletion(){}
}
