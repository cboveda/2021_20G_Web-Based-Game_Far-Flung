using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ComGameModes : MonoBehaviour
{

    Toggle toggle;
    public GameObject tileNumberObject;
    public SpriteRenderer tileNumberRend;


    

    public void EasyModeOn()
    {
        Debug.Log("easy mode");

        string[] tileNumbers = { "n1", "n2", "n3", "n4", "n5", "n6", "n7", "n8", "n9", "n10", "n11", "n12" };
        string sortingLayer = "";
        //string tileNumber = "";

        toggle = GetComponent<Toggle>();

        sortingLayer = "Default";        
        if (toggle.isOn)
        {
            sortingLayer = "Numbers";
        }

        foreach (string tileNumber in tileNumbers)
        {
            Debug.Log(tileNumber);
            tileNumberObject = GameObject.Find(tileNumber);
            tileNumberRend = tileNumberObject.GetComponent<SpriteRenderer>();
            tileNumberRend.sortingLayerName = sortingLayer;
        }       
    }
}
