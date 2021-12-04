using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text _fpsText;
    [SerializeField] private float _refresh = 1;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > _timer) 
        {
            int frames = (int) (1 / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + frames;
            _timer = Time.unscaledTime + _refresh;
        }   
    }
}
