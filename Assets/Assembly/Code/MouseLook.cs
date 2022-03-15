using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float lookUpLimit;
    public float lookDownLimit;
    
    float xRotation = 0f;

    public Transform playerBody;
    public GameObject MenuObject;

    // Update is called once per frame
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
        Cursor.visible = true;
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

    // ------------------------- SAMPLE ----------------------------------------

/**
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown() {

        mZCoord = Camera.main.WorldToScreenPoint( gameObject.transform.position ).z;

        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint() {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen

        mousePoint.z = mZCoord;

        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag() {

        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
    */
}