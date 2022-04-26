using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogMaker;

namespace Flightpath
{
    /*
        Runs through a set script of tutorial dialog with the help of the DialogMaker module.
        Author: Chris Boveda
    */
    public class DialogController : MonoBehaviour
    {
        [SerializeField]
        public Button launchButton;
        [SerializeField]
        public Button resetButton;
        [SerializeField]
        public Slider powerSlider;
        [SerializeField]
        public GameObject powerHandle;
        [SerializeField]
        public GameObject powerTargetArrow;
        [SerializeField]
        public GameObject powerTargetArrow2;
        [SerializeField]
        public Slider angleSlider;
        [SerializeField]
        public GameObject angleHandle;
        [SerializeField]
        public GameObject angleTargetArrow;
        [SerializeField]
        public GameObject angleTargetArrow2;
        [SerializeField]
        public LaunchManager launchManager;

        [SerializeField]
        public GameObject dialogGeneratorPrefab;
        public GameObject dialogGenerator;
        public DialogGenerator dg;
        [SerializeField]
        public DialogScriptableObject[] Scripts;
        private int _scriptIndex;
        private int _scriptMax;
        private int _phase;
        private float _timeDelta;
        private float _timeMax;
        [SerializeField]
        private Color _highlightColor;
        [SerializeField]
        private Color _defaultColor;
        [SerializeField]
        private float _lerpRatio;

        // Start is called before the first frame update
        void Start()
        {
            launchButton.interactable = false;
            resetButton.interactable = false;
            powerSlider.interactable = false;
            angleSlider.interactable = false;
            angleTargetArrow.GetComponent<Image>().enabled = false;
            angleTargetArrow2.GetComponent<Image>().enabled = false;
            powerTargetArrow.GetComponent<Image>().enabled = false;
            powerTargetArrow2.GetComponent<Image>().enabled = false;

            launchButton.onClick.AddListener(() => dg.FastForwardDialog());
            resetButton.onClick.AddListener(() => dg.FastForwardDialog());

            _scriptIndex = 0;
            _scriptMax = Scripts.Length;
            _phase = -1;
            _timeMax = 2;
            SetDG();
        }

        private void DoIntermediatePhase()
        {
            dg.BeginPlayingDialog();
            _phase++;
        }

        private void DoAnglePhase()
        {
            if (angleSlider.interactable == false && dg.GetCurrentDialogPosition() == 2)
            {
                angleSlider.interactable = true;
                angleTargetArrow.GetComponent<Image>().enabled = true;
                angleTargetArrow2.GetComponent<Image>().enabled = true;
            }
            angleTargetArrow.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            angleTargetArrow2.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            angleHandle.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            float value = angleSlider.GetComponent<Slider>().value;
            if (42 <= value && value <= 43)
            {
                angleSlider.interactable = false;
                angleTargetArrow.GetComponent<Image>().enabled = false;
                angleTargetArrow2.GetComponent<Image>().enabled = false;
                angleHandle.GetComponent<Image>().color = _defaultColor;
                _phase++;
                dg.FastForwardDialog();
                SetDG();
            }
        }

        private void DoPowerPhase()
        {
            if (powerSlider.interactable == false)
            {
                powerSlider.interactable = true;
                powerTargetArrow.GetComponent<Image>().enabled = true;
                powerTargetArrow2.GetComponent<Image>().enabled = true;
            }
            powerHandle.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            powerTargetArrow.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            powerTargetArrow2.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            float value = powerSlider.GetComponent<Slider>().value;
            if (84 <= value && value <= 86)
            {
                powerSlider.interactable = false;
                powerTargetArrow.GetComponent<Image>().enabled = false;
                powerTargetArrow2.GetComponent<Image>().enabled = false;
                powerHandle.GetComponent<Image>().color = _defaultColor;
                _phase++;
                dg.FastForwardDialog();
                SetDG();
            }
        }

        private void DoLaunchPhase()
        {
            if (launchButton.interactable == false)
            {
                launchButton.interactable = true;
            }
            launchButton.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            if (launchManager.hasStopped())
            {
                launchButton.interactable = false;
                launchButton.GetComponent<Image>().color = _defaultColor;
                _phase++;
                dg.FastForwardDialog();
                SetDG();
            }
        }

        private void DoCrashPhase()
        {
            if (_timeDelta < _timeMax)
            {
                _timeDelta += Time.deltaTime;
            }
            else
            {
                DoIntermediatePhase();
            }
        }

        public void DoResetPhase()
        {
            if (resetButton.interactable == false && dg.GetCurrentDialogPosition() == 2)
            {
                resetButton.interactable = true;
            }
            if (resetButton.interactable == true)
            {
                resetButton.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            }
            if (!launchManager.hasStopped())
            {
                dg.FastForwardDialog();
                resetButton.GetComponent<Image>().color = _defaultColor;
                launchButton.interactable = true;
                angleSlider.interactable = true;
                powerSlider.interactable = true;
                launchManager.enableMarsDialog();
            }
        }

        public void UnlockAllPhase()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch (_phase)
            {
                case -1:
                    DoIntermediatePhase();
                    break;
                case 0:
                    DoAnglePhase();
                    break;
                case 1:
                    DoIntermediatePhase();
                    break;
                case 2:
                    DoPowerPhase();
                    break;
                case 3:
                    DoIntermediatePhase();
                    break;
                case 4:
                    DoLaunchPhase();
                    break;
                case 5:
                    DoCrashPhase();
                    break;
                case 6:
                    DoResetPhase();
                    break;
                default:
                    Debug.Log("Invalid Tutorial Phase");
                    break;
            }
        }

        private void SetDG()
        {
            if (_scriptIndex < _scriptMax)
            {
                dialogGenerator = Object.Instantiate(dialogGeneratorPrefab, this.transform);
                dg = dialogGenerator.GetComponent<DialogGenerator>();
                dg.dialogContainer = Scripts[_scriptIndex++];
            }
        }
    }
}