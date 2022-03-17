using UnityEngine;

public class ParentGhost : MonoBehaviour {
    
    public float rotationSpeed;

    void Update() {

        transform.Rotate(new Vector3(0, 1, 0) * (rotationSpeed * Time.deltaTime), Space.World);
        transform.Rotate(new Vector3(1, 0, 0) * (rotationSpeed * Time.deltaTime), Space.Self);
    }
}
