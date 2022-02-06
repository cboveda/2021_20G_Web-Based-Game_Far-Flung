using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadFromStart : MonoBehaviour {

    // public GameObject loadingScreen;
    // public Slider slider;

    public void LoadScene() {
        
        // loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneInBG());
    }

    IEnumerator LoadSceneInBG() {

        AsyncOperation nextScene = SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );

        nextScene.allowSceneActivation = false;

        while ( !nextScene.isDone ) { // creates game objects in next scene

            // slider.value = Mathf.Clamp01( nextScene.progress / 0.9f );

            if (nextScene.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // allow activation stage on next scene
                nextScene.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}