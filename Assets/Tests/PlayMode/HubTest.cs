using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using DialogMaker;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class HubTest
{

    //private CinemachineVirtualCamera assemblyCamera;
    //private CinemachineVirtualCamera labCamera;
    //private CinemachineVirtualCamera flightPlanCamera;
    //private CinemachineVirtualCamera missionControlCamera;
    //private CinemachineVirtualCamera commsCamera;
    //private List<CinemachineVirtualCamera> virtualCameras;
    //private CameraPositionController cameraController;
    //private const int CAMERA_PRIORITY_HIGH = 1000;
    //private const int CAMERA_PRIORITY_LOW = 10;

    //private Dialog line1 = new Dialog("Line1 is not too long of a line but needs to be long enough", RobotCharacter.Serious);

    DialogGenerator diagIntro;
    DialogGenerator diagFlight;
    DialogGenerator diagScan;
    DialogGenerator diagComms;
    DialogGenerator diagLab;
    DialogGenerator diagOutro;

    [UnityTest]
    public IEnumerator Test_Hub()
    {
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);

        GameObject goHubControl = GameObject.Find("HubControlFlow");
        HubControlFlow hubController = goHubControl.GetComponent<HubControlFlow>();

        Assert.AreEqual(false, HubTracker.IntroStarted);
        Assert.AreEqual(HubTracker.LevelToLoad, 0);

        // Mimic game start

        hubController.StartIntro();
        yield return new WaitForSeconds(3.1f);
        diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();
        diagIntro.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(2, HubTracker.LevelToLoad);

        // Mimic second load of Hub: into Flight
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(2, HubTracker.LevelToLoad);
        diagFlight = GameObject.Find("DialogFlight").GetComponent<DialogGenerator>();
        diagFlight.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(3, HubTracker.LevelToLoad);

        // Mimic third load of Hub: into Scan
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(3, HubTracker.LevelToLoad);
        diagScan = GameObject.Find("DialogScan").GetComponent<DialogGenerator>();
        diagScan.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(4, HubTracker.LevelToLoad);

        // Mimic fourth load of Hub: into Comms
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(4, HubTracker.LevelToLoad);
        diagComms = GameObject.Find("DialogComms").GetComponent<DialogGenerator>();
        diagComms.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(5, HubTracker.LevelToLoad);

        // Mimic fifth load of Hub: into Lab
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(5, HubTracker.LevelToLoad);
        diagLab = GameObject.Find("DialogLab").GetComponent<DialogGenerator>();
        diagLab.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(6, HubTracker.LevelToLoad);

        // Mimic sixth load of Hub: into Outro
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, HubTracker.IntroStarted);
        Assert.AreEqual(6, HubTracker.LevelToLoad);
        diagOutro = GameObject.Find("DialogOutro").GetComponent<DialogGenerator>();
        diagOutro.FastForwardDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(0, HubTracker.LevelToLoad);
        //SceneManager.UnloadScene("Hub");
        //SceneManager.UnloadScene("1_FlightpathIntro");
        MainMenu.ResetMenu();
        SceneManager.UnloadSceneAsync("Hub");

    }

    [UnityTest]
    public IEnumerator Test_Cameras()
    {

        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        string[] buttonNames =
        {
        "Camera Menu",
        "Assembly",
        "Flight Planning",
        "Mission Control",
        "Communications",
        "Lab Analysis"
         };

        CinemachineVirtualCamera assemblyCamera;
        CinemachineVirtualCamera labCamera;
        CinemachineVirtualCamera flightPlanCamera;
        CinemachineVirtualCamera missionControlCamera;
        CinemachineVirtualCamera commsCamera;

        assemblyCamera = GameObject.Find("vcamAssembly").GetComponent<CinemachineVirtualCamera>();
        labCamera = GameObject.Find("vcamLab").GetComponent<CinemachineVirtualCamera>();
        flightPlanCamera = GameObject.Find("vcamFlightPlanning").GetComponent<CinemachineVirtualCamera>();
        missionControlCamera = GameObject.Find("vcamMissionControl").GetComponent<CinemachineVirtualCamera>();
        commsCamera = GameObject.Find("vcamComms").GetComponent<CinemachineVirtualCamera>();



        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        DebugCamera dbCamera = GameObject.Find("DebugCams").GetComponent<DebugCamera>();

        yield return new WaitForSeconds(0.1f);

        GameObject goMenuButton = GameObject.Find("Camera Menu");
        Button menuButton = goMenuButton.GetComponent<Button>();
        
        eventSystem.SetSelectedGameObject(menuButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        menuButton.onClick.Invoke();

        yield return new WaitForSeconds(0.1f);

        GameObject goAssemblyButton = GameObject.Find("Assembly");
        Button assemblyButton = goAssemblyButton.GetComponent<Button>();
        GameObject goFlightButton = GameObject.Find("Flight Planning");
        Button flightButton = goFlightButton.GetComponent<Button>();
        GameObject goCommsButton = GameObject.Find("Communications");
        Button commsButton = goCommsButton.GetComponent<Button>();
        GameObject goScanButton = GameObject.Find("Mission Control");
        Button scanButton = goScanButton.GetComponent<Button>();
        GameObject goLabButton = GameObject.Find("Lab Analysis");
        Button labButton = goLabButton.GetComponent<Button>();

        eventSystem.SetSelectedGameObject(assemblyButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        assemblyButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1000, assemblyCamera.Priority);

        eventSystem.SetSelectedGameObject(flightButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        flightButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1000, flightPlanCamera.Priority);

        eventSystem.SetSelectedGameObject(commsButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        commsButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1000, commsCamera.Priority);

        eventSystem.SetSelectedGameObject(scanButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        scanButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1000, missionControlCamera.Priority);

        eventSystem.SetSelectedGameObject(labButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        labButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1000, labCamera.Priority);
        Assert.AreEqual(10, assemblyCamera.Priority);

        eventSystem.SetSelectedGameObject(menuButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        menuButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(false, assemblyButton.IsActive());

        SceneManager.UnloadSceneAsync("Hub");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Assembly");
        yield return new WaitForSeconds(0.1f);
        MainMenu.ResetMenu();
    }

    [UnityTest]
    public IEnumerator Test_Menu_System()
    {
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.1f);
        MainMenu.AssemblyButtonClicked();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Assembly");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("VoicesButton").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(GameObject.Find("VoicesButtonText").GetComponent<Text>().text, "Voices: Off");

        MainMenu.FlightPlanningButtonClicked();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "1_FlightpathIntro");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.2f);

        MainMenu.ScanningButtonClicked();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "StartScene.Scanning");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.2f);

        MainMenu.CommsButtonClicked();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "comGameIntro");
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Hub");
        yield return new WaitForSeconds(0.2f);

        MainMenu.LabButtonClicked();
        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "scene5");
        yield return new WaitForSeconds(0.1f);


        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Assembly");
        yield return new WaitForSeconds(0.1f);
        MainMenu.ResetMenu();

        SceneManager.LoadScene("Hub");
    }



}
