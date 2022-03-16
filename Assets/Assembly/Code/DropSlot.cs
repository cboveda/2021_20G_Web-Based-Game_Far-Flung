using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, Completion
{

    public Vector3 offset;
    public string slotTypeMatch; // set to premanently lock the object when placed
    public bool undefinedSlot;
    public TextAsset completionTextAsset;

    private GameObject slotObject;

    public bool completed;

    void Start(){
        undefinedSlot = System.String.IsNullOrEmpty(slotTypeMatch)?true: false;
        completed = false;
    }

    void Update(){
        if(slotObject == null) return;
        DragObject dragObject = slotObject.GetComponent<DragObject>();
        // make sure the object can still be moved by the player
        if(dragObject==null || dragObject.isHeld) return;

        //slotObject.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
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
        // Debug.Log("triggered");
        if (gameObject.GetComponent<DragObject>())
        {
            DragObject dragObject = gameObject.GetComponent<DragObject>();
            //for unspecified slots set the current object being placed to 
            if (undefinedSlot)
            {
                // Debug.Log("undefined slot");
                slotTypeMatch = dragObject.itemType;
            }
            if (slotTypeMatch == dragObject.itemType)
            {
                slotObject =  gameObject;
                completed = true;
                Debug.Log("Drop in SLot");
                gameObject.transform.parent = this.transform; 
                gameObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
                if (gameObject.GetComponent<Rigidbody>())
                {
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }

                //try to find the text panel for a popup and check for set texdt if found show the appropriate text
                
                // if the slot is defined make it so the object can't be moved again and the slot is invisible
                if (!undefinedSlot)
                {
                    Debug.Log("defined slot running");
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }

                //make the slot invisible
                GetComponent<Renderer>().forceRenderingOff = true;
                OnCompletion();
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
        return completed;
    }

    public void OnCompletion(){
        CallParentCompletion();
    }
}
