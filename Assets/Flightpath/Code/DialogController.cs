using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogMaker;

namespace Flightpath {
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
        private GameObject DialogGeneratorPrefab;
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;

        
        [SerializeField]
        private DialogScriptableObject[] Scripts;
        private int _scriptIndex;
        private int _scriptMax;
        private int _phase;
        
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
            SetDG();
            _dg.BeginPlayingDialog();
        }

        // Update is called once per frame
        void Update()
        {
            if (_phase == -1) 
            {
                _dg.BeginPlayingDialog();
                _phase++;
            }
            if (_phase == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _dg.BeginPlayingDialog();
                }
                _angleSlider.interactable = true;
                float value = _angleSlider.GetComponent<Slider>().value;
                if (40 <= value && value <= 42)
                {
                    _angleSlider.interactable = false;
                    _phase++;
                    SetDG();
                    _dg.BeginPlayingDialog();
                }
            } 
            else if (_phase == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _dg.BeginPlayingDialog();
                }
                _powerSlider.interactable = true;
                float value = _powerSlider.GetComponent<Slider>().value;
                if (84 <= value && value <= 88)
                {
                    _powerSlider.interactable = false;
                    _phase++;
                    SetDG();
                    _dg.BeginPlayingDialog();
                }
            }
            // else if (_scriptIndex == 1) 
            // {
            //     if (Input.GetMouseButtonDown(0))
            //     {
            //         _dg.BeginPlayingDialog();
            //     }
            //     _powerSlider.interactable = true;
            //     float value = _powerSlider.GetComponent<Slider>().value;
            //     if (84 <= value && value <= 88)
            //     {
            //         SetDG();
            //         _dg.BeginPlayingDialog();
            //     }
            // }
        }

        void SetDG()
        {
            if (_scriptIndex < _scriptMax) 
            {
                _dialogGenerator = Object.Instantiate(DialogGeneratorPrefab, this.transform);
                _dg = _dialogGenerator.GetComponent<DialogGenerator>();
                _dg.dialogContainer = Scripts[_scriptIndex++];
                //_dg.BeginPlayingDialog();
            }
        }
    }

}