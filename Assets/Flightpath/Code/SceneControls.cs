using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControls : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Previous()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
