using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public float pickupRange = 5; //changes how far out the player can pickup objects
    public Transform holdParent; //a transform object that effects where the held object is located
    public float moveForce = 250;

    private GameObject heldObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
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
            MoveObject();
        }

    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10; //add air drag to the object

            objRig.transform.parent = holdParent;

            heldObj = pickObj;
        }
    }
    void MoveObject()
    {
        Vector3 moveDirection = (holdParent.position - heldObj.transform.position); //move towards the hold parent

        heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
    }
    void DropObject()
    {
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;
        
        heldObj.transform.parent = null;
        heldObj = null;
    }


}
