using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComUnscrambleMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideLetters();
    }


    public void HideLetters()
    {
        //Debug.Log("hide");
        Color hiddenColor = new Color32(246, 34, 250, 0);
        
        int backgroundChild = 0;
        int textChild = 0;
        int buttonLastIndex = 49;

        GameObject buttons = GameObject.Find("ButtonCanvas");

        for (int buttonIndex = 9; buttonIndex < buttonLastIndex; buttonIndex++)
        {
            buttons.transform.GetChild(buttonIndex).GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = hiddenColor;

        }
    }
}