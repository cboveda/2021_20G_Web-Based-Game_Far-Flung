using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadWithLoading : MonoBehaviour {

    public GameObject buttonParent;
    public Slider slider;

    public void LoadScene() {
        
        buttonParent.SetActive(false);
        StartCoroutine(LoadSceneInBG());
    }

    IEnumerator LoadSceneInBG() {

        AsyncOperation nextScene = SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );

        nextScene.allowSceneActivation = false;

        while ( !nextScene.isDone ) { // creates game objects in next scene

            slider.value = nextScene.progress;

            if (nextScene.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // add delay to finish awake call, happens in unity an unspecified amount of time after 90% loaded
                nextScene.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}