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
        [SerializeField] private Button _launchButton;
        public Button LaunchButton { get; set; }
        [SerializeField] private Button _resetButton;
        public Button ResetButton { get; set; }
        [SerializeField] private Slider _powerSlider;
        public Slider PowerSlider { get; set; }
        [SerializeField] private GameObject _powerHandle;
        public GameObject PowerHandle { get; set; }
        [SerializeField] private GameObject _powerTargetArrow;
        public GameObject PowerTargetArrow { get; set; }
        [SerializeField] private GameObject _powerTargetArrow2;
        public GameObject PowerTargetArrow2 { get; set; }
        [SerializeField] private Slider _angleSlider;
        public Slider AngleSlider { get; set; }
        [SerializeField] private GameObject _angleHandle;
        public GameObject AngleHandle { get; set; }
        [SerializeField] private GameObject _angleTargetArrow;
        public GameObject AngleTargetArrow { get; set; }
        [SerializeField] private GameObject _angleTargetArrow2;
        public GameObject AngleTargetArrow2 { get; set; }
        [SerializeField] private LaunchManager _launchManager;
        public LaunchManager LaunchManager { get; set; }
        [SerializeField] private GameObject _dialogGeneratorPrefab;
        public GameObject DialogGeneratorPrefab { get; set; }
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;
        public DialogGenerator Dg { get; set; }
        [SerializeField] private DialogScriptableObject[] _scripts;
        public DialogScriptableObject[] Scripts { get; set; }
        private int _scriptIndex;
        private int _scriptMax;
        private int _phase;
        public int Phase { get; set; }
        private float _timeDelta;
        private float _timeMax;
        [SerializeField] private Color _highlightColor;
        public Color HighlightColor { get; set; }
        [SerializeField] private Color _defaultColor;
        public Color DefaultColor { get; set; }
        [SerializeField] private float _lerpRatio;
        public float LerpRatio { get; set; }

        // Start is called before the first frame update
        public void Start()
        {

            LaunchButton.interactable = false;
            ResetButton.interactable = false;
            PowerSlider.interactable = false;
            AngleSlider.interactable = false;
            AngleTargetArrow.GetComponent<Image>().enabled = false;
            AngleTargetArrow2.GetComponent<Image>().enabled = false;
            PowerTargetArrow.GetComponent<Image>().enabled = false;
            PowerTargetArrow2.GetComponent<Image>().enabled = false;

            LaunchButton.onClick.AddListener(() => Dg.FastForwardDialog());
            ResetButton.onClick.AddListener(() => Dg.FastForwardDialog());

            _scriptIndex = 0;
            _scriptMax = Scripts.Length;
            _phase = -1;
            _timeMax = 2;
            SetDG();
        }

        public void DoIntermediatePhase()
        {
            Dg.BeginPlayingDialog();
            _phase++;
        }

        private void DoAnglePhase()
        {
            if (AngleSlider.interactable == false && Dg.GetCurrentDialogPosition() == 2)
            {
                AngleSlider.interactable = true;
                AngleTargetArrow.GetComponent<Image>().enabled = true;
                AngleTargetArrow2.GetComponent<Image>().enabled = true;
            }
            AngleTargetArrow.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            AngleTargetArrow2.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            AngleHandle.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            float value = AngleSlider.GetComponent<Slider>().value;
            if (42 <= value && value <= 43)
            {
                AngleSlider.interactable = false;
                AngleTargetArrow.GetComponent<Image>().enabled = false;
                AngleTargetArrow2.GetComponent<Image>().enabled = false;
                AngleHandle.GetComponent<Image>().color = DefaultColor;
                _phase++;
                Dg.FastForwardDialog();
                SetDG();
            }
        }

        private void DoPowerPhase()
        {
            if (PowerSlider.interactable == false)
            {
                PowerSlider.interactable = true;
                PowerTargetArrow.GetComponent<Image>().enabled = true;
                PowerTargetArrow2.GetComponent<Image>().enabled = true;
            }
            PowerHandle.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            PowerTargetArrow.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            PowerTargetArrow2.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            float value = PowerSlider.GetComponent<Slider>().value;
            if (84 <= value && value <= 86)
            {
                PowerSlider.interactable = false;
                PowerTargetArrow.GetComponent<Image>().enabled = false;
                PowerTargetArrow2.GetComponent<Image>().enabled = false;
                PowerHandle.GetComponent<Image>().color = DefaultColor;
                _phase++;
                Dg.FastForwardDialog();
                SetDG();
            }
        }

        private void DoLaunchPhase()
        {
            if (LaunchButton.interactable == false)
            {
                LaunchButton.interactable = true;
            }
            LaunchButton.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            if (LaunchManager.hasStopped())
            {
                LaunchButton.interactable = false;
                LaunchButton.GetComponent<Image>().color = DefaultColor;
                _phase++;
                Dg.FastForwardDialog();
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
            if (ResetButton.interactable == false && Dg.GetCurrentDialogPosition() == 2)
            {
                ResetButton.interactable = true;
            }
            if (ResetButton.interactable == true)
            {
                ResetButton.GetComponent<Image>().color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(Time.time, LerpRatio));
            }
            if (!LaunchManager.hasStopped())
            {
                Dg.FastForwardDialog();
                ResetButton.GetComponent<Image>().color = DefaultColor;
                LaunchButton.interactable = true;
                AngleSlider.interactable = true;
                PowerSlider.interactable = true;
                LaunchManager.enableMarsDialog();
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
                _dialogGenerator = Object.Instantiate(DialogGeneratorPrefab, this.transform);
                Dg = _dialogGenerator.GetComponent<DialogGenerator>();
                Dg.dialogContainer = Scripts[_scriptIndex++];
            }
        }
    }
}