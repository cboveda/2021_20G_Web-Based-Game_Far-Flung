using UnityEngine;

public class DropSlot : MonoBehaviour
{

    public Vector3 offset;
    public string slotTypeMatch; // set to premanently lock the object when placed
    public TextAsset completionTextAsset;

    public bool completed;

    void Start() {
        
        if ( System.String.IsNullOrEmpty(slotTypeMatch) ) {
            slotTypeMatch = "No Type";
        }
        
        completed = false;
    }

    void OnTriggerEnter(Collider other) {

        GameObject g_o = other.gameObject;
        ConveyorObject c_o = g_o.GetComponent<ConveyorObject>();

        if ( c_o && c_o.ItemTypeIdentifier == slotTypeMatch ) {

            completed = true;
            Debug.Log("Drop in SLot");
            g_o.transform.parent = this.transform; 
            g_o.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
            
            if (g_o.GetComponent<Rigidbody>()) {
                g_o.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            // TODO: WINDOW POPUP (?)
            
            // Make it so the object can't be moved
            g_o.layer = LayerMask.NameToLayer("Ignore Raycast");
        
            // make the slot invisible
            GetComponent<Renderer>().forceRenderingOff = true;
            CallParentCompletion();
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
        } else {
            Debug.Log("Parent does not have a completion script");
        }
    }
}
