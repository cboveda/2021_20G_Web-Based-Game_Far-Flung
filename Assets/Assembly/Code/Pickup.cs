using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Camera cam;
    // public float pickupRange = 10; //changes how far out the player can pickup objects
    // public Transform holdParent; //a transform object that effects where the held object is located
    public float moveForce = 250;
    public GameObject target;

    private GameObject heldObj;

    private Vector3 mousePoint;
    private Vector3 pickupOffset;
    private float distance;

    private Plane plane;

    void Start()
    {
        cam = Camera.main;
        distance = Vector3.Distance(cam.transform.position, target.transform.position);
        plane = new Plane(Vector3.forward , target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (heldObj == null)
                {
                    ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Debug.Log("Call Pickup Object");
                        PickupObject(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            DropObject();
        }


        if (heldObj != null)
        {
            // Debug.Log("Move call");
            MoveObject();
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (heldObj == null)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                Debug.DrawLine(ray.origin, hit.point);
                // Debug.Log("Call Pickup Object");
                PickupObject(hit.transform.gameObject);
            }
        }
    }

    void OnMouseUp()
    {
        if (heldObj != null)
        {
            DropObject();
        }
    }

    void PickupObject(GameObject pickObj)
    {  
        Debug.Log("Pickup " + pickObj.name);
        if (pickObj.GetComponent<DragObject>() && pickObj.GetComponent<Rigidbody>())
        {
            pickupOffset = pickObj.transform.position - mousePoint;

            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.constraints = RigidbodyConstraints.None;
            objRig.useGravity = false;
            objRig.drag = 10; //add air drag to the object

            objRig.freezeRotation = true;
            // // objRig.transform.parent = holdParent;

            heldObj = pickObj;
            heldObj.GetComponent<DragObject>().isHeld = true;
        }

        //if picking up an object in a slot make sure the slot is rendered again
        if (heldObj.GetComponent<DragObject>().currentSlot)
        {
            heldObj.GetComponent<DragObject>().currentSlot.GetComponent<Renderer>().forceRenderingOff = false;
        }
    }
    void MoveObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray,out distance);
        Vector2 mousePos = new Vector2();
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        mousePoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
        if (Vector3.Distance(heldObj.transform.position, mousePoint) > 0.1f)
        {
            Vector3 moveDirection = (mousePoint - heldObj.transform.position); //move towards the mouse
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);

        }
    }
    void DropObject()
    {
        if (heldObj == null) return;
        Debug.Log("Drop Cube");
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();

        heldRig.useGravity = true;
        heldRig.drag = 1;
        heldRig.velocity = Vector3.zero;

        heldRig.freezeRotation = false;

        heldObj.GetComponent<DragObject>().isHeld = false;

        //if the object is in a slot, drop the object 
        if (heldObj.GetComponent<DragObject>().currentSlot)
        {
            // heldObj.GetComponent<DragObject>().currentSlot.placeObjectInSlot(heldObj);
        }

        heldObj = null;
    }
}
