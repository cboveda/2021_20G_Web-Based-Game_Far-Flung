using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float lookUpLimit;
    public float lookDownLimit;

    public GameObject MenuObject;
    
    float xRotation = 0f;

    public Transform playerBody;
    // public GameObject MenuObject; 

    void Start() {

    }

    void Update() {
        
        if ( Input.GetButtonDown("Cancel") ) {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                ShowMenu();
            
            } else {
            
                HideMenu();
            }
        }
        if (Cursor.lockState == CursorLockMode.Locked) {
            LookUpdate();
        }
    }

    public void PauseAssembly() {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void ResumeAssembly() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale =1;
    }

    public void ShowMenu(){
        PauseAssembly();
        MenuObject.SetActive(true);
    }

    public void HideMenu(){
        MenuObject.SetActive(false);
        ResumeAssembly();
    }

    void LookUpdate() {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookUpLimit, lookDownLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void OnDestroy() {
        Cursor.lockState = CursorLockMode.None;
    }
}