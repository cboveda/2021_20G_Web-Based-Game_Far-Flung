using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DialogMaker;

namespace Flightpath
{
    public class LaunchManager : MonoBehaviour
    {
        public const int TopBoundaryScriptIndex = 0;
        public const int BottomBoundaryScriptIndex = 1;
        public const int LeftBoundaryScriptIndex = 2;
        public const int RightBoundaryScriptIndex = 3;
        public const int MarsBoundaryScriptIndex = 4;
        public const int WinBoundaryScriptIndex = 5;
        
        public GameObject Satellite;
        public Slider AngleSlider;
        public Slider PowerSlider;
        public Trajectory TrajectoryArrow;
        public SatellitePathDrawing SatellitePath;

        [SerializeField]
        private GameObject _dialogGeneratorPrefab;
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;
        [SerializeField]
        private DialogScriptableObject[] Scripts;
        private bool _sceneAdvanceStart;
        private bool _launchLocked;
        private bool _stopped;


        public void Start()
        {
            Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
            Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);
            TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
            TrajectoryArrow.SetPowerRange(PowerSlider.GetComponent<Slider>().minValue, PowerSlider.GetComponent<Slider>().maxValue);
            _sceneAdvanceStart = false;
            _launchLocked = false;
            SatellitePath.Active = false;
        }

        public void Update() 
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_dg != null)
                {
                    _dg.BeginPlayingDialog();
                }
            }
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
            SatellitePath.Active = false;
            var particles = Satellite.GetComponent<ParticleSystem>();
            if (particles != null) {
                particles.Stop();
                particles.Clear();
            }
            _launchLocked = false;
            _stopped = false;
        }

        public void OnAsteroidCollisionDetected()
        {
            StopAll();
            _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
            _dg = _dialogGenerator.GetComponent<DialogGenerator>();
            _dg.dialogContainer = Scripts[WinBoundaryScriptIndex];
            StartCoroutine("DelayedDialogStart");
            if (!_sceneAdvanceStart)
            {
                StartCoroutine("DelayedSceneAdvance");
            }
        }

        public void OnMarsCollisionDetected() 
        {
            StopAll();
            _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
            _dg = _dialogGenerator.GetComponent<DialogGenerator>();
            _dg.dialogContainer = Scripts[MarsBoundaryScriptIndex];
            StartCoroutine("DelayedDialogStart");

            Satellite.GetComponent<ParticleSystem>().Play();
        }

        public void OnSatelliteLeaveWindow(int scriptIndex)
        {
            StopAll();
            _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
            _dg = _dialogGenerator.GetComponent<DialogGenerator>();
            _dg.dialogContainer = Scripts[scriptIndex];
            StartCoroutine("DelayedDialogStart");
        }

        private IEnumerator DelayedDialogStart()
        {
            yield return new WaitForFixedUpdate();
            _dg.BeginPlayingDialog();
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
            _stopped = true;
        }

        private IEnumerator DelayedSceneAdvance()
        {
            _sceneAdvanceStart = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public bool hasStopped()
        {
            return _stopped;
        }

    }
}