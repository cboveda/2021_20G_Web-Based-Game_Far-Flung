using UnityEngine;
using UnityEngine.SceneManagement;
using DialogMaker;

public class AssemblyGameDriver : MonoBehaviour {

    public ConveyorPickup DragDropSystem;
    
    public float rotationSpeed;

    public int TotalComponents;
    int FinishedCounter;

     public DialogGenerator introDiag;
    public DialogGenerator outroDiag;

    public MouseLook mouseLook;
    // Start is called before the first frame update
    void Start()
    {
        if(introDiag == null){
            try{
                introDiag = GameObject.Find("AssemblyIntro").GetComponent<DialogGenerator>();
            } catch {
                Debug.Log("Failed to find Assembly Intro Dialog Generator");
            }
        }

        if( mouseLook == null){
            try{
                mouseLook = GameObject.Find("Assembly Camera").GetComponent<MouseLook>();
            } catch {
                Debug.Log("Failed to find Assembly Camrera mouse look");
            }
        }
        
        mouseLook.PauseAssembly();
        introDiag.BeginPlayingDialog();
    
        FinishedCounter = 0;
    }

    void Update() {

        transform.Rotate(new Vector3(0, 1, 0) * (rotationSpeed * Time.deltaTime), Space.World);
        transform.Rotate(new Vector3(1, 0, 0) * (rotationSpeed * Time.deltaTime), Space.Self);
    }
    
    public void CompleteObject() {

        DragDropSystem.EvictHeldObject();
        FinishedCounter++;

        if (FinishedCounter == TotalComponents) {
            mouseLook.PauseAssembly();
            outroDiag.BeginPlayingDialog();
        }
    }

    public void ReturnToHub(){
        SceneManager.LoadScene("Hub");
    }
}
