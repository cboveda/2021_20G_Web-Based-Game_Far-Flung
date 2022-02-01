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
        private Slider _angleSlider;
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

        // Start is called before the first frame update
        void Start()
        {
            _launchButton.interactable = false;
            _resetButton.interactable = false;
            _powerSlider.interactable = false;
            _angleSlider.interactable = false;

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
            if (Input.GetMouseButtonDown(0))
            {
                _dg.BeginPlayingDialog();
            }
            _angleSlider.interactable = true;
            float value = _angleSlider.GetComponent<Slider>().value;
            if (42 <= value && value <= 43)
            {
                _angleSlider.interactable = false;
                _phase++;
                SetDG();
            }
        }

        private void DoPowerPhase()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dg.BeginPlayingDialog();
            }
            _powerSlider.interactable = true;
            float value = _powerSlider.GetComponent<Slider>().value;
            if (84 <= value && value <= 86)
            {
                _powerSlider.interactable = false;
                _phase++;
                SetDG();
            }
        }

        private void DoLaunchPhase()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dg.BeginPlayingDialog();
            }
            _launchButton.interactable = true;
            if (_launchManager.hasStopped())
            {
                _launchButton.interactable = false;
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
            if (Input.GetMouseButtonDown(0))
            {
                _dg.BeginPlayingDialog();
            }
            _resetButton.interactable = true;
            if (!_launchManager.hasStopped())
            {
                _launchButton.interactable = true;
                _angleSlider.interactable = true;
                _powerSlider.interactable = true;
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

        void SetDG()
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