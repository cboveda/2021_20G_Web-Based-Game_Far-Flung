using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DialogMaker;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    static HubControlFlow hubController;
    static List<GameObject> mainMenuButtons = new List<GameObject>();
    static List<GameObject> levelSelectButtons = new List<GameObject>();
    static bool mainMenuInitialized;

    // Main Menu Buttons
    static GameObject playCampaignButton;
    static GameObject exploreButton;
    static GameObject voicesButton;
    static GameObject exitButton;

    // Level Select Buttons
    static GameObject playAssemblyButton;
    static GameObject playFlightPlanButton;
    static GameObject playScanningButton;
    static GameObject playCommsButton;
    static GameObject playLabButton;
    static GameObject backToMainButton;

    // Other Menu Elements

    static GameObject logo;
    static GameObject uiBackground;
    static GameObject voicesButtonText;
    


    // Start is called before the first frame update
    void Start()
    {
        hubController = GameObject.Find("HubControlFlow").GetComponent<HubControlFlow>();

        if(mainMenuButtons.Count == 0)
        {
            // Main Menu Buttons
            playCampaignButton = GameObject.Find("PlayButton");
            exploreButton = GameObject.Find("ExploreButton");
            voicesButton = GameObject.Find("VoicesButton");

            voicesButtonText = GameObject.Find("VoicesButtonText");
            
            exitButton = GameObject.Find("ExitButton");

            mainMenuButtons.Add(playCampaignButton);
            mainMenuButtons.Add(exploreButton);
            mainMenuButtons.Add(voicesButton);
            mainMenuButtons.Add(exitButton);
        }

        if (levelSelectButtons.Count == 0)
        {
            // Level Select Buttons
            playAssemblyButton = GameObject.Find("PlayButtonAssembly");
            playFlightPlanButton = GameObject.Find("PlayButtonFlightPlanning");
            playScanningButton = GameObject.Find("PlayButtonScanning");
            playCommsButton = GameObject.Find("PlayButtonComms");
            playLabButton = GameObject.Find("PlayButtonLab");
            backToMainButton = GameObject.Find("BackToMain");

            levelSelectButtons.Add(playAssemblyButton);
            levelSelectButtons.Add(playFlightPlanButton);
            levelSelectButtons.Add(playScanningButton);
            levelSelectButtons.Add(playCommsButton);
            levelSelectButtons.Add(playLabButton);
            levelSelectButtons.Add(backToMainButton);
        }

        uiBackground = GameObject.Find("UIBackground");
        logo = GameObject.Find("FarFlungLogoImg");

        if (DialogGenerator.AudioOn)
        {
            voicesButtonText.GetComponent<Text>().text = "Voices: On";
        }
        else
        {
            voicesButtonText.GetComponent<Text>().text = "Voices: Off";
        }

        if (!mainMenuInitialized)
        {
            mainMenuInitialized = true;
            
            SwitchToMainMenu();

        }

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public static void PlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        
        hubController.StartIntro();

    }

    public static void LevelSelectButtonClicked()
    {
        Debug.Log("Level select button clicked");
        SwitchToLevelSelect();

    }

    public static void BackToMainButtonClicked()
    {
        Debug.Log("Back to main menu button clicked");
        SwitchToMainMenu();

    }

    public static void SwitchVoiceOnOff()
    {
        if (DialogGenerator.AudioOn)
        {
            DialogGenerator.AudioOn = false;
            GameObject.Find("VoicesButtonText").GetComponent<Text>().text = "Voices: Off";
        }
        else
        {
            DialogGenerator.AudioOn = true;
            GameObject.Find("VoicesButtonText").GetComponent<Text>().text = "Voices: On";
        }
    }

    public static void AssemblyButtonClicked()
    {
        hubController.LaunchAssembly();

    }

    public static void FlightPlanningButtonClicked()
    {
        hubController.LaunchFlightPlanning();

    }

    public static void ScanningButtonClicked()
    {
        hubController.LaunchScanning();

    }

    public static void CommsButtonClicked()
    {
        hubController.LaunchComms();

    }

    public static void LabButtonClicked()
    {
        hubController.LaunchLab();

    }

    private static void SwitchToLevelSelect()
    {
        foreach (GameObject button in mainMenuButtons)
        {
            button.SetActive(false);
        }
        foreach (GameObject button in levelSelectButtons)
        {
            button.SetActive(true);
        }
    }

    private static void SwitchToMainMenu()
    {
        foreach (GameObject button in levelSelectButtons)
        {
            button.SetActive(false);
        }
        foreach ( GameObject button in mainMenuButtons) {
            button.SetActive(true);
        }
        
        
    }

    public static void HideMainMenu()
    {
        uiBackground.SetActive(false);
        logo.SetActive(false);
        foreach (GameObject button in mainMenuButtons)
        {
            button.SetActive(false);
        }
        foreach (GameObject button in levelSelectButtons)
        {
            button.SetActive(false);
        }
        ResetMenu();
    }
    public static void ShowMainMenu()
    {
        uiBackground.SetActive(true);
        logo.SetActive(true);
        SwitchToMainMenu();
    }

    public static void ResetMenu()
    {
        mainMenuInitialized = false;
        mainMenuButtons.Clear();
        levelSelectButtons.Clear();
    }

    public static void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
