using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float height = 4;
    public float speed = 12f;

    void Update() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = (transform.right * x) + (transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);
    }
}
