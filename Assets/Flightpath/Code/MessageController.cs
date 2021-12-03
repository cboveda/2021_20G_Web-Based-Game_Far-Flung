using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flightpath {
    public class MessageController : MonoBehaviour
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
        private MessageDisplay _messageDisplay;
        private int _stage;
        private int _maxStages;
        
        // Start is called before the first frame update
        void Start()
        {
            _stage = 0;
            _maxStages = _messageDisplay.MessageCount();
            _launchButton.interactable = false;
            _resetButton.interactable = false;
            _powerSlider.interactable = false;
            _angleSlider.interactable = false;
            _messageDisplay.showNext();
        }

        // Update is called once per frame
        void Update()
        {
            if (_stage < _maxStages)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _messageDisplay.showNext();
                    if (_stage == 0) 
                    {
                        _angleSlider.interactable = true;
                    }
                    else if (_stage == 1) 
                    {
                        _powerSlider.interactable = true;
                    }
                    else if (_stage == 2) 
                    {
                        _launchButton.interactable = true;
                    }
                    else if (_stage == 3) 
                    {
                        _resetButton.interactable = true;
                    } else {
                        _messageDisplay.hide();
                    }
                    _stage++;
                }
            }
        }
    }
}