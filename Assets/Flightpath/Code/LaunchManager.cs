using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Flightpath
{
    public class LaunchManager : MonoBehaviour
    {
        public GameObject Satellite;
        public Slider AngleSlider;
        public Slider PowerSlider;
        public Trajectory TrajectoryArrow;
        public SatellitePathDrawing SatellitePath;

        // Placeholder members for win/lose text
        public Text WinText;
        public Text LoseText;
        private bool _sceneAdvanceStart;
        private bool _launchLocked;


        public void Start()
        {
            Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
            Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);
            TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
            TrajectoryArrow.SetPowerRange(PowerSlider.GetComponent<Slider>().minValue, PowerSlider.GetComponent<Slider>().maxValue);
            _sceneAdvanceStart = false;
            _launchLocked = false;
            SatellitePath.Active = false;
            // todo
            ResetPlaceholderText();
        }

        public void OnAngleSliderChanged(float value)
        {
            Satellite.GetComponent<Launch>().SetAngle(value);
        }

        public void OnPowerSliderChanged(float value)
        {
            Satellite.GetComponent<Launch>().SetPower(value);

        }

        public void OnLaunchButtonClicked()
        {
            if (!_launchLocked)
            {
                _launchLocked = true;
                TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = false;
                Satellite.GetComponent<Launch>().DoLaunch();
                PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
                foreach (PathFollower p in pathFollowers)
                {
                    p.BeginMovement();
                    p.StartOrbitter();
                }
                SatellitePath.ClearHistory();
                SatellitePath.Active = true;
            }
        }

        public void OnResetButtonClicked()
        {
            Satellite.GetComponent<Launch>().ResetLaunch();
            TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
            PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
            foreach (PathFollower p in pathFollowers)
            {
                p.ResetPosition();
                p.StopOrbitter();
            }
            ResetPlaceholderText();
            SatellitePath.Active = false;
            var particles = Satellite.GetComponent<ParticleSystem>();
            if (particles != null) {
                particles.Stop();
                particles.Clear();
            }
            _launchLocked = false;
        }

        public void OnAsteroidCollisionDetected()
        {
            Satellite.GetComponent<Launch>().StopLaunch();
            PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
            foreach (PathFollower p in pathFollowers)
            {
                p.StopPosition();
                p.StopOrbitter();
            }
            SatellitePath.Active = false;

            if (!_sceneAdvanceStart)
            {
                StartCoroutine("DelayedSceneAdvance");
            }
            // todo
            EnablePlaceholderWinText();
        }

        public void OnMarsCollisionDetected() 
        {
            StopAll();
            LoseText.text = "Oops, ran into Mars! Try again."; 
            LoseText.enabled = true;
            Satellite.GetComponent<ParticleSystem>().Play();
        }

        public void OnSatelliteLeaveWindow()
        {
            StopAll();
            LoseText.text = "Missed the target! Try again.";
            LoseText.enabled = true;
        }

        private void StopAll() 
        {
            Satellite.GetComponent<Launch>().StopLaunch();
            PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
            foreach (PathFollower p in pathFollowers)
            {
                p.StopPosition();
                p.StopOrbitter();
            }
            SatellitePath.Active = false;
        }

        private void ResetPlaceholderText()
        {
            if (WinText)
            {
                WinText.enabled = false;
            }
            if (LoseText)
            {
                LoseText.enabled = false;
            }
        }

        // Temporary methods for placeholder win/lose text
        private void EnablePlaceholderWinText()
        {
            if (WinText)
            {
                WinText.enabled = true;
            }
        }

        private void EnablePlaceholderLoseText()
        {
            if (LoseText)
            {
                LoseText.enabled = true;
            }
        }

        private IEnumerator DelayedSceneAdvance()
        {
            _sceneAdvanceStart = true;
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}