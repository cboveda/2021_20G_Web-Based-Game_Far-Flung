using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellTyper : MonoBehaviour
{
    private float timer;
    private Text uiTextObject;
    private string textToType;
    private float typingSpeed;
    private int stringIndex;

    public void AddTyper(Text uiTextObject, string textToType, float typingSpeed)
    {
        this.uiTextObject = uiTextObject;
        this.textToType = textToType;
        this.typingSpeed = typingSpeed;
        timer = typingSpeed;
        stringIndex = 0;
    }

    private void Update()
    {
        if(uiTextObject != null && textToType != null && textToType.Length >= stringIndex)
        {
            timer -= Time.deltaTime;

            if(timer <= 0f)
            {
                uiTextObject.text = textToType.Substring(0, stringIndex);
                stringIndex++;
                timer = typingSpeed;
            }
        }
    }
}
