using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public Text mainText;
    public TextPanelButton textPanelButton;

    public void ShowText(TextAsset textAsset, Action doAfterClose)
    {
        if (textAsset != null)
        {
            mainText.text = textAsset.text;
        }
        else
        {
            mainText.text = "Error Text not found";
        }
        this.gameObject.SetActive(true);
        StartCoroutine(textPanelButton.CloseOnClick(doAfterClose));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
