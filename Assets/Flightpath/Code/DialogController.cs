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
        private Button _launchButton;
        public Button LaunchButton { get; set; }
        [SerializeField]
        private Button _resetButton;
        public Button ResetButton { get; set; }
        [SerializeField]
        private Slider _powerSlider;
        public Slider PowerSlider { get; set; }
        [SerializeField]
        private GameObject _powerHandle;
        public GameObject PowerHandle { get; set; }
        [SerializeField]
        private GameObject _powerTargetArrow;
        public GameObject PowerTargetArrow { get; set; }
        [SerializeField]
        private GameObject _powerTargetArrow2;
        public GameObject PowerTargetArrow2 { get; set; }
        [SerializeField]
        private Slider _angleSlider;
        public Slider AngleSlider { get; set; }
        [SerializeField]
        private GameObject _angleHandle;
        public GameObject AngleHandle { get; set; }
        [SerializeField]
        private GameObject _angleTargetArrow;
        public GameObject AngleTargetArrow { get; set; }
        [SerializeField]
        private GameObject _angleTargetArrow2;
        public GameObject AngleTargetArrow2 { get; set; }
        [SerializeField]
        private LaunchManager _launchManager;
        public LaunchManager LaunchManager { get; set; }

        [SerializeField]
        private GameObject _dialogGeneratorPrefab;
        public GameObject DialogGeneratorPrefab { get; set; }
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;
        public DialogGenerator Dg { get; set; }
        [SerializeField]
        private DialogScriptableObject[] _scripts;
        public DialogScriptableObject[] Scripts { get; set; }
        private int _scriptIndex;
        private int _scriptMax;
        private int _phase;
        public int Phase { get; set; }
        private float _timeDelta;
        private float _timeMax;
        [SerializeField]
        private Color _highlightColor;
        public Color HighlightColor { get; set; }
        [SerializeField]
        private Color _defaultColor;
        public Color DefaultColor { get; set; }
        [SerializeField]
        private float _lerpRatio;
        public Color LerpRatio { get; set; }

        // Start is called before the first frame update
        public void Start()
        {
            _launchButton.interactable = false;
            _resetButton.interactable = false;
            _powerSlider.interactable = false;
            _angleSlider.interactable = false;
            _angleTargetArrow.GetComponent<Image>().enabled = false;
            _angleTargetArrow2.GetComponent<Image>().enabled = false;
            _powerTargetArrow.GetComponent<Image>().enabled = false;
            _powerTargetArrow2.GetComponent<Image>().enabled = false;

            _launchButton.onClick.AddListener(() => _dg.FastForwardDialog());
            _resetButton.onClick.AddListener(() => _dg.FastForwardDialog());

            _scriptIndex = 0;
            _scriptMax = _scripts.Length;
            _phase = -1;
            _timeMax = 2;
            SetDG();
        }

        public void DoIntermediatePhase()
        {
            _dg.BeginPlayingDialog();
            _phase++;
        }

        private void DoAnglePhase()
        {
            if (_angleSlider.interactable == false && _dg.GetCurrentDialogPosition() == 2)
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
                _dg.FastForwardDialog();
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
                _dg.FastForwardDialog();
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
                _dg.FastForwardDialog();
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
            if (_resetButton.interactable == false && _dg.GetCurrentDialogPosition() == 2)
            {
                _resetButton.interactable = true;
            }
            if (_resetButton.interactable == true)
            {
                _resetButton.GetComponent<Image>().color = Color.Lerp(_defaultColor, _highlightColor, Mathf.PingPong(Time.time, _lerpRatio));
            }
            if (!_launchManager.hasStopped())
            {
                _dg.FastForwardDialog();
                _resetButton.GetComponent<Image>().color = _defaultColor;
                _launchButton.interactable = true;
                _angleSlider.interactable = true;
                _powerSlider.interactable = true;
                _launchManager.enableMarsDialog();
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
                _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
                _dg = _dialogGenerator.GetComponent<DialogGenerator>();
                _dg.dialogContainer = _scripts[_scriptIndex++];
            }
        }
    }
}