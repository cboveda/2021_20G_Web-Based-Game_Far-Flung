using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogMaker;

namespace Flightpath
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField]
        private Button _launchButton;
        [SerializeField]
        private Button _resetButton;
        [SerializeField]
        private Slider _powerSlider;
        [SerializeField]
        private GameObject _powerHandle;
        [SerializeField]
        private GameObject _powerTargetArrow;
        [SerializeField]
        private GameObject _powerTargetArrow2;
        [SerializeField]
        private Slider _angleSlider;
        [SerializeField]
        private GameObject _angleHandle;
        [SerializeField]
        private GameObject _angleTargetArrow;
        [SerializeField]
        private GameObject _angleTargetArrow2;
        [SerializeField]
        private LaunchManager _launchManager;

        [SerializeField]
        private GameObject _dialogGeneratorPrefab;
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;
        [SerializeField]
        private DialogScriptableObject[] Scripts;
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
            _launchButton.interactable = false;
            _resetButton.interactable = false;
            _powerSlider.interactable = false;
            _angleSlider.interactable = false;
            _angleTargetArrow.GetComponent<Image>().enabled = false;
            _angleTargetArrow2.GetComponent<Image>().enabled = false;
            _powerTargetArrow.GetComponent<Image>().enabled = false;
            _powerTargetArrow2.GetComponent<Image>().enabled = false;

            _scriptIndex = 0;
            _scriptMax = Scripts.Length;
            _phase = -1;
            _timeMax = 2;
            SetDG();
        }

        private void DoIntermediatePhase()
        {
            _dg.BeginPlayingDialog();
            _phase++;
        }

        private void DoAnglePhase()
        {
            if (_angleSlider.interactable == false)
            {
                _angleSlider.interactable = true;
                _angleTargetArrow.GetComponent<Image>().enabled = true;
                _angleTargetArrow2.GetComponent<Image>().enabled = true;
            }
            _angleTargetArrow.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            _angleTargetArrow2.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            _angleHandle.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            float value = _angleSlider.GetComponent<Slider>().value;
            if (42 <= value && value <= 43)
            {
                _angleSlider.interactable = false;
                _angleTargetArrow.GetComponent<Image>().enabled = false;
                _angleTargetArrow2.GetComponent<Image>().enabled = false;
                _angleHandle.GetComponent<Image>().color = _defaultColor;
                _phase++;
                SetDG();
            }
        }

        private void DoPowerPhase()
        {
            if (_powerSlider.interactable == false)
            {
                _powerSlider.interactable = true;
                _powerTargetArrow.GetComponent<Image>().enabled = true;
                _powerTargetArrow2.GetComponent<Image>().enabled = true;
            }
            _powerHandle.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            _powerTargetArrow.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            _powerTargetArrow2.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            float value = _powerSlider.GetComponent<Slider>().value;
            if (84 <= value && value <= 86)
            {
                _powerSlider.interactable = false;
                _powerTargetArrow.GetComponent<Image>().enabled = false;
                _powerTargetArrow2.GetComponent<Image>().enabled = false;
                _powerHandle.GetComponent<Image>().color = _defaultColor;
                _phase++;
                SetDG();
            }
        }

        private void DoLaunchPhase()
        {
            if (_launchButton.interactable == false)
            {
                _launchButton.interactable = true;
            }
            _launchButton.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            if (_launchManager.hasStopped())
            {
                _launchButton.interactable = false;
                _launchButton.GetComponent<Image>().color = _defaultColor;
                _phase++;
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
            if (_resetButton.interactable == false)
            {
                _resetButton.interactable = true;
            }
            _resetButton.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            if (!_launchManager.hasStopped())
            {
                _resetButton.GetComponent<Image>().color = _defaultColor;
                _launchButton.interactable = true;
                _angleSlider.interactable = true;
                _powerSlider.interactable = true;
                _launchManager.enableMarsDialog();
            }
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
                _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
                _dg = _dialogGenerator.GetComponent<DialogGenerator>();
                _dg.dialogContainer = Scripts[_scriptIndex++];
            }
        }
    }
}