using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DialogMaker;

namespace Flightpath
{
    /*
        Event system component that handles the launch behavior of all game objects.
        Author: Chris Boveda
    */
    public class LaunchManager : MonoBehaviour
    {
        public const int NoScriptIndex = -1;
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
        public GameObject DialogGeneratorPrefab { get; set; }
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;
        [SerializeField]
        private DialogScriptableObject[] _topScripts;
        public DialogScriptableObject[] TopScripts { get; set; }
        [SerializeField]
        private DialogScriptableObject[] _botScripts;
        public DialogScriptableObject[] BotScripts { get; set; }
        [SerializeField]
        private DialogScriptableObject[] _rightScripts;
        public DialogScriptableObject[] RightScripts { get; set; }
        [SerializeField]
        private DialogScriptableObject[] _leftScripts;
        public DialogScriptableObject[] LeftScripts { get; set; }
        [SerializeField]
        private DialogScriptableObject[] _marsScripts;
        public DialogScriptableObject[] MarsScripts { get; set; }
        private int _lastScript;
        private bool _sceneAdvanceStart;
        private bool _launchLocked;
        private bool _stopped;
        private bool _marsDialogEnabled;


        public void Start()
        {
            Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
            Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);
            TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
            TrajectoryArrow.SetPowerRange(PowerSlider.GetComponent<Slider>().minValue, PowerSlider.GetComponent<Slider>().maxValue);
            _sceneAdvanceStart = false;
            _launchLocked = false;
            _marsDialogEnabled = false;
            _lastScript = -1;
            SatellitePath.Active = false;
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
                if (_dg)
                {
                    _dg.FastForwardDialog();
                }
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
            if (particles != null)
            {
                particles.Stop();
                particles.Clear();
            }
            _launchLocked = false;
            _stopped = false;
            if (_dg)
            {
                _dg.FastForwardDialog();
            }
        }

        public void createDialogGenerator(int scriptIndex)
        {
            if (_dialogGeneratorPrefab == null)
            {
                return;
            }
            _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
            _dg = _dialogGenerator.GetComponent<DialogGenerator>();
            switch (scriptIndex)
            {
                case TopBoundaryScriptIndex:
                    _dg.dialogContainer = pickNewScript(_topScripts);
                    break;
                case BottomBoundaryScriptIndex:
                    _dg.dialogContainer = pickNewScript(_botScripts);
                    break;
                case RightBoundaryScriptIndex:
                    _dg.dialogContainer = pickNewScript(_rightScripts);
                    break;
                case LeftBoundaryScriptIndex:
                    _dg.dialogContainer = pickNewScript(_leftScripts);
                    break;
                case MarsBoundaryScriptIndex:
                    _dg.dialogContainer = pickNewScript(_marsScripts);
                    break;
                default:
                    break;
            }
        }

        private DialogScriptableObject pickNewScript(DialogScriptableObject[] scriptCollection)
        {
            int newScript;
            do
            {
                newScript = Random.Range(0, scriptCollection.Length);
            } while (newScript == _lastScript);
            _lastScript = newScript;
            return scriptCollection[newScript];
        }

        public void OnAsteroidCollisionDetected()
        {
            StopAll();
            createDialogGenerator(WinBoundaryScriptIndex);
            StartCoroutine("DelayedDialogStart");
            if (!_sceneAdvanceStart)
            {
                StartCoroutine("DelayedSceneAdvance");
            }
        }

        public void OnMarsCollisionDetected()
        {
            StopAll();
            if (Satellite.GetComponent<ParticleSystem>() != null)
            {
                Satellite.GetComponent<ParticleSystem>().Play();
            }
            if (_marsDialogEnabled)
            {
                createDialogGenerator(MarsBoundaryScriptIndex);
                StartCoroutine("DelayedDialogStartWithTime", 1.0);
            }
        }

        public void OnSatelliteLeaveWindow(int scriptIndex)
        {
            Debug.Log("Satellite outside bounds");
            StopAll();
            createDialogGenerator(scriptIndex);
            StartCoroutine("DelayedDialogStart");
        }

        private IEnumerator DelayedDialogStart()
        {
            yield return new WaitForFixedUpdate();
            if (_dg != null)
            {
                _dg.BeginPlayingDialog();
            }
        }

        private IEnumerator DelayedDialogStartWithTime(float t)
        {
            yield return new WaitForSeconds(t);
            if (_dg != null)
            {
                _dg.BeginPlayingDialog();
            }
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
            Debug.Log("Stopping all moveables");
            _stopped = true;
        }

        private IEnumerator DelayedSceneAdvance()
        {
            _sceneAdvanceStart = true;
            yield return new WaitForSeconds(2f);
            if (SceneManager.GetActiveScene().name == "2_Flightpath")
            {
                SceneManager.LoadScene("3_FlightpathOutro");
            }
        }

        public bool hasStopped()
        {
            return _stopped;
        }

        public void enableMarsDialog()
        {
            _marsDialogEnabled = true;
        }
    }
}