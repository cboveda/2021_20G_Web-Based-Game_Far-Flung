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
        private MessageScriptableObject messageContainer;
        private int _index;
        void Start()
        {
            _text = GetComponent<Text>();
            _index = 0;
        }

        public void showNext() {
            if (_index < MessageCount())
            {
                _text.text = messageContainer.Messages[_index++];
            }
        }

        public int MessageCount() {
            return messageContainer.Messages.Length;
        }
    }
}