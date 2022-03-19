using UnityEngine;

public class DropSlot : MonoBehaviour {

    public Vector3 offset;
    public string slotTypeMatch; // set to premanently lock the object when placed
    public TextAsset completionTextAsset;

    void Start() {
        
        if ( System.String.IsNullOrEmpty(slotTypeMatch) ) {
            slotTypeMatch = "No Type";
        }
    }

    void OnTriggerEnter(Collider other) {

        ConveyorObject c_o = other.gameObject.GetComponent<ConveyorObject>();

        if ( c_o && c_o.ItemTypeIdentifier == slotTypeMatch ) {

            other.gameObject.transform.parent = this.transform; 
            other.gameObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);

            c_o.PlaceInModel();            
        
            // make the slot invisible
            GetComponent<Renderer>().forceRenderingOff = true;
            transform.parent.gameObject.GetComponent<AssemblyGameDriver>().CompleteObject();
        }
    }
}
