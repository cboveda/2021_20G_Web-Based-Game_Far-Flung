using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float lookUpLimit;
    public float lookDownLimit;
    
    float xRotation = 0f;

    public Transform playerBody;
    public GameObject MenuObject; 

    void Start() {

        ResumeAssembly();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                PauseAssembly();
            
            } else {
            
                ResumeAssembly();
            }
        }
        if (Cursor.lockState == CursorLockMode.Locked) {
            LookUpdate();
        }
    }

    public void PauseAssembly() {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuObject.SetActive(true);
    }

    public void ResumeAssembly() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuObject.SetActive(false);
    }

    void LookUpdate() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookUpLimit, lookDownLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}