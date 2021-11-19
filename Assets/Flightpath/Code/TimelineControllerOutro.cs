using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Flightpath
{
    public class TimelineControllerOutro : MonoBehaviour
    {
        private PlayableDirector director;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.stopped += Director_Stopped;
        }

        private void Director_Stopped(PlayableDirector o)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}