using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineControllerOutro : MonoBehaviour
{
    private PlayableDirector director;

    private void Awake() {
        director = GetComponent<PlayableDirector>();
    }

    private void Director_Stopped()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
