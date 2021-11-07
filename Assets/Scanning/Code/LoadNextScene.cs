using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    public void LoadScene() {
        StartCoroutine(LoadSceneInBG());
    }

    IEnumerator LoadSceneInBG() {

        AsyncOperation nextScene = SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );

        nextScene.allowSceneActivation = false;

        while ( !nextScene.isDone ) { // creates game objects in next scene

            if (nextScene.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // add delay to finish awake call, happens in unity an unspecified amount of time after 0.9% loaded
                nextScene.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
