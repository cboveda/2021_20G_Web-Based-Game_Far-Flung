using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;



public class ComGameMain : MonoBehaviour
{
    AudioSource audioSource;
    GameObject volumeSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        SetTileStartPositions();
        try
        {
            audioSource = GameObject.Find("MainMusic").GetComponent<AudioSource>();
        }
        catch(Exception e)
        {
            Debug.Log("Unable to find MainMusic, probably because we're testing. Setting to null.");
            audioSource = null;
        }
        
        //audioSource = GetComponent<AudioSource>();
        volumeSlider = GameObject.Find("VolumeSlider");        
        //audioSource.Play();

        // start score of 1000
        Scoring.Instance.initialize(1000, "ComObjective1");
        //Scoring.Instance.gameScoreDetails(1000, "Objective1");
    }


    void Update()
    {   
        if(audioSource != null)
        {
            audioSource.volume = volumeSlider.GetComponent<Slider>().value;
        }
        
    }


    public void SetTileStartPositions()
    {
        //Debug.Log("set start positions");

        float[] startPos = { 0.0F, 0.0F };
        int[] tilePositions = { 0, 11, 12, 13, 21, 22, 23, 24, 31, 32, 33, 34, };
        int x = 0;
        int y = 1;
        string tileNumber = "";
        GameObject tilePosObject;

        // set start positions
        foreach (int tilePosition in tilePositions)
        {
            startPos = FindObjectOfType<ComGameData>().getStartPosition(tilePosition);

            tileNumber = tilePosition.ToString();
            if (tileNumber == "0")
            {
                tileNumber = "blank";
            }
            tilePosObject = GameObject.Find(tileNumber);
            tilePosObject.transform.position = new Vector3(startPos[x], startPos[y]);

        }
    }
}