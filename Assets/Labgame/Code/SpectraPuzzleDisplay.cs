using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectraPuzzleDisplay : MonoBehaviour
{
    public const int SPECTRA_DISPLAY_WIDTH = 600;
    public const int SPECTRA_DISPLAY_HEIGHT = 150;
    public const float SPECTRA_DISPLAY_SCALE = 0.8f;
    public const float COLOR_BAND_SCALE_WIDTH = 0.5f;
    public const float COLOR_BAND_SCALE_HEIGHT = 2f;

    GameObject[] spectraSolutionDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraAttemptDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    SpectraPuzzle currentPuzzle;
    GameObject solutionDisplay;
    RectTransform solutionRect;

    GameObject attemptDisplay;
    RectTransform attemptRect;

    // Start is called before the first frame update
    void Awake()
    {
        solutionDisplay = GameObject.Find("SpectraSolution");

        if((solutionRect = solutionDisplay.GetComponent<RectTransform>()) == null)
        {
            solutionRect = solutionDisplay.AddComponent<RectTransform>();
        }

        
        solutionRect.transform.localScale = new Vector3(SPECTRA_DISPLAY_SCALE, 1, 1);
        
        solutionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SPECTRA_DISPLAY_WIDTH);
        solutionRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SPECTRA_DISPLAY_HEIGHT);

        

        attemptDisplay = GameObject.Find("SpectraAttempt");

        if ((attemptRect = attemptDisplay.GetComponent<RectTransform>()) == null)
        {
            attemptRect = attemptDisplay.AddComponent<RectTransform>();
        }

        
        attemptRect.transform.localScale = new Vector3(SPECTRA_DISPLAY_SCALE, 1, 1);

        attemptRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SPECTRA_DISPLAY_WIDTH);
        attemptRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SPECTRA_DISPLAY_HEIGHT);

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            spectraSolutionDisplayPrimatives[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
            spectraSolutionDisplayPrimatives[i].transform.SetParent(solutionRect);

            
        }

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            spectraAttemptDisplayPrimatives[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
            spectraAttemptDisplayPrimatives[i].transform.SetParent(attemptRect);


        }

    }

    public void UpdateSolutionDisplay()
    {
        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            MeshRenderer renderer = spectraSolutionDisplayPrimatives[i].GetComponent<MeshRenderer>();
            Material spectraMat = Resources.Load("SpectraMaterial", typeof(Material)) as Material;
            renderer.material = spectraMat;
            renderer.material.color = currentPuzzle.GetSpectraDisplayColor(true, i);

            spectraSolutionDisplayPrimatives[i].transform.localPosition = new Vector3(i, 0, 0);
            spectraSolutionDisplayPrimatives[i].transform.localScale = new Vector3(GetBandWidthValueFromColor(renderer.material.color), COLOR_BAND_SCALE_HEIGHT, 1);
        }


    }

    public void UpdateAttemptDisplay()
    {
        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            MeshRenderer renderer = spectraAttemptDisplayPrimatives[i].GetComponent<MeshRenderer>();
            Material spectraMat = Resources.Load("SpectraMaterial", typeof(Material)) as Material;
            renderer.material = spectraMat;
            renderer.material.color = currentPuzzle.GetSpectraDisplayColor(false, i);

            spectraAttemptDisplayPrimatives[i].transform.localPosition = new Vector3(i, 0, 0);
            spectraAttemptDisplayPrimatives[i].transform.localScale = new Vector3(GetBandWidthValueFromColor(renderer.material.color), COLOR_BAND_SCALE_HEIGHT, 1);
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

    public void SetSpectraPuzzleToDisplay(SpectraPuzzle puzzle)
    {
        currentPuzzle = puzzle;
        UpdateSolutionDisplay();
        UpdateAttemptDisplay();
    }

    public void ClearPuzzleDisplay()
    {
        //Destroy(solutionRect);
        //Destroy(attemptRect);

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            Destroy(spectraAttemptDisplayPrimatives[i]);
            Destroy(spectraSolutionDisplayPrimatives[i]);
        }

        Destroy(this);
    }
}