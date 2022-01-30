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
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void PlayButtonClicked()
    {
        Debug.Log("Play button clicked");

        //Scene scene = SceneManager.GetActiveScene();
        //sceneNum = scene.buildIndex;

        diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();
        diagIntro.BeginPlayingDialog();
        HubTracker.LevelToLoad = 1;
        HubTracker.IntroStarted = true;
        //introStarted = true;
        

        //SceneManager.LoadScene(sceneNum + 1);
    }
}
