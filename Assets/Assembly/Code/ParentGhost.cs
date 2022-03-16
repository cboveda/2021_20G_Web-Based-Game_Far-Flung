using UnityEngine;

public class ParentGhost : MonoBehaviour {
    
    public float rotationSpeed;

    void Update() {

        this.transform.Rotate(new Vector3(1, 1, 0) * (rotationSpeed * Time.deltaTime));
    }
}
