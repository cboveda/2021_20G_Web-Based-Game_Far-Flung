using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DebugCamera : MonoBehaviour
{

    //public DebugMenu debugMenuScript = null;
    public Canvas debugCanvas;
    public RectTransform menuPosition;
    public GameObject buttonPrefab;
    private const string BUTTON_PREFAB_PATH = "Prefabs/DebugButtonPF";
    private float canvasHeight;
    private const float BUTTON_WIDTH = 150.0f;
    private const float BUTTON_HEIGHT = 30.0f;
    private const float BUTTON_WIDTH_OFFSET = (BUTTON_WIDTH / 2) + BUTTON_WIDTH;
    private const float BUTTON_HEIGHT_OFFSET = (BUTTON_HEIGHT / 2) + 5.0f;
    private const int CAMERA_PRIORITY_HIGH = 1000;
    private const int CAMERA_PRIORITY_LOW = 10;
    private bool firstClickHappened = false;
    private CinemachineVirtualCamera assemblyCamera;
    private CinemachineVirtualCamera labCamera;
    private CinemachineVirtualCamera flightPlanCamera;
    private CinemachineVirtualCamera missionControlCamera;
    private CinemachineVirtualCamera commsCamera;
    private List<GameObject> debugButtons;
    private List<CinemachineVirtualCamera> virtualCameras;
    private CameraPositionController cameraController;

    private bool debugMenuOpen;

    private string[] buttonNames =
    {
        "Camera Menu",
        "Assembly",
        "Flight Planning",
        "Mission Control",
        "Communications",
        "Lab Analysis"
    };


    // Start is called before the first frame update
    void Start()
    {

        buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB_PATH);

        debugCanvas = gameObject.AddComponent<Canvas>();


        debugCanvas.name = "DebugCams";
        debugCanvas.sortingOrder = 999;
        debugCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GraphicRaycaster myCaster = gameObject.AddComponent<GraphicRaycaster>();
        myCaster.transform.parent = this.transform;
        myCaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

        canvasHeight = debugCanvas.pixelRect.height;
        debugButtons = new List<GameObject>();
        virtualCameras = new List<CinemachineVirtualCamera>();

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject button = GetNewUIButton(buttonNames[i], i);
            button.transform.parent = this.transform;
            debugButtons.Add(button);

            if (button.name != "Camera Menu")
            {
                button.SetActive(false);
            }
        }

        debugMenuOpen = false;

        //commsCamera = GameObject.Find("vcamComms").GetComponent<CinemachineVirtualCamera>();





    }

    private GameObject GetNewUIButton(string displayName, int buttonNumber)
    {
        GameObject goButton = Instantiate<GameObject>(buttonPrefab);
        goButton.name = displayName;

        goButton.GetComponentInChildren<Text>().text = displayName;
        Vector3 buttonLocation = new Vector3(BUTTON_WIDTH_OFFSET, canvasHeight - BUTTON_HEIGHT * (buttonNumber + 1) + BUTTON_HEIGHT_OFFSET, 0);
        goButton.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BUTTON_WIDTH);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BUTTON_HEIGHT);
        goButton.GetComponent<RectTransform>().SetPositionAndRotation(buttonLocation, Quaternion.identity);

        goButton.GetComponent<Button>().onClick.AddListener(() => { DebugButtonClicked(); });


        return goButton;
    }

    public void DebugButtonClicked()
    {
        Debug.Log("I done got clicked.");
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName + " is what got clicked omg finally.");

        if (!firstClickHappened)
        {
            FirstMenuClick();
            firstClickHappened = true;
        }

        if (buttonName.Equals("Camera Menu"))
        {
            if (!debugMenuOpen)
            {
                foreach (GameObject button in debugButtons)
                {
                    button.SetActive(true);
                }
                debugMenuOpen = true;
            }
            else
            {
                foreach (GameObject button in debugButtons)
                {
                    if (!button.name.Equals("Camera Menu"))
                    {
                        button.SetActive(false);
                    }
                }
                debugMenuOpen = false;
            }
        }
        else if (buttonName == "Assembly")
        {
            SetPriorityCamera(assemblyCamera);
            //cameraController.AdjustCurrentCamera(assemblyCamera);
        }
        else if (buttonName == "Flight Planning")
        {
            SetPriorityCamera(flightPlanCamera);
            //cameraController.AdjustCurrentCamera(flightPlanCamera);
        }
        else if (buttonName == "Communications")
        {
            SetPriorityCamera(commsCamera);
            //cameraController.AdjustCurrentCamera(labCamera);
        }
        else if (buttonName == "Mission Control")
        {
            SetPriorityCamera(missionControlCamera);
            //cameraController.AdjustCurrentCamera(missionControlCamera);
        }
        else if (buttonName == "Lab Analysis")
        {
            SetPriorityCamera(labCamera);
            //cameraController.AdjustCurrentCamera(labCamera);
        }
    }

    private void SetPriorityCamera(CinemachineVirtualCamera camera)
    {
        foreach (CinemachineVirtualCamera vCamera in virtualCameras)
        {
            vCamera.Priority = CAMERA_PRIORITY_LOW;
        }
        camera.Priority = CAMERA_PRIORITY_HIGH;
        cameraController.AdjustCurrentCamera(camera);
    }

    private void FirstMenuClick()
    {
        virtualCameras.Add(assemblyCamera = GameObject.Find("vcamAssembly").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(labCamera = GameObject.Find("vcamLab").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(flightPlanCamera = GameObject.Find("vcamFlightPlanning").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(missionControlCamera = GameObject.Find("vcamMissionControl").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(commsCamera = GameObject.Find("vcamComms").GetComponent<CinemachineVirtualCamera>());

        cameraController = GameObject.Find("CameraControlScript").GetComponent<CameraPositionController>();
    }
}
