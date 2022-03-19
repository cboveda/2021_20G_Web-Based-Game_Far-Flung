using UnityEngine;
using UnityEngine.SceneManagement;

public class AssemblyGameDriver : MonoBehaviour {

    public ConveyorPickup DragDropSystem;
    
    public float rotationSpeed;

    public int TotalComponents;
    int FinishedCounter;

    void Start() {
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
            SceneManager.LoadScene("Hub"); // TODO: Something other than return to hub & fade effects
        }
    }
}