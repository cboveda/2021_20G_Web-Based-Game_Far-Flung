using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using DialogMaker;

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

        public void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.stopped += Director_Stopped;
            Time.timeScale = _timeScale;
        }

        public void Start()
        {
            director.Play();
        }

        public void Director_Stopped(PlayableDirector o)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}