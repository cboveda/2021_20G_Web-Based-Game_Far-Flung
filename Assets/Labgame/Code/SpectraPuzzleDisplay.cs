using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectraPuzzleDisplay : MonoBehaviour
{
    public const int SOLUTION_DISPLAY_WIDTH = 600;
    public const int SOLUTION_DISPLAY_HEIGHT = 150;
    public const float COLOR_BAND_SCALE_WIDTH = 0.5f;
    public const float COLOR_BAND_SCALE_HEIGHT = 2f;

    GameObject[] spectraDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    SpectraPuzzle puzzle1;
    GameObject solutionDisplay;
    RectTransform solutionRect;

    // Start is called before the first frame update
    void Start()
    {
        solutionDisplay = GameObject.Find("SpectraSolution");
        solutionRect = solutionDisplay.AddComponent<RectTransform>();
        solutionRect.transform.localScale = new Vector3(.8f, 1, 1);
        puzzle1 = solutionDisplay.AddComponent<SpectraPuzzle>();

        puzzle1.InitializeSpectraPuzzle("Spectra Puzzle 1", 0);

        solutionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SOLUTION_DISPLAY_WIDTH);
        solutionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SOLUTION_DISPLAY_HEIGHT);

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            spectraDisplayPrimatives[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
            spectraDisplayPrimatives[i].transform.SetParent(solutionRect);
            
            MeshRenderer renderer = spectraDisplayPrimatives[i].GetComponent<MeshRenderer>();
            Material spectraMat = Resources.Load("SpectraMaterial", typeof(Material)) as Material;
            renderer.material = spectraMat;
            renderer.material.color = puzzle1.GetSpectraDisplayColor(true, i);

            spectraDisplayPrimatives[i].transform.localPosition = new Vector3(i, 0, 0);
            spectraDisplayPrimatives[i].transform.localScale = new Vector3(GetBandWidthValueFromColor(renderer.material.color), COLOR_BAND_SCALE_HEIGHT, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float GetBandWidthValueFromColor(Color32 color)
    {
        float result = 0.0f;

        result = (color.a/255.0f) * COLOR_BAND_SCALE_WIDTH;

        return result;
    }
}
