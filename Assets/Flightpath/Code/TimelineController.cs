using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Flightpath
{
    /*
        Orchestrates the animation timeline of the flightpath intro scene, and triggers scene transitions.
        Author: Chris Boveda
    */
    public class TimelineController : MonoBehaviour
    {
        [SerializeField]
        private float _timeScale;
        private PlayableDirector director;
        public GameObject startButton;

        public void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.played += Director_Played;
            director.stopped += Director_Stopped;
            Time.timeScale = _timeScale;
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