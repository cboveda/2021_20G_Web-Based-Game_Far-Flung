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
        private int index;
        private float timer;
        void Start()
        {
            _text = GetComponent<Text>();
            index = 0;
            timer = 0;
            _text.text = messageContainer.Messages[index];
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                index++;
                if (index >= messageContainer.Messages.Length)
                    index = 0;
                _text.text = messageContainer.Messages[index];
            }
        }
    }
}