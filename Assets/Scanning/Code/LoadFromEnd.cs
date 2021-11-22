using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadFromEnd : MonoBehaviour
{
    public FadeDriver fadeDriver;

    public void LoadNextScene() {
        StartCoroutine( TriggerTransition( SceneManager.GetActiveScene().buildIndex + 1)  );
    }
    public void RestartGame() {
        StartCoroutine( TriggerTransition( SceneManager.GetActiveScene().buildIndex - 2)  );
    }

    IEnumerator TriggerTransition( int sceneIndex ) {
        fadeDriver.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene( sceneIndex );
    }
}