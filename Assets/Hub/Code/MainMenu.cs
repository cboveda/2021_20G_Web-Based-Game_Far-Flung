using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int sceneNum;
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

        Scene scene = SceneManager.GetActiveScene();
        sceneNum = scene.buildIndex;
        SceneManager.LoadScene(sceneNum + 1);
    }
}
