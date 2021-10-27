using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TriggerScene : MonoBehaviour
{
    private string buttonName;
    private int sceneNum;
    private int next = -1;
    private int prev = -1;

    public void StartScene()
    {

        Debug.Log("button clicked");

        Scene scene = SceneManager.GetActiveScene();
        sceneNum = scene.buildIndex;
        Debug.Log(sceneNum);

        next = sceneNum + 1;
        prev = (sceneNum>0) ? sceneNum - 1: sceneNum;


        buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "NextScene")
        {
            LoadNext();
        }
        else if (buttonName == "PreviousScene")
        {
            LoadPrevious();
        }

    }

    public void LoadNext()
    {
        if (next == -1) next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void LoadPrevious()
    {
        if (prev == -1)
        {
            int current = SceneManager.GetActiveScene().buildIndex;
            prev = (current>0)?current - 1: current;
        }
        SceneManager.LoadScene(prev);
    }

}
