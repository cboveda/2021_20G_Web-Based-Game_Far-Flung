using UnityEngine;
using UnityEngine.SceneManagement;
using DialogMaker;

public class AssemblyGameDriver : MonoBehaviour {

    public ConveyorPickup DragDropSystem;
    
    public float RotationSpeed;

    public int TotalComponents;

    [HideInInspector]
    public int FinishedCounter;

    public DialogGenerator introDiag;
    public DialogGenerator outroDiag;

    void Start() {
        
        // MouseLook.PauseAssembly();
        MouseLook.ResumeAssembly();
        // introDiag.BeginPlayingDialog();
    
        FinishedCounter = 0;
    }

    void Update() {

        transform.Rotate(new Vector3(0, 1, 0) * (RotationSpeed * Time.deltaTime), Space.World);
        transform.Rotate(new Vector3(1, 0, 0) * (RotationSpeed * Time.deltaTime), Space.Self);
    }
    
    public void CompleteObject() {

        DragDropSystem.EvictHeldObject();
        FinishedCounter++;

        if (FinishedCounter == TotalComponents) {
            MouseLook.PauseAssembly();
            outroDiag.BeginPlayingDialog();
        }
    }

    public void ReturnToHub() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Hub");
    }
}