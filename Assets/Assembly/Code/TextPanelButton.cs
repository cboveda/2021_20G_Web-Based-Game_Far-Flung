using System;
using System.Collections;
using UnityEngine;

public class TextPanelButton : MonoBehaviour
{
    private bool clicked;

    public IEnumerator CloseOnClick(Action doAfterClick) {

        yield return new WaitUntil(() => clicked);

        if (!clicked) {
            yield return new WaitForEndOfFrame();
        }

        clicked = false;
        Debug.Log("Button Clicked");
        Resources.FindObjectsOfTypeAll<TextPanel>()[0].gameObject.SetActive(false);
        doAfterClick();
    }

    public void Clicked() {
        clicked = true;
    }
}
