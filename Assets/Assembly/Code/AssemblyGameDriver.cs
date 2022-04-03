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

    // Start is called before the first frame update
    void Start()
    {
        if (introDiag == null) {
            try{
                introDiag = GameObject.Find("AssemblyIntro").GetComponent<DialogGenerator>();
            } catch {
                Debug.Log("Failed to find Assembly Intro Dialog Generator");
            }
        }
        
        MouseLook.PauseAssembly();
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
            MouseLook.PauseAssembly();
            outroDiag.BeginPlayingDialog();
        }
    }

    public void ReturnToHub(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Hub");
    }
}