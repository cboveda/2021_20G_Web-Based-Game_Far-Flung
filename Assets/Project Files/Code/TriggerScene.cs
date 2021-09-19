using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TriggerScene : MonoBehaviour
{

    string buttonName = "";
    string sceneName = "";
    string sceneNum = "";
    int next = 0;
    int prev = 0;
    string nextScene = "";
    string prevScene = "";

    public void StartScene()
    {

        Debug.Log("button clicked");

        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        sceneNum = sceneName.Substring(5);

        next = int.Parse(sceneNum) + 1;
        prev = int.Parse(sceneNum) - 1;

        nextScene = "scene" + next.ToString();
        prevScene = "scene" + prev.ToString();


        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "NextScene")
        {
            SceneManager.LoadScene(nextScene);
        }
        else if (buttonName == "PreviousScene")
        {
            SceneManager.LoadScene(prevScene);
        }

    }

}
