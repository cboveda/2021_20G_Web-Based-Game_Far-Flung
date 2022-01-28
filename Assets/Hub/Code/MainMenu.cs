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
    bool introStarted;
    // Start is called before the first frame update
    void Start()
    {
        diagIntro = GameObject.Find("DialogIntro").GetComponent<DialogGenerator>();
        introStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (introStarted)
        {
            //if (diagIntro.AllDialogComplete())
            if(diagIntro == null)
            {
                Scene scene = SceneManager.GetActiveScene();
                sceneNum = scene.buildIndex;
                SceneManager.LoadScene(sceneNum + 1);
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
