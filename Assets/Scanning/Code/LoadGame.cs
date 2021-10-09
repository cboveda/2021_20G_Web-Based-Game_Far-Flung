using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadMainScene() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }

}
