using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Flightpath
{
    public class TimelineController : MonoBehaviour
    {
        private PlayableDirector director;
        public GameObject startButton;

        public void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.played += Director_Played;
            director.stopped += Director_Stopped;
        }

        public void Director_Stopped(PlayableDirector o)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Director_Played(PlayableDirector o)
        {
            startButton.SetActive(false);
        }

        public void StartTimeline()
        {
            director.Play();
        }
    }
}