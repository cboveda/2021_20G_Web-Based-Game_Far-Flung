using UnityEngine;

public class ConveyorPickup : MonoBehaviour
{

    public float pickupRange = 10; //changes how far out the player can pickup objects
    public float HoldOffset;
    public float moveForce = 250;

    public float ObjectDistMin = 3;
    public float ObjectDistMax = 20;

    public GameObject heldObj;

    public float SmoothTime = 0.2f;

    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {

            if (heldObj == null) {

                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange)) {

                    PickupObject(hit);
                }

            } else { DropObject(); }

        } else if (heldObj != null) {

            HoldOffset = Mathf.Clamp((HoldOffset + Input.mouseScrollDelta.y), ObjectDistMin, ObjectDistMax);

            Vector3 p1 = heldObj.transform.position;
            Vector3 p2 = transform.position + (transform.forward * HoldOffset);

            heldObj.transform.position = Vector3.SmoothDamp(p1, p2, ref velocity, SmoothTime);
        }
    }

    void PickupObject(RaycastHit hit) {

        GameObject pickObj = hit.transform.gameObject;

        DragObject d_o = pickObj.GetComponent<DragObject>();
        ConveyorObject c_o = pickObj.GetComponent<ConveyorObject>();

        if ( d_o && c_o ) {

            HoldOffset = hit.distance - 1.0f;
            heldObj = pickObj;

            c_o.OnPickUp();
            d_o.isHeld = true;

            //if picking up an object in a slot make sure the slot is rendered again
            if ( d_o.currentSlot ) { 
                d_o.currentSlot.GetComponent<Renderer>().forceRenderingOff = false;
            }
        }
    }

    void DropObject() {

        heldObj.GetComponent<ConveyorObject>().OnDrop();

        DragObject d_o = heldObj.GetComponent<DragObject>();
        d_o.isHeld = false;

        if (d_o.currentSlot) {  //if the object is in a slot, drop the object
            d_o.currentSlot.placeObjectInSlot(heldObj);
        }

        heldObj = null;
    }
}
