using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComUnscrambleInfo : MonoBehaviour
{

    SpriteRenderer rend;    
    Color currentColor;
    string currentLayer = "";
    string spriteName = "";
    GameObject spriteObject;
    GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        currentColor = rend.color;
        currentLayer = rend.sortingLayerName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseEnter()
    {
        spriteName = rend.sprite.name;
        //Debug.Log(spriteName);
        HighlightPicture(spriteName);
        DisplayInfo(spriteName);

        

    }

    public void DisplayInfo(string spriteName)
    {
        Color hiddenColor = new Color32(255, 255, 255, 0);
        instructions = GameObject.Find("InstructionsText");
        instructions.GetComponent<UnityEngine.UI.Text>().enabled = false;

        spriteObject = GameObject.Find(spriteName + "info");
        spriteObject.GetComponent<SpriteRenderer>().sortingLayerName = "Board";

    }

    public void HighlightPicture(string spriteName)
    {
        Color highlightColor = new Color32(255, 255, 255, 255);
        spriteObject = GameObject.Find(spriteName);

        spriteObject.GetComponent<SpriteRenderer>().color = highlightColor;
    }

    public void OnMouseExit()
    {        
        rend.color = currentColor;
        spriteName = rend.sprite.name;
        HideInfo(spriteName);     

    }

    public void HideInfo(string spriteName)
    {
        spriteObject = GameObject.Find(spriteName + "info");
        spriteObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
        instructions = GameObject.Find("InstructionsText");
        instructions.GetComponent<UnityEngine.UI.Text>().enabled = true;

    }
}
