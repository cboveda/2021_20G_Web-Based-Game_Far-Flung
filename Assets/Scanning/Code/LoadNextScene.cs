using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public void LoadScene() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }

}
