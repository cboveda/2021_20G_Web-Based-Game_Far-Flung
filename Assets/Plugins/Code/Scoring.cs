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
    public GameObject buttonPrefab;
    public GameObject scoringPrefab;
    private const string BUTTON_PREFAB_PATH = "Prefabs/DebugButtonPF";
    private const string SCORING_PREFAB_PATH = "Scoring";
    private float canvasHeight;
    private float canvasWidth;
    private const float BUTTON_WIDTH = 90.0f;
    private const float BUTTON_HEIGHT = 30.0f;
    private const float BUTTON_WIDTH_OFFSET = 100.0f;
    private List<GameObject> scoringButtons;
    CanvasGroup scoringCanvasGroup;

    GameObject scoreDetails;
    int totalScore = 0;
    int comPuzzleScore = 0;
    int comUnscrambleScore = 0;
    int assemblyScore = 0;
    int flightPathScore = 0;
    int scanningScore = 0;
    int labScore = 0;
    public bool showingScore = false;


    public bool getShowingScore
    {
        get { return showingScore; }
        set { showingScore = value; }
    }

    public int getTotalScore
    {
        get { return totalScore; }
        set { totalScore = value; }
    }

    public int getComPuzzleScore
    {
        get { return comPuzzleScore; }
        set { comPuzzleScore = value; }
    }

    public int getComUnscrambleScore
    {
        get { return comUnscrambleScore; }
        set { comUnscrambleScore = value; }
    }

    public int getAssemblyScore
    {
        get { return assemblyScore; }
        set { assemblyScore = value; }
    }

    public int getFlightPathScore
    {
        get { return flightPathScore; }
        set { flightPathScore = value; }
    }

    public int getScanningScore
    {
        get { return scanningScore; }
        set { scanningScore = value; }
    }

        public int getLabScore
    {
        get { return labScore; }
        set { labScore = value; }
    }

    private Dictionary<string, string> scoringBox = new Dictionary<string, string>()
    {
        {"MainScoring", "Total"},
        {"MainScoreBox", "0"},
        {"ScoringButton", "Game"},
        {"ScoreBox", "0"}
    };


    public static Scoring Instance;
    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
     
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

        buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB_PATH);
        scoringPrefab = Resources.Load<GameObject>(SCORING_PREFAB_PATH);
        scoringCanvas = gameObject.AddComponent<Canvas>();
        scoringCanvas.name = "ScoringCanvas";
        scoringCanvas.sortingOrder = 999;
        scoringCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        scoreDetails = GameObject.Find("ScoreDetails");        
        hideScoreDetailsDisplay();

        CanvasScaler scoreCanvasScaler = gameObject.AddComponent<CanvasScaler>();
        scoreCanvasScaler.transform.SetParent(this.transform);
        scoreCanvasScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scoreCanvasScaler.referenceResolution = new Vector2(1440, 900);

        GraphicRaycaster myCaster = gameObject.AddComponent<GraphicRaycaster>();
        myCaster.transform.SetParent(this.transform);
        myCaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;

        canvasHeight = scoringCanvas.pixelRect.height;
        canvasWidth = scoringCanvas.pixelRect.width;
        
        scoringButtons = new List<GameObject>();

        int itemNum = 0;
        Color textColor = new Color32(255, 150, 0, 255);
        foreach (KeyValuePair<string, string> item in scoringBox)
        {
            if ( itemNum > 1 )
            {
                textColor = Color.red;
            }
            GameObject button = GetNewUIButton(item.Key, itemNum, item.Value, textColor);
            button.transform.SetParent(this.transform);
            scoringButtons.Add(button);
            itemNum++;
        }
    }

    private GameObject GetNewUIButton(string displayName, int buttonNumber, string text, Color textColor)
    {
        Color blackColor = new Color32(0, 0, 0, 255);
        GameObject goButton = Instantiate<GameObject>(buttonPrefab);
        goButton.name = displayName;

        goButton.GetComponentInChildren<Text>().text = text;
        goButton.GetComponentInChildren<Text>().color = textColor;
        goButton.GetComponent<Image>().color = Color.white;

        ColorBlock colors = goButton.GetComponent<UnityEngine.UI.Button>().colors;
        colors.normalColor = Color.black;
        colors.highlightedColor = Color.gray;
        colors.pressedColor = Color.black;
        colors.selectedColor = Color.black;
        colors.disabledColor = Color.black;        
        goButton.GetComponent<UnityEngine.UI.Button>().colors = colors;

        //Debug.Log(canvasWidth / 2 + (BUTTON_WIDTH * buttonNumber));
        //Debug.Log(canvasHeight - BUTTON_HEIGHT / 2);

        Vector3 buttonLocation = new Vector3(canvasWidth / 2 + (BUTTON_WIDTH * buttonNumber) + BUTTON_WIDTH_OFFSET, canvasHeight - BUTTON_HEIGHT / 2, 0);
        goButton.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BUTTON_WIDTH);
        goButton.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BUTTON_HEIGHT);
        goButton.GetComponent<RectTransform>().SetPositionAndRotation(buttonLocation, Quaternion.identity);
        
        goButton.GetComponent<Button>().onClick.AddListener(() => { ScoreButtonClicked(); });
   
        return goButton;
    }
    

    public void hideScoreDetailsDisplay()
    {
        scoreDetails = GameObject.Find("ScoreDetails");
        scoringCanvasGroup = scoreDetails.GetComponent<CanvasGroup>();
        scoringCanvasGroup.alpha = 0f;
        scoringCanvasGroup.blocksRaycasts = false;
        FindObjectOfType<Scoring>().getShowingScore = false;        
    }

    public void ScoreButtonClicked()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        scoringCanvasGroup = scoreDetails.GetComponent<CanvasGroup>();


        if (buttonName.Equals("ScoringButton"))
        {
            //Debug.Log("Scoring Info");
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (buttonName == "MainScoreBox")
        {
            //Debug.Log("Score Details");
            EventSystem.current.SetSelectedGameObject(null);
            scoringCanvasGroup.alpha = 1f;
            scoringCanvasGroup.blocksRaycasts = true;
            FindObjectOfType<Scoring>().getShowingScore = true;
            updateScore(0);                       
        }
    }

    public void initialize(int score)
    {
        StartCoroutine(InitializeScore(score));
    }

    IEnumerator InitializeScore(int score)
    {
        yield return new WaitForSeconds(0.1f);        
        updateScore(score);
    }


    public void updateScore(int score)
    {
        //Debug.Log("updated score");
        GameObject scoringObj = GameObject.Find("ScoreBox");        
        GameObject gameScore;        
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        switch (sceneName)
        {          
            
            case "comGame":                                 
                comPuzzleScore = FindObjectOfType<Scoring>().getComPuzzleScore + score;
                FindObjectOfType<Scoring>().getComPuzzleScore = comPuzzleScore;
                scoringObj.GetComponentInChildren<Text>().text = comPuzzleScore.ToString();
                gameScore = GameObject.Find("ComPuzzleScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = comPuzzleScore.ToString();
                break;
            case "comUnscramble":
                comUnscrambleScore = FindObjectOfType<Scoring>().getComUnscrambleScore + score;
                FindObjectOfType<Scoring>().getComUnscrambleScore = comUnscrambleScore;
                scoringObj.GetComponentInChildren<Text>().text = comUnscrambleScore.ToString(); 
                gameScore = GameObject.Find("ComUnscrambleScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = comUnscrambleScore.ToString();      
                break;
            case "Assembly 3d":
                assemblyScore = FindObjectOfType<Scoring>().getAssemblyScore + score;
                FindObjectOfType<Scoring>().getAssemblyScore = assemblyScore;
                scoringObj.GetComponentInChildren<Text>().text = assemblyScore.ToString();
                gameScore = GameObject.Find("AssemblyScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = assemblyScore.ToString();
                break;
            case "2_Flightpath":
                flightPathScore = FindObjectOfType<Scoring>().getFlightPathScore + score;
                FindObjectOfType<Scoring>().getFlightPathScore = flightPathScore;
                scoringObj.GetComponentInChildren<Text>().text = flightPathScore.ToString();
                gameScore = GameObject.Find("FlightPathScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = flightPathScore.ToString();
                break;
            case "scene5":
                labScore = FindObjectOfType<Scoring>().getLabScore + score;
                FindObjectOfType<Scoring>().getLabScore = labScore;
                scoringObj.GetComponentInChildren<Text>().text = labScore.ToString();
                gameScore = GameObject.Find("LabScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = labScore.ToString();
                break;
            case "Scanning":
                scanningScore = FindObjectOfType<Scoring>().getScanningScore + score;
                FindObjectOfType<Scoring>().getScanningScore = scanningScore;
                scoringObj.GetComponentInChildren<Text>().text = scanningScore.ToString();
                gameScore = GameObject.Find("ScanningScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = scanningScore.ToString();
                break;
        }

        setCurrentScore();

    }

    public void setCurrentScore()
    {
        GameObject totalGameScore = GameObject.Find("TotalScore");
        GameObject scoringMainObj = GameObject.Find("MainScoreBox");
        int total = 0;
        int score = 0;
        string[] allScenes = { "comGame", "comUnscramble", "Assembly 3d", "2_Flightpath", "scene5", "Scanning" };       
        foreach (string scene in allScenes)        
        {
            score = getGameScore(scene);
            Debug.Log(score);
            total += score;            
        }
        FindObjectOfType<Scoring>().getTotalScore = total;
        totalScore = FindObjectOfType<Scoring>().getTotalScore;        
        totalGameScore.transform.GetChild(0).GetComponent<Text>().text = totalScore.ToString();
        scoringMainObj.GetComponentInChildren<Text>().text = totalScore.ToString();
    }

    public int getGameScore(string sceneName)
    {
        int score = 0;
        switch (sceneName)
        {
            case "comGame":
                score = FindObjectOfType<Scoring>().getComPuzzleScore;
                break;
            case "comUnscramble":
                score = FindObjectOfType<Scoring>().getComUnscrambleScore;
                break;
            case "Assembly 3d":
                score = FindObjectOfType<Scoring>().getAssemblyScore;
                break;
            case "2_Flightpath":
                score = FindObjectOfType<Scoring>().getFlightPathScore;
                break;
            case "scene5":
                score = FindObjectOfType<Scoring>().getLabScore;
                break;
            case "Scanning":
                score = FindObjectOfType<Scoring>().getScanningScore;
                break;
        }
        return score;

    }

}
