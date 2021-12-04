using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogMaker
{
    public class DialogTyper : MonoBehaviour
    {
        private float timer;
        private Text uiTextObject;
        private string textToType;
        private float typingSpeed;
        private int stringIndex;
        public bool currentlyTyping = false;


        public void AddTyper(Text uiTextObject, string textToType, float typingSpeed)
        {
            this.uiTextObject = uiTextObject;
            this.textToType = textToType;
            this.typingSpeed = typingSpeed;
            timer = typingSpeed;
            stringIndex = 0;
            currentlyTyping = true;
        }

        public void FinishTypingFaster()
        {
            typingSpeed = 0.001f;
            timer = typingSpeed;
        }

        private void Update()
        {
            if (uiTextObject != null && textToType != null)
            {
                timer -= Time.deltaTime;

                while (timer <= 0f)
                {
                    string tempText = textToType.Substring(0, stringIndex);
                    tempText += "<color=#00000000>" + textToType.Substring(stringIndex) + "</color>";
                    uiTextObject.text = tempText;
                    stringIndex++;
                    timer = typingSpeed;

                    if (stringIndex > textToType.Length)
                    {
                        textToType = null;
                        currentlyTyping = false;
                        return;
                    }
                }
            }
        }
    }
}

