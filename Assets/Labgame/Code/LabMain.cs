using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RadioPuzzleParams puzzleParams = new RadioPuzzleParams();
        puzzleParams.Amplitude = 1;
        puzzleParams.Frequency = 1;
        RadioPuzzle puzzle = new RadioPuzzle("Puzzle 1", puzzleParams);

        GameObject grapher = GameObject.Find("Grapher");
        GraphViewer viewer = grapher.AddComponent<GraphViewer>();
        SineWave wave = grapher.AddComponent<SineWave>();

        wave.SetParameters(puzzleParams);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
