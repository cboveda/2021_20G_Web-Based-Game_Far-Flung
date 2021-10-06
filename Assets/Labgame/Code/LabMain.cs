using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject main = GameObject.Find("LabGameStart");

        

        RadioPuzzleParams puzzle1params = new RadioPuzzleParams();
        puzzle1params.Amplitude = 1;
        puzzle1params.Frequency = 1;

        RadioPuzzle puzzle1 = main.AddComponent<RadioPuzzle>();
        puzzle1.InitializeRadioPuzzle("Puzzle 1", puzzle1params);

        //GameObject grapher = GameObject.Find("RadioQuestion");
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
