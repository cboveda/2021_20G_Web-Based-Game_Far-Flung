using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFromEnd : MonoBehaviour
{
    public void LoadNextScene() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }
    public void RestartGame() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 2 );
    }
}