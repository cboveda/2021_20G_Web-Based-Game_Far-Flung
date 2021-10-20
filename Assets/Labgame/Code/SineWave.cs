using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public LineRenderer wave;
    public bool isInitialized;
    Material myMat;
    public float amplitude;
    public float frequency;

    // These display values will be used for lerping/smooth transition.
    public float displayAmplitude = 0.1f;
    public float displayFrequency = 0.1f;

    const float WAVE_START_X_POSITION = -2;
    const float WAVE_RESOLUTION = 0.0001f;
    const int POSITION_COUNT = 400;
    const float WAVE_WIDTH = 0.1f;
    const float SPEED = 1;
    const float WAVE_LERP_AMOUNT = 0.001f;
    const float WAVE_LERP_ROUNDING_MULTIPLIER = 1000.0f;
    const float LERP_SPEED = 1.0f;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        
        

        

    }

    public void InitializeSineWave()
    {
        wave = gameObject.AddComponent<LineRenderer>();
        //lineShader = Shader.Find("Custom/CRTLine");
        //wave.material.shader = lineShader;
        //myMat = (Material)Resources.Load("CRTMat", typeof(Material));
        wave.material = myMat;
        
        wave.startWidth = WAVE_WIDTH;
        wave.endWidth = WAVE_WIDTH;
        wave.useWorldSpace = false;
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (wave != null)
        {
            float x = WAVE_START_X_POSITION;
            float y;
            float waveNumber = 2 * Mathf.PI * displayFrequency;
            float w = waveNumber * SPEED;
            wave.positionCount = POSITION_COUNT;
            for (int i = 0; i < wave.positionCount; i++)
            {
                x += i * WAVE_RESOLUTION;
                y = displayAmplitude * Mathf.Sin(waveNumber * x + w * Time.time);
                wave.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        //waveLERP();
        newLERP();


        
    }

    public void SetMaterial(Material mat)
    {
        myMat = mat;
    }

    public void SetParameters(RadioPuzzleParams myParams)
    {
        amplitude = myParams.Amplitude;
        frequency = myParams.Frequency;
    }

    private void waveLERP()
    {
        float amplitudeLowBound = amplitude - WAVE_LERP_AMOUNT;
        float amplitudeUpBound = amplitude + WAVE_LERP_AMOUNT;
        float frequencyLowBound = frequency - WAVE_LERP_AMOUNT;
        float frequencyUpBound = frequency + WAVE_LERP_AMOUNT;

        if (displayAmplitude < amplitudeUpBound && displayAmplitude > amplitudeLowBound)
        {
            displayAmplitude = amplitude;
        }

        if (displayFrequency < frequencyUpBound && displayFrequency > frequencyLowBound)
        {
            displayFrequency = frequency;
        }

        if (amplitude != displayAmplitude)
        {
            if (amplitude > displayAmplitude)
            {
                displayAmplitude += WAVE_LERP_AMOUNT;
                displayAmplitude = Mathf.Round(displayAmplitude * WAVE_LERP_ROUNDING_MULTIPLIER) * WAVE_LERP_AMOUNT;
            }
            else
            {
                displayAmplitude -= WAVE_LERP_AMOUNT;
                displayAmplitude = Mathf.Round(displayAmplitude * WAVE_LERP_ROUNDING_MULTIPLIER) * WAVE_LERP_AMOUNT;
            }
        }

        if (frequency != displayFrequency)
        {
            if (frequency > displayFrequency)
            {
                displayFrequency += WAVE_LERP_AMOUNT;
                displayFrequency = Mathf.Round(displayFrequency * WAVE_LERP_ROUNDING_MULTIPLIER) * WAVE_LERP_AMOUNT;
            }
            else
            {
                displayFrequency -= WAVE_LERP_AMOUNT;
                displayFrequency = Mathf.Round(displayFrequency * WAVE_LERP_ROUNDING_MULTIPLIER) * WAVE_LERP_AMOUNT;
            }
        }
    }

    private void newLERP()
    {
        timer += 0.5f * Time.deltaTime;
        displayAmplitude = Mathf.Lerp(displayAmplitude, amplitude, timer);
        displayFrequency = Mathf.Lerp(displayFrequency, frequency, timer);

        float testFrequency = Mathf.Round(displayFrequency * 10) * 0.1f;
        float testAmplitude = Mathf.Round(displayAmplitude * 10) * 0.1f;

        if (displayFrequency == frequency && displayAmplitude == amplitude)
        {
            timer = 0;
        }
    }

    public void DestroyWave()
    {
        //Destroy the linerenderer
        Destroy(wave);
        //Destroy this object too
        Destroy(this);
    }
}
