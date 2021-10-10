using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public LineRenderer wave;
    Shader lineShader;
    GameObject worldObj;
    bool isInitialized;
    
    public float amplitude;
    public float frequency;

    const float WAVE_START_X_POSITION = -2;
    const float WAVE_RESOLUTION = 0.0001f;
    const int POSITION_COUNT = 400;
    const float WAVE_WIDTH = 0.05f;
    const float SPEED = 1;


    // Start is called before the first frame update
    void Start()
    {
        
        

        

    }

    public void InitializeSineWave()
    {
        wave = gameObject.AddComponent<LineRenderer>();
        //lineShader = Shader.Find("Custom/CRTLine");
        //wave.material.shader = lineShader;
        Material myMat = (Material)Resources.Load("CRTMat", typeof(Material));
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
            float waveNumber = 2 * Mathf.PI * frequency;
            float w = waveNumber * SPEED;
            wave.positionCount = POSITION_COUNT;
            for (int i = 0; i < wave.positionCount; i++)
            {
                x += i * WAVE_RESOLUTION;
                y = amplitude * Mathf.Sin(waveNumber * x + w * Time.time);
                wave.SetPosition(i, new Vector3(x, y, 0));
            }
        }
        


        
    }

    public void SetParameters(RadioPuzzleParams myParams)
    {
        amplitude = myParams.Amplitude;
        frequency = myParams.Frequency;
    }

    public void DestroyWave()
    {
        //Destroy the linerenderer
        Destroy(wave);
        //Destroy this object too
        Destroy(this);
    }
}
