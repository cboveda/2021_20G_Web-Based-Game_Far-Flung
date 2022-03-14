using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Flightpath
{
    /*
        Orchestrates the animation timeline of the flightpath outro scene, and triggers scene transitions.
        Author: Chris Boveda
    */
    public class TimelineControllerOutro : MonoBehaviour
    {
        [SerializeField]
        private float _timeScale;
        private PlayableDirector director;

        public void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.stopped += Director_Stopped;
            Time.timeScale = _timeScale;
        }

        public void Director_Stopped(PlayableDirector o)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Hub");
        }
    }
}