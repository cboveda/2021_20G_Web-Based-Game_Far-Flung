using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DialogMaker;

public class MainMenu : MonoBehaviour
{
    int sceneNum;
    DialogGenerator diagIntro;
    DialogGenerator diagFlight;
    DialogGenerator diagScan;
    bool introStarted;
    // Start is called before the first frame update
    void Start()
    {
        diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();
        diagFlight = GameObject.Find("DialogFlight").GetComponent<DialogGenerator>();
        diagScan = GameObject.Find("DialogScan").GetComponent<DialogGenerator>();
        introStarted = false;
        
        switch (HubTracker.LevelToLoad)
        {
            case 2:
                diagFlight.BeginPlayingDialog();
                break;
            case 3:
                diagScan.BeginPlayingDialog();
                break;
            default:
                break;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (introStarted)
        {
            //if (diagIntro.AllDialogComplete())
            if(diagIntro == null && HubTracker.LevelToLoad == 1)
            {
                //Scene scene = SceneManager.GetActiveScene();
                //sceneNum = scene.buildIndex;
                SceneManager.LoadScene(HubTracker.LevelToLoad++ + 1);
            }
            else if(diagFlight == null && HubTracker.LevelToLoad == 2)
            {
                SceneManager.LoadScene(HubTracker.LevelToLoad++ + 1);
            }
            else if (diagScan == null && HubTracker.LevelToLoad == 3)
            {
                SceneManager.LoadScene(HubTracker.LevelToLoad++ + 1);
            }
        }
        
    }

    public void PlayButtonClicked()
    {
        Debug.Log("Play button clicked");

        //Scene scene = SceneManager.GetActiveScene();
        //sceneNum = scene.buildIndex;


        diagIntro.BeginPlayingDialog();
        introStarted = true;
        

        //SceneManager.LoadScene(sceneNum + 1);
    }
}
