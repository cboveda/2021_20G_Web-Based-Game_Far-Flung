using UnityEngine;
using DialogMaker;
public class DropSlot : MonoBehaviour {

    public Vector3 offset;
    public string slotTypeMatch;
    public DialogGenerator completionDialog;

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

                if ( c_o.PlaceInModel() ) {
    
                    other.gameObject.transform.parent = this.transform; 
                    other.gameObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
    
                    GetComponent<Renderer>().forceRenderingOff = true; // make the slot invisible
                    transform.parent.gameObject.GetComponent<AssemblyGameDriver>().CompleteObject();

                    set = true;

                    if (!completionDialog) return; //skips dialog if null
                    MouseLook.PauseAssembly();
                    completionDialog.BeginPlayingDialog();
                }
            }
        }
    }

    public void TriggerTestHook( Collider c ) {
        this.OnTriggerEnter(c);
    }
}
