using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSpeed = 5f;
    public float lookUpLimit;
    public float lookDownLimit;

    static GameObject menu;

    public static GameObject MenuObject 
    {get {return menu? menu: (menu = GameObject.Find("Menus").transform.Find("PauseMenu").gameObject);}}

    void Update() {
        
        if ( Input.GetButtonDown("Cancel") ) {
            if (Cursor.lockState == CursorLockMode.Locked )
            {
                ShowMenu();
            
            } else if(MenuObject.activeSelf){
            
                HideMenu();
            }
        }
        if (Cursor.lockState == CursorLockMode.Locked) {
            LookUpdate();
        }
    }

    public static void PauseAssembly() {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void ResumeAssembly() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public static void ShowMenu() {
        PauseAssembly();
        MenuObject.SetActive(true);
    }

    public static void HideMenu() {
        MenuObject.SetActive(false);
        ResumeAssembly();
    }

    void LookUpdate() {
        float h = MouseSpeed * Input.GetAxis("Mouse X");
        float v = -MouseSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(v, 0, 0), Space.Self);
        transform.Rotate(new Vector3(0, h, 0), Space.World);
    }

    void OnDestroy() {
        Cursor.lockState = CursorLockMode.None;
    }
}