using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatellitePathFader : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _locked;
    private float _timer;
    private float _fadeDuration;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _locked = false;
        _timer = 0;
        _fadeDuration = 1;
    }

    public void BeginFade(float duration)
    {
        _fadeDuration = duration;
        if (!_locked)
        {
            StartCoroutine("Fade");
        }
    }

    private IEnumerator Fade()
    {
        while (_timer < _fadeDuration) 
        {
            _timer += Time.deltaTime;
            float alpha = 1 - (_timer / _fadeDuration);
            Color newColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
            _renderer.color = newColor;
            yield return new WaitForEndOfFrame();
        }
        Destroy(GetComponent<Transform>().gameObject);
    }
}
