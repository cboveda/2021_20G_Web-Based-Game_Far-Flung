using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        if (uiTextObject != null && textToType != null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                uiTextObject.text = textToType.Substring(0, stringIndex);
                stringIndex++;
                timer = typingSpeed;

                if(stringIndex > textToType.Length)
                {
                    textToType = null;
                    currentlyTyping = false;
                    return;
                }
            }
        }
    }
}
