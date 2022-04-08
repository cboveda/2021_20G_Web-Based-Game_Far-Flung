using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.3f;
    float height = 4f;

    float zMin = -9;
    float zMax = 86;
    float xMin = -73;
    float xMax = 4;

    void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if ( x != 0f || z != 0f ) { // performance

            Vector3 move = transform.localPosition + (transform.forward * z) + (transform.right * x);

            move.y = height;
            move.x = Mathf.Clamp(move.x, xMin, xMax);
            move.z = Mathf.Clamp(move.z, zMin, zMax);

            transform.localPosition = Vector3.Lerp(transform.localPosition, move, speed);
        }
    }
}
