using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flightpath
{
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
}