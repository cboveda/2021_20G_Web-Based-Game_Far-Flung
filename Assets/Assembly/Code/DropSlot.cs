using UnityEngine;

public class DropSlot : MonoBehaviour {

    public Vector3 offset;
    public string slotTypeMatch;
    public TextAsset completionTextAsset;

    bool set;

    void Start() {

        set = false;
        
        if ( System.String.IsNullOrEmpty(slotTypeMatch) ) {
            slotTypeMatch = "No Type";
        }
    }

    void OnTriggerEnter(Collider other) {

        if (!set) {

            ConveyorObject c_o = other.gameObject.GetComponent<ConveyorObject>();

            if ( c_o && c_o.ItemTypeIdentifier == slotTypeMatch ) {

                other.gameObject.transform.parent = this.transform; 
                other.gameObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);

                c_o.PlaceInModel();            
            
                // make the slot invisible
                GetComponent<Renderer>().forceRenderingOff = true;
                transform.parent.gameObject.GetComponent<AssemblyGameDriver>().CompleteObject();

                set = true;
            }
        }
    }
}
