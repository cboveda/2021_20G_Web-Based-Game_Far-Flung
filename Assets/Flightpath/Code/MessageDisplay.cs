using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flightpath
{
    public class MessageDisplay : MonoBehaviour
    {
        private Text _text;
        [SerializeField]
        private MessageScriptableObject _messageContainer;
        private int _index;
        void Start()
        {
            _text = GetComponent<Text>();
            _index = 0;
            showNext();
        }

        public void showNext() 
        {
            if (_index < MessageCount() && _messageContainer != null && _text != null)
            {
                Debug.Log(_text);
                Debug.Log(_messageContainer);
                Debug.Log(_messageContainer.Messages);
                Debug.Log(_messageContainer.Messages[0]);
                _text.text = _messageContainer.Messages[_index++];
            }
        }

        public void hide()
        {
            if (_text != null)
            {
                _text.enabled = false;
            }
        }

        public int MessageCount() {
            if (_messageContainer != null)
            {
                return _messageContainer.Messages.Length;
            }
            return 0;
        }
    }
}