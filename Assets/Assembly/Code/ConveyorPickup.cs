using UnityEngine;

public class ConveyorPickup : MonoBehaviour {

    public float pickupRange = 10; //changes how far out the player can pickup objects
    public float HoldOffset;
    public float moveForce = 250;

    public float ObjectDistMin = 3;
    public float ObjectDistMax = 20;

    public GameObject heldObj;

    public float SmoothTime = 0.2f;

    Vector3 velocity = Vector3.zero;

    void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {

            if (heldObj == null) {

                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange)) {

                    PickupObject(hit);
                }

            } else { DropObject(); }

        } else if ( heldObj != null ) {

            HoldOffset = Mathf.Clamp((HoldOffset + Input.mouseScrollDelta.y), ObjectDistMin, ObjectDistMax);

            Vector3 p1 = heldObj.transform.position;
            Vector3 p2 = transform.position + (transform.forward * HoldOffset);

            heldObj.transform.position = Vector3.SmoothDamp(p1, p2, ref velocity, SmoothTime);
        }
    }

    void PickupObject(RaycastHit hit) {

        GameObject pickObj = hit.transform.gameObject;
        ConveyorObject c_o = pickObj.GetComponent<ConveyorObject>();

        if ( c_o ) {

            HoldOffset = hit.distance - 1.0f;
            heldObj = pickObj;
            c_o.OnPickUp();
        }
    }

    void DropObject() {

        ConveyorObject c_o = heldObj.GetComponent<ConveyorObject>();

        if ( c_o ) {
            
            c_o.OnDrop();
            heldObj = null;
        }
    }

    public void EvictHeldObject() {
        heldObj = null;
    }
}
