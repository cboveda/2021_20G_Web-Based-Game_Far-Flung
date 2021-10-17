using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour
{

    string gameScene = "";

    public void StartGameScene()
    {
        gameScene = "comGame";
        SceneManager.LoadScene(gameScene);
    }



}
