using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DialogMaker;
using Cinemachine;

public class HubControlFlow : MonoBehaviour
{
    int sceneNum;
    bool cameraTransitionedToNewLevel;

    private CinemachineVirtualCamera assemblyCamera;
    private CinemachineVirtualCamera labCamera;
    private CinemachineVirtualCamera flightPlanCamera;
    private CinemachineVirtualCamera missionControlCamera;
    private CinemachineVirtualCamera commsCamera;
    private List<CinemachineVirtualCamera> virtualCameras;
    private CameraPositionController cameraController;
    private const int CAMERA_PRIORITY_HIGH = 1000;
    private const int CAMERA_PRIORITY_LOW = 10;
    private MusicPlayer musicPlayer;

    DialogGenerator diagIntro;
    DialogGenerator diagFlight;
    DialogGenerator diagScan;
    DialogGenerator diagComms;
    DialogGenerator diagLab;
    DialogGenerator diagOutro;
    AudioSource bgAudio;
    FadeController fader;
    //MainMenu mainMenu;
    bool introStarted;
    // Start is called before the first frame update
    void Start()
    {
        //mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        if (!HubTracker.FirstLoad)
        {
            DialogGenerator.AudioOn = true;
            HubTracker.FirstLoad = true;
            //HideLevelSelectButtons();
        }
        musicPlayer = GameObject.Find("MainMusic").GetComponent<MusicPlayer>();
        diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();

        diagFlight = GameObject.Find("DialogFlight").GetComponent<DialogGenerator>();
        diagScan = GameObject.Find("DialogScan").GetComponent<DialogGenerator>();
        diagComms = GameObject.Find("DialogComms").GetComponent<DialogGenerator>();
        diagLab = GameObject.Find("DialogLab").GetComponent<DialogGenerator>();
        diagOutro = GameObject.Find("DialogOutro").GetComponent<DialogGenerator>();
        fader = GameObject.Find("FadeController").GetComponent<FadeController>();

        introStarted = false;
        cameraTransitionedToNewLevel = false;
        musicPlayer.StopMusic();
        bgAudio = GameObject.Find("Background Music").GetComponent<AudioSource>();

        bgAudio.volume = 0.5f;

        virtualCameras = new List<CinemachineVirtualCamera>();
        ReadyCameras();

        switch (HubTracker.LevelToLoad)
        {
            case 2:
                SetPriorityCamera(assemblyCamera);
                HideMainMenu();
                diagFlight.BeginPlayingDialog();
                //SetPriorityCamera(flightPlanCamera);
                break;
            case 3:
                SetPriorityCamera(flightPlanCamera);
                HideMainMenu();
                diagScan.BeginPlayingDialog();
                //SetPriorityCamera(missionControlCamera);
                break;
            case 4:
                SetPriorityCamera(missionControlCamera);
                HideMainMenu();
                diagComms.BeginPlayingDialog();
                //SetPriorityCamera(commsCamera);
                break;
            case 5:
                SetPriorityCamera(commsCamera);
                HideMainMenu();
                diagLab.BeginPlayingDialog();
                //SetPriorityCamera(labCamera);
                break;
            case 6:
                SetPriorityCamera(labCamera);
                HideMainMenu();
                diagOutro.BeginPlayingDialog();
                //SetPriorityCamera(assemblyCamera);
                break;
            default:
                break;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (HubTracker.IntroStarted)
        {
            bgAudio.volume = 0.1f;
        }

        if (!cameraTransitionedToNewLevel)
        {
            cameraTransitionedToNewLevel = true;
            StartCoroutine(SetCameraForUpcomingLevel());
        }

        if (HubTracker.LevelToLoad >= 0)
        {
            //if (diagIntro.AllDialogComplete())
            if (diagIntro == null && HubTracker.LevelToLoad == 1)
            {
                //Scene scene = SceneManager.GetActiveScene();
                //sceneNum = scene.buildIndex;
                //SceneManager.LoadScene(HubTracker.LevelToLoad++ + 1);
                IncrementLevelToLoad();
                LaunchAssembly();
            }
            else if (diagFlight == null && HubTracker.LevelToLoad == 2)
            {
                IncrementLevelToLoad();
                LaunchFlightPlanning();


            }
            else if (diagScan == null && HubTracker.LevelToLoad == 3)
            {
                IncrementLevelToLoad();
                LaunchScanning();
            }
            else if (diagComms == null && HubTracker.LevelToLoad == 4)
            {
                IncrementLevelToLoad();
                LaunchComms();
            }
            else if (diagLab == null && HubTracker.LevelToLoad == 5)
            {
                IncrementLevelToLoad();
                LaunchLab();
            }
            else if (diagOutro == null && HubTracker.LevelToLoad == 6)
            {
                introStarted = false;
                HubTracker.IntroStarted = false;
                IncrementLevelToLoad();
                SceneManager.LoadScene("Hub");
                
                //HubTracker.LevelToLoad++;
                //SceneManager.LoadScene("scene5");
            }
        }
    }

    public void Fade()
    {
        fader.Fade();
    }

    public void StartIntro()
    {
        StartCoroutine(BeginGame());
    }

    private IEnumerator SetCameraForUpcomingLevel()
    {
        yield return new WaitForSeconds(1);
        switch (HubTracker.LevelToLoad)
        {
            case 2:
                SetPriorityCamera(flightPlanCamera);
                break;
            case 3:
                SetPriorityCamera(missionControlCamera);
                break;
            case 4:
                SetPriorityCamera(commsCamera);
                break;
            case 5:
                SetPriorityCamera(labCamera);
                break;
            case 6:
                SetPriorityCamera(assemblyCamera);
                break;
            default:
                break;

        }
    }

    IEnumerator BeginGame()
    {
        //diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();
        Fade();
        yield return new WaitForSeconds(2);
        diagIntro.BeginPlayingDialog();
        HubTracker.LevelToLoad = 1;
        HubTracker.IntroStarted = true;
        HideMainMenu();
        //mainMenu.SetActive(false);
        yield return new WaitForSeconds(2);
        fader.ResetAndFade();
    }

    IEnumerator delay(int amount)
    {
        yield return new WaitForSeconds(amount);
    }

    public void HideMainMenu()
    {
        MainMenu.HideMainMenu();
        //GameObject.Find("UIBackground").SetActive(false);
        //GameObject.Find("PlayButton").SetActive(false);
        //GameObject.Find("ExploreButton").SetActive(false);
        //GameObject.Find("FarFlungLogoImg").SetActive(false);
    }

    public void ShowMainMenu()
    {
        MainMenu.ShowMainMenu();
        //GameObject.Find("UIBackground").SetActive(true);
        //GameObject.Find("PlayButton").SetActive(true);
        //GameObject.Find("ExploreButton").SetActive(true);
        //GameObject.Find("FarFlungLogoImg").SetActive(true);
    }

    private void IncrementLevelToLoad()
    {
        if (HubTracker.IntroStarted)
        {
            HubTracker.LevelToLoad++;
        }
        else
        {
            HubTracker.LevelToLoad = 0;
        }
    }

    public void LaunchAssembly()
    {
        HideMainMenu();
        musicPlayer.StartMusic();
        SceneManager.LoadScene("Assembly");
    }

    public void LaunchFlightPlanning()
    {
        HideMainMenu();
        musicPlayer.StartMusic();
        SceneManager.LoadScene("1_FlightpathIntro");
    }

    public void LaunchScanning()
    {
        HideMainMenu();
        musicPlayer.StartMusic();
        SceneManager.LoadScene("StartScene.Scanning");
    }
    public void LaunchComms()
    {
        HideMainMenu();
        musicPlayer.StartMusic();
        SceneManager.LoadScene("comGameIntro");
    }

    public void LaunchLab()
    {
        HideMainMenu();
        musicPlayer.StartMusic();
        SceneManager.LoadScene("scene5");
    }

    private void ReadyCameras()
    {
        virtualCameras.Add(assemblyCamera = GameObject.Find("vcamAssembly").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(labCamera = GameObject.Find("vcamLab").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(flightPlanCamera = GameObject.Find("vcamFlightPlanning").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(missionControlCamera = GameObject.Find("vcamMissionControl").GetComponent<CinemachineVirtualCamera>());
        virtualCameras.Add(commsCamera = GameObject.Find("vcamComms").GetComponent<CinemachineVirtualCamera>());

        cameraController = GameObject.Find("CameraControlScript").GetComponent<CameraPositionController>();
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
}
