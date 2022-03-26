using System;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public Text mainText;
    public TextPanelButton textPanelButton;

    public void ShowText(TextAsset textAsset, Action doAfterClose) {

        if (textAsset != null) {
            mainText.text = textAsset.text;

        } else {
            mainText.text = "Error Text not found";
        }

        this.gameObject.SetActive(true);

        StartCoroutine(textPanelButton.CloseOnClick(doAfterClose));
    }
}
