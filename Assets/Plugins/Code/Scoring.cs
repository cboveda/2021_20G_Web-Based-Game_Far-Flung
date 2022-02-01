using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{

    public Canvas scoringCanvas;
    public RectTransform menuPosition;
    public GameObject buttonPrefab;
    public GameObject fpsCounterPrefab;
    public GameObject scoringPrefab;
    private GameObject fpsCounter;
    private const string BUTTON_PREFAB_PATH = "Prefabs/DebugButtonPF";
    private const string FPSCOUNTER_PREFAB_PATH = "Prefabs/FPSCounter";
    private const string SCORING_PREFAB_PATH = "Scoring";
    private float canvasHeight;
    private float canvasWidth;
    private const float BUTTON_WIDTH = 90.0f;
    private const float BUTTON_HEIGHT = 30.0f;
    private const float BUTTON_WIDTH_OFFSET = 100.0f;
    private const float BUTTON_HEIGHT_OFFSET = 0.0f;
    private List<GameObject> debugButtons;

    private bool debugMenuOpen;


    private Dictionary<string, string> scoringBox = new Dictionary<string, string>()
    {
        {"ScoringButton", "Score"},
        {"ScoreBox", "0"}
    };

    // Start is called before the first frame update
    void Start()
    {

        buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB_PATH);
        //fpsCounterPrefab = Resources.Load<GameObject>(FPSCOUNTER_PREFAB_PATH);
        scoringPrefab = Resources.Load<GameObject>(SCORING_PREFAB_PATH);

        scoringCanvas = gameObject.AddComponent<Canvas>();

        scoringCanvas.name = "ScoringCanvas";
        scoringCanvas.sortingOrder = 999;
        scoringCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GraphicRaycaster myCaster = gameObject.AddComponent<GraphicRaycaster>();
        myCaster.transform.SetParent(this.transform);
        myCaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

        canvasHeight = scoringCanvas.pixelRect.height;
        canvasWidth = scoringCanvas.pixelRect.width;
        
        debugButtons = new List<GameObject>();

        for (int i = 0; i < scoringBox.Count; i++)
        {
            GameObject button = GetNewUIButton(scoringBox.Key, i, scoringBox.Value);
            button.transform.SetParent(this.transform);
            debugButtons.Add(button);

        }

        debugMenuOpen = false;


    }

    private GameObject GetNewUIButton(string displayName, int buttonNumber, string text)
    {

        GameObject goButton = Instantiate<GameObject>(buttonPrefab);
        goButton.name = displayName;

        goButton.GetComponentInChildren<Text>().text = text;
        Debug.Log(canvasWidth / 2 + (BUTTON_WIDTH * buttonNumber));
        Debug.Log(canvasHeight - BUTTON_HEIGHT / 2);


        Vector3 buttonLocation = new Vector3(canvasWidth / 2 + (BUTTON_WIDTH * buttonNumber) + BUTTON_WIDTH_OFFSET, canvasHeight - BUTTON_HEIGHT / 2, 0);
        goButton.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BUTTON_WIDTH);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BUTTON_HEIGHT);
        //goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BUTTON_WIDTH);
        //goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BUTTON_HEIGHT);
        goButton.GetComponent<RectTransform>().SetPositionAndRotation(buttonLocation, Quaternion.identity);
        //Debug.Log("Creating button.  Adding Listener.");
        //goButton.AddComponent<DebugMenu>();

        if (displayName == "Scoring")
        {
            goButton.GetComponent<Button>().onClick.AddListener(() => { DebugButtonClicked(); });
        }      

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
                    if (!button.name.Equals("Debug Menu"))
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
            else
            {
                fpsCounter.SetActive(!fpsCounter.activeSelf);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
