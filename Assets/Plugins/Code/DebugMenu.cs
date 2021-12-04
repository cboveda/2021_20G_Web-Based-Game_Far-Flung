using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{

    //public DebugMenu debugMenuScript = null;
    public Canvas debugCanvas;
    public RectTransform menuPosition;
    public GameObject buttonPrefab;
    public GameObject fpsCounterPrefab;
    private GameObject fpsCounter;
    private const string BUTTON_PREFAB_PATH = "Prefabs/DebugButtonPF";
    private const string FPSCOUNTER_PREFAB_PATH = "Prefabs/FPSCounter";
    private float canvasHeight;
    private const float BUTTON_WIDTH = 90.0f;
    private const float BUTTON_HEIGHT = 30.0f;
    private const float BUTTON_WIDTH_OFFSET = (BUTTON_WIDTH / 2 ) + 5.0f;
    private const float BUTTON_HEIGHT_OFFSET = (BUTTON_HEIGHT / 2) + 5.0f;
    private List<GameObject> debugButtons;

    private bool debugMenuOpen;

    private string[] buttonNames =
    {
        "Debug Menu",
        "Hub",
        "Assembly",
        "Flightpath",
        "Scanning",
        "Communications",
        "Lab Analysis",
        "FPS Counter"
    };
    
    
    // Start is called before the first frame update
    void Start()
    {
         
        buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB_PATH);
        fpsCounterPrefab = Resources.Load<GameObject>(FPSCOUNTER_PREFAB_PATH);
        
        debugCanvas = gameObject.AddComponent<Canvas>();
        
        debugCanvas.name = "DebugMenu";
        debugCanvas.sortingOrder = 999;
        debugCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GraphicRaycaster myCaster = gameObject.AddComponent<GraphicRaycaster>();
        myCaster.transform.SetParent(this.transform);
        myCaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

        canvasHeight = debugCanvas.pixelRect.height;
        debugButtons = new List<GameObject>();

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject button = GetNewUIButton(buttonNames[i], i);
            button.transform.SetParent(this.transform);
            debugButtons.Add(button);

            if(button.name != "Debug Menu")
            {
                button.SetActive(false);
            }
        }

        debugMenuOpen = false;

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
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
        //Debug.Log("Creating button.  Adding Listener.");
        //goButton.AddComponent<DebugMenu>();
        
        goButton.GetComponent<Button>().onClick.AddListener(() => { DebugButtonClicked(); });


        return goButton;
    }

    public void DebugButtonClicked()
    {
        Debug.Log("I done got clicked.");
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName + " is what got clicked.");

        if (buttonName.Equals("Debug Menu"))
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
                    if(!button.name.Equals("Debug Menu"))
                    {
                        button.SetActive(false);
                    }
                }
                debugMenuOpen = false;
            }
        }
        else if (buttonName == "Assembly")
        {
            SceneManager.LoadScene("Assembly");
        }
        else if (buttonName == "Flightpath")
        {
            SceneManager.LoadScene("1_FlightpathIntro");
        }
        else if (buttonName == "Communications")
        {
            SceneManager.LoadScene("ComGame");
        }
        else if (buttonName == "Scanning")
        {
            SceneManager.LoadScene("StartScene.Scanning");
        }
        else if (buttonName == "Lab Analysis")
        {
            SceneManager.LoadScene("scene5");
        }
        else if (buttonName == "Hub")
        {
            SceneManager.LoadScene("Hub");
        }
        else if (buttonName == "FPS Counter")
        {
            if (fpsCounter == null)
            {
                fpsCounter = Instantiate<GameObject>(fpsCounterPrefab);
                fpsCounter.transform.parent = this.transform;
                fpsCounter.SetActive(true);
            } 
            else {
                fpsCounter.SetActive(!fpsCounter.activeSelf);
            }
        }
    }
}
