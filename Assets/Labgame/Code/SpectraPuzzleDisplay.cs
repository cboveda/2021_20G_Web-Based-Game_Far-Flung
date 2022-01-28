using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectraPuzzleDisplay : MonoBehaviour
{
    public const int SPECTRA_DISPLAY_WIDTH = 600;
    public const int SPECTRA_DISPLAY_HEIGHT = 150;
    public const float SPECTRA_DISPLAY_SCALE_WIDTH = 0.8f;
    public const float SPECTRA_DISPLAY_SCALE_HEIGHT = 0.8f;
    public const float COLOR_BAND_SCALE_WIDTH = 0.5f;
    public const float COLOR_BAND_SCALE_HEIGHT = 2f;

    GameObject[] spectraSolutionDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraAttemptDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraExampleDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraPrimaryDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraSecondaryDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject[] spectraTraceDisplayPrimatives = new GameObject[Spectra.SPECTRA_ARRAY_SIZE];
    GameObject elementNameDisplay;
    Text elementName;
    SpectraPuzzle currentPuzzle;

    GameObject solutionDisplay;
    RectTransform solutionRect;

    GameObject attemptDisplay;
    RectTransform attemptRect;

    GameObject primaryDisplay;
    RectTransform primaryRect;

    GameObject secondaryDisplay;
    RectTransform secondaryRect;

    GameObject traceDisplay;
    RectTransform traceRect;

    GameObject exampleDisplay;
    RectTransform exampleRect;

    GameObject spectraResponse;
    Text responseText;


    // Start is called before the first frame update
    void Awake()
    {
        MakeSpectraDisplay(solutionDisplay, "SpectraSolution", solutionRect, spectraSolutionDisplayPrimatives);
        MakeSpectraDisplay(attemptDisplay, "SpectraAttempt", attemptRect, spectraAttemptDisplayPrimatives);
        MakeSpectraDisplay(exampleDisplay, "SpectraExample", exampleRect, spectraExampleDisplayPrimatives);
        MakeSpectraDisplay(primaryDisplay, "SpectraPrimary", primaryRect, spectraPrimaryDisplayPrimatives);
        MakeSpectraDisplay(secondaryDisplay, "SpectraSecondary", secondaryRect, spectraSecondaryDisplayPrimatives);
        MakeSpectraDisplay(traceDisplay, "SpectraTrace", traceRect, spectraTraceDisplayPrimatives);



        elementNameDisplay = GameObject.Find("ElementName");
        elementName = elementNameDisplay.GetComponent<Text>();

        spectraResponse = GameObject.Find("ResponseIndicator");
        responseText = spectraResponse.GetComponent<Text>();

        

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

    public void UpdateExampleDisplay()
    {
        ClearStatusDisplay();
        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            MeshRenderer renderer = spectraExampleDisplayPrimatives[i].GetComponent<MeshRenderer>();
            Material spectraMat = Resources.Load("SpectraMaterial", typeof(Material)) as Material;
            renderer.material = spectraMat;
            renderer.material.color = currentPuzzle.GetSpectraExampleColor(i);
            

            spectraExampleDisplayPrimatives[i].transform.localPosition = new Vector3(i, 0, 0);
            spectraExampleDisplayPrimatives[i].transform.localScale = new Vector3(GetBandWidthValueFromColor(renderer.material.color), COLOR_BAND_SCALE_HEIGHT, 1);
        }
        elementName.text = currentPuzzle.spectraList[currentPuzzle.selectedElementSlot].GetSpectraName();


    }

    public void UpdateElementalDisplay(int elementalDisplayToUpdate)
    {
        GameObject[] elementalDisplayPrimatives;
        ClearStatusDisplay();
        switch (elementalDisplayToUpdate)
        {
            case 1:
                elementalDisplayPrimatives = spectraPrimaryDisplayPrimatives;
                break;
            case 2:
                elementalDisplayPrimatives = spectraSecondaryDisplayPrimatives;
                break;
            case 3:
                elementalDisplayPrimatives = spectraTraceDisplayPrimatives;
                break;
            default:
                elementalDisplayPrimatives = spectraPrimaryDisplayPrimatives;
                break;
        }

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            
            MeshRenderer renderer = elementalDisplayPrimatives[i].GetComponent<MeshRenderer>();
            Material spectraMat = Resources.Load("SpectraMaterial", typeof(Material)) as Material;
            renderer.material = spectraMat;
            renderer.material.color = currentPuzzle.GetAddedElementColor(elementalDisplayToUpdate, i);


            elementalDisplayPrimatives[i].transform.localPosition = new Vector3(i, 0, 0);
            elementalDisplayPrimatives[i].transform.localScale = new Vector3(GetBandWidthValueFromColor(renderer.material.color), COLOR_BAND_SCALE_HEIGHT, 1);
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
            Destroy(spectraExampleDisplayPrimatives[i]);
            Destroy(spectraPrimaryDisplayPrimatives[i]);
            Destroy(spectraSecondaryDisplayPrimatives[i]);
            Destroy(spectraTraceDisplayPrimatives[i]);
        }
        ClearStatusDisplay();
        Destroy(this);
    }

    private void MakeSpectraDisplay(GameObject goDisplay, string goName, RectTransform displayRect, GameObject[] displayPrimatives)
    {
        float displayScale = SPECTRA_DISPLAY_SCALE_HEIGHT;

        // Decided too late I wanted a separate height possibility...
        if(goDisplay == primaryDisplay || goDisplay == secondaryDisplay || goDisplay == traceDisplay)
        {
            displayScale = 0.5f;
        }

        goDisplay = GameObject.Find(goName);

        if ((displayRect = goDisplay.GetComponent<RectTransform>()) == null)
        {
            displayRect = goDisplay.AddComponent<RectTransform>();
        }


        displayRect.transform.localScale = new Vector3(SPECTRA_DISPLAY_SCALE_WIDTH, displayScale, 1);

        displayRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SPECTRA_DISPLAY_WIDTH);
        displayRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SPECTRA_DISPLAY_HEIGHT);

        for (int i = 0; i < Spectra.SPECTRA_ARRAY_SIZE; i++)
        {
            displayPrimatives[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
            displayPrimatives[i].transform.SetParent(displayRect);


        }
    }

    public void DisplayIncorrectGuess()
    {
        responseText.text = "Incorrect guess.";
    }

    public void ClearStatusDisplay()
    {
        responseText.text = "";
    }
}
