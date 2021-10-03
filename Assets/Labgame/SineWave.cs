using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    public Vector3 waveStart;
    public Vector3 waveEnd;
    public LineRenderer wave;
    public float waveWidth;
    public float amplitude;
    public float frequency;
    public float speed;
    public int positionCount;
    public float waveStartX;
    public float waveResolution;

    public Plane myPlane;

    // Start is called before the first frame update
    void Start()
    {
        //myPlane = GetComponent<Plane>();
        wave = gameObject.AddComponent<LineRenderer>();
        waveWidth = 0.05f;
        waveStartX = -2;
        amplitude = 1;
        frequency = 1;
        positionCount = 400;
        speed = 1;
        waveResolution = 0.0001f;
        //waveStart = new Vector3(-2, .5f);
        //waveEnd = new Vector3(2, .5f);
        //wave.SetPosition(0, waveStart);
        //wave.SetPosition(1, waveEnd);
        wave.useWorldSpace = false;
        wave.startWidth = waveWidth;
        wave.endWidth = waveWidth;

    }

    // Update is called once per frame
    void Update()
    {
        float x = waveStartX;
        float y;
        float waveNumber = 2 * Mathf.PI * frequency;
        float w = waveNumber * speed;
        wave.positionCount = positionCount;
        for (int i = 0; i < positionCount; i++)
        {
            x += i * waveResolution;
            y = amplitude * Mathf.Sin(waveNumber * x + w * Time.time);
            wave.SetPosition(i, new Vector3(x, y, 0));
        }


        
    }
}
