using UnityEngine;
using UnityEngine.SceneManagement;

public class AssemblyGameDriver : MonoBehaviour {

    public ConveyorPickup DragDropSystem;
    public int TotalComponents;
    int FinishedCounter;

    void Start() {
        FinishedCounter = 0;
    }

    public void CompleteObject() {

        DragDropSystem.EvictHeldObject();
        FinishedCounter++;

        if (FinishedCounter == TotalComponents) {
            SceneManager.LoadScene("Hub"); // TODO: Something other than return to hub & fade effects
        }
    }
}
