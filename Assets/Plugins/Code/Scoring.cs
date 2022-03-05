using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;


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
    private const float BUTTON_WIDTH_OFFSET = 0.0f;
    private List<GameObject> scoringButtons;
    CanvasGroup scoringCanvasGroup;
    CanvasGroup gameCanvasGroup;

    GameObject scoreDetails;
    GameObject gameDetails;
    GameObject scoreBox;
    GameObject mainScoreBox;
    int totalScore = 0;
    int comPuzzleScore = 0;
    int comUnscrambleScore = 0;
    int assemblyScore = 0;
    int flightPathScore = 0;
    int scanningScore = 0;
    int labScore = 0;
    int blankScore = 0;
    public bool showingScore = false;
    public bool showingGameScore = false;
    public bool initialized = false;

    int comObjective1 = 0;
    int comObjective2 = 0;
    int comObjective3 = 0;
    int comObjective4 = 0;
    int comObjective5 = 0;
    int comObjective6 = 0;
    int comObjective7 = 0;
    int comObjective8 = 0;
    int comObjective9 = 0;
    int comObjective10 = 0;
    int comObjective11 = 0;
    int comObjective12 = 0;
    int comObjective13 = 0;

    public int getComObjective1
    {
        get { return comObjective1; }
        set { comObjective1 = value; }
    }

    public int getComObjective2
    {
        get { return comObjective2; }
        set { comObjective2 = value; }
    }

    public int getComObjective3
    {
        get { return comObjective3; }
        set { comObjective3 = value; }
    }

    public int getComObjective4
    {
        get { return comObjective4; }
        set { comObjective4 = value; }
    }

    public int getComObjective5
    {
        get { return comObjective5; }
        set { comObjective5 = value; }
    }

    public int getComObjective6
    {
        get { return comObjective6; }
        set { comObjective6 = value; }
    }

    public int getComObjective7
    {
        get { return comObjective7; }
        set { comObjective7 = value; }
    }

    public int getComObjective8
    {
        get { return comObjective8; }
        set { comObjective8 = value; }
    }

    public int getComObjective9
    {
        get { return comObjective9; }
        set { comObjective9 = value; }
    }

    public int getComObjective10
    {
        get { return comObjective10; }
        set { comObjective10 = value; }
    }

    public int getComObjective11
    {
        get { return comObjective11; }
        set { comObjective11 = value; }
    }

    public int getComObjective12
    {
        get { return comObjective12; }
        set { comObjective12 = value; }
    }

    public bool getInitialized
    {
        get { return initialized; }
        set { initialized = value; }
    }

    public bool getShowingGameScore
    {
        get { return showingGameScore; }
        set { showingGameScore = value; }
    }

    public bool getShowingScore
    {
        get { return showingScore; }
        set { showingScore = value; }
    }

    public int getBlankScore
    {
        get { return blankScore; }
        set { blankScore = value; }
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

        hideScoreDetailsDisplay("ScoreDetails");
        hideScoreDetailsDisplay("GameDetails");

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
            if (itemNum > 1)
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

        if (displayName == "MainScoring" || displayName == "ScoringButton")
        {
            goButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }


        goButton.GetComponentInChildren<Text>().text = text;
        goButton.GetComponentInChildren<Text>().color = textColor;
        goButton.GetComponent<Image>().color = Color.white;

        ColorBlock colors = goButton.GetComponent<UnityEngine.UI.Button>().colors;
        colors.normalColor = Color.black;
        colors.highlightedColor = Color.gray;
        colors.pressedColor = Color.gray;
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


    public void hideScoreDetailsDisplay(string scoreObject)
    {
        GameObject details = GameObject.Find(scoreObject);
        CanvasGroup scoreCanvasGroup = details.GetComponent<CanvasGroup>();
        scoreCanvasGroup.alpha = 0f;
        scoreCanvasGroup.blocksRaycasts = false;
        FindObjectOfType<Scoring>().getShowingScore = false;
        FindObjectOfType<Scoring>().getShowingGameScore = false;
    }

    public void ScoreButtonClicked()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        scoreDetails = GameObject.Find("ScoreDetails");
        gameDetails = GameObject.Find("GameDetails");
        scoreBox = GameObject.Find("ScoreBox");
        mainScoreBox = GameObject.Find("MainScoreBox");

        Color highlightColor = new Color32(128, 128, 128, 255);
        Color normalColor = new Color32(0, 0, 0, 255);

        ColorBlock scoreBoxColors = scoreBox.GetComponent<UnityEngine.UI.Button>().colors;
        ColorBlock mainScoreBoxColors = mainScoreBox.GetComponent<UnityEngine.UI.Button>().colors;

        bool displayOn = FindObjectOfType<Scoring>().getShowingScore;
        bool gameDisplayOn = FindObjectOfType<Scoring>().getShowingGameScore;

        if (gameDisplayOn && buttonName == "ScoreBox")
        {
            scoreBoxColors.normalColor = normalColor;
            scoreBox.GetComponent<UnityEngine.UI.Button>().colors = scoreBoxColors;
            hideScoreDetailsDisplay("GameDetails");
            return;
        }
        if (displayOn && buttonName == "MainScoreBox")
        {
            mainScoreBoxColors.normalColor = normalColor;
            mainScoreBox.GetComponent<UnityEngine.UI.Button>().colors = mainScoreBoxColors;
            hideScoreDetailsDisplay("ScoreDetails");
            return;
        }

        if (buttonName.Equals("ScoreBox"))
        {
            if (displayOn)
            {
                hideScoreDetailsDisplay("ScoreDetails");                
            }
            //Debug.Log("Scoring Info");
            scoringCanvasGroup = gameDetails.GetComponent<CanvasGroup>();
            mainScoreBoxColors.normalColor = normalColor;            
            scoreBoxColors.normalColor = highlightColor;
            scoreBox.GetComponent<UnityEngine.UI.Button>().colors = scoreBoxColors;
            mainScoreBox.GetComponent<UnityEngine.UI.Button>().colors = mainScoreBoxColors;

            EventSystem.current.SetSelectedGameObject(null);

            scoringCanvasGroup.alpha = 1f;
            scoringCanvasGroup.blocksRaycasts = true;
            FindObjectOfType<Scoring>().getShowingGameScore = true;
            //updateGameScore(0);

        }
        else if (buttonName == "MainScoreBox")
        {
            if (gameDisplayOn)
            {
                hideScoreDetailsDisplay("GameDetails");
            }
            //Debug.Log("Score Details");
            scoringCanvasGroup = scoreDetails.GetComponent<CanvasGroup>();

            scoreBoxColors.normalColor = normalColor;            
            mainScoreBoxColors.normalColor = highlightColor;
            mainScoreBox.GetComponent<UnityEngine.UI.Button>().colors = mainScoreBoxColors;
            scoreBox.GetComponent<UnityEngine.UI.Button>().colors = scoreBoxColors;

            EventSystem.current.SetSelectedGameObject(null);

            scoringCanvasGroup.alpha = 1f;
            scoringCanvasGroup.blocksRaycasts = true;
            FindObjectOfType<Scoring>().getShowingScore = true;
            addToScore(0, "");
        }
    }

    public void initialize(int score, string objective)
    {
        StartCoroutine(InitializeScore(score, objective));             
    }

    IEnumerator InitializeScore(int score, string objective)
    {
        yield return new WaitForSeconds(0.1f);

        string[] allScenes = { "comGame", "comUnscramble", "Assembly 3d", "2_Flightpath", "scene5", "Scanning" };
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        int sceneIndex = Array.IndexOf(allScenes, sceneName);
        int notFound = -1;
        //Debug.Log(sceneIndex);
        //Debug.Log(sceneName);
        scoreBox = GameObject.Find("ScoreBox");
        if (sceneIndex == notFound)
        {            
            scoreBox.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        else
        {
            scoreBox.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        initialized = FindObjectOfType<Scoring>().getInitialized;
        if (initialized == false)
        {
            //Debug.Log("initialize");
            FindObjectOfType<Scoring>().getInitialized = true;
            addToScore(score, objective);
        }
    }

    void OnDestroy()
    {
        FindObjectOfType<Scoring>().getInitialized = false;
        resetGameScore(); 
    }

    public void addToScore(int score, string objective)
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
            default:
                FindObjectOfType<Scoring>().getBlankScore = 0;
                scoringObj.GetComponentInChildren<Text>().text = scanningScore.ToString();
                break;
        }

        setCurrentScore();

        if (objective != "")
        {
            gameScoreDetails(score, objective, false);
        }

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
            //Debug.Log(score);
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


    public void gameScoreDetails(int score, string objectName, bool reset)
    {
        GameObject gameScoreObj = GameObject.Find(objectName);
        int newScore = 0;
        switch (objectName)
        {
            case "ComObjective1":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective1 + score;
                }                
                FindObjectOfType<Scoring>().getComObjective1 = newScore;
                break;
            case "ComObjective2":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective2 + score;
                }
                FindObjectOfType<Scoring>().getComObjective2 = newScore;
                break;
            case "ComObjective3":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective3 + score;
                }
                FindObjectOfType<Scoring>().getComObjective3 = newScore;
                break;
            case "ComObjective4":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective4 + score;
                }
                FindObjectOfType<Scoring>().getComObjective4 = newScore;
                break;
            case "ComObjective5":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective5 + score;
                }
                FindObjectOfType<Scoring>().getComObjective5 = newScore;
                break;

            case "ComObjective6":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective6 + score;
                }   
                FindObjectOfType<Scoring>().getComObjective6 = newScore;
                break;
            case "ComObjective7":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective7 + score;
                }
                FindObjectOfType<Scoring>().getComObjective7 = newScore;
                break;
            case "ComObjective8":
                //Debug.Log("here");
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective8 + score;
                }
                FindObjectOfType<Scoring>().getComObjective8 = newScore;
                break;
            case "ComObjective9":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective9 + score;
                }
                FindObjectOfType<Scoring>().getComObjective9 = newScore;
                break;
            case "ComObjective10":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective10 + score;
                }
                FindObjectOfType<Scoring>().getComObjective10 = newScore;
                break;
            case "ComObjective11":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective11 + score;
                }
                FindObjectOfType<Scoring>().getComObjective11 = newScore;
                break;
            case "ComObjective12":
                if (!reset)
                {
                    newScore = FindObjectOfType<Scoring>().getComObjective12 + score;
                }
                FindObjectOfType<Scoring>().getComObjective12 = newScore;
                break;
        }

        gameScoreObj = GameObject.Find(objectName);
        gameScoreObj.transform.GetChild(0).GetComponent<Text>().text = newScore.ToString();
    }


    public void resetObjectives(string [] gameObjectives)
    {
        foreach (string objective in gameObjectives)
        {
            gameScoreDetails(0, objective, true);
        }
    }


    public void resetGameScore()
    {
        //Debug.Log("reset");
        GameObject scoringObj = GameObject.Find("ScoreBox");
        GameObject gameScore;

        string[] comPuzzleObjectives = { "ComObjective1", "ComObjective2", "ComObjective3", "ComObjective4", "ComObjective5" };
        string[] comUnscrambleObjective = { "ComObjective6", "ComObjective7", "ComObjective8", "ComObjective9", "ComObjective10", "ComObjective11", "ComObjective12", };
        string[] assemblyObjectives = { };
        string[] flightpathObjectives = { };
        string[] scene5Objectives = { };
        string[] scanningObjectives = { };

        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        switch (sceneName)
        {

            case "comGame":
                comPuzzleScore = 0;
                FindObjectOfType<Scoring>().getComPuzzleScore = comPuzzleScore;
                scoringObj.GetComponentInChildren<Text>().text = comPuzzleScore.ToString();
                gameScore = GameObject.Find("ComPuzzleScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = comPuzzleScore.ToString();
                resetObjectives(comPuzzleObjectives);
                break;
            case "comUnscramble":
                comUnscrambleScore = 0;
                FindObjectOfType<Scoring>().getComUnscrambleScore = comUnscrambleScore;
                scoringObj.GetComponentInChildren<Text>().text = comUnscrambleScore.ToString();
                gameScore = GameObject.Find("ComUnscrambleScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = comUnscrambleScore.ToString();
                resetObjectives(comUnscrambleObjective);
                break;
            case "Assembly 3d":
                assemblyScore = 0;
                FindObjectOfType<Scoring>().getAssemblyScore = assemblyScore;
                scoringObj.GetComponentInChildren<Text>().text = assemblyScore.ToString();
                gameScore = GameObject.Find("AssemblyScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = assemblyScore.ToString();
                resetObjectives(assemblyObjectives);
                break;
            case "2_Flightpath":
                flightPathScore = 0;
                FindObjectOfType<Scoring>().getFlightPathScore = flightPathScore;
                scoringObj.GetComponentInChildren<Text>().text = flightPathScore.ToString();
                gameScore = GameObject.Find("FlightPathScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = flightPathScore.ToString();
                resetObjectives(flightpathObjectives);
                break;
            case "scene5":
                labScore = 0;
                FindObjectOfType<Scoring>().getLabScore = labScore;
                scoringObj.GetComponentInChildren<Text>().text = labScore.ToString();
                gameScore = GameObject.Find("LabScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = labScore.ToString();
                resetObjectives(scene5Objectives);
                break;
            case "Scanning":
                scanningScore = 0;
                FindObjectOfType<Scoring>().getScanningScore = scanningScore;
                scoringObj.GetComponentInChildren<Text>().text = scanningScore.ToString();
                gameScore = GameObject.Find("ScanningScore");
                gameScore.transform.GetChild(0).GetComponent<Text>().text = scanningScore.ToString();
                resetObjectives(scanningObjectives);
                break;
        }

        setCurrentScore();

    }
}
