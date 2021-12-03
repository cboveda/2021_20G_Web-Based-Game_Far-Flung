using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public float pickupRange = 10; //changes how far out the player can pickup objects
    public Transform holdParent; //a transform object that effects where the held object is located
    public float moveForce = 250;

    private GameObject heldObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log("E pressed");
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    // Debug.Log("Call Pickup Object");
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            // Debug.Log("Move call");
            MoveObject();
        }

    }

    void PickupObject(GameObject pickObj)
    {
        Debug.Log("Pickup " + pickObj.name);
        if (pickObj.GetComponent<DragObject>() && pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.constraints = RigidbodyConstraints.None;
            objRig.useGravity = false;
            objRig.drag = 10; //add air drag to the object

            objRig.freezeRotation = true;
            objRig.transform.parent = holdParent;

            heldObj = pickObj;
            heldObj.GetComponent<DragObject>().isHeld = true;
        }

        //if picking up an object in a slot make sure the slot is rendered again
        if(heldObj.GetComponent<DragObject>().currentSlot){
            heldObj.GetComponent<DragObject>().currentSlot.GetComponent<Renderer>().forceRenderingOff = false;
        }
    }
    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position); //move towards the hold parent
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);

        }
    }
    void DropObject()
    {
        Debug.Log("Drop Cube");
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldRig.freezeRotation = false;
        heldObj.GetComponent<DragObject>().isHeld = false;

        //if the object is in a slot, drop the object 
        if(heldObj.GetComponent<DragObject>().currentSlot){
            heldObj.GetComponent<DragObject>().currentSlot.placeObjectInSlot(heldObj);
        }

        heldObj.transform.parent = null;
        heldObj = null;
        
    }


}
