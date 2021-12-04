using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class SatellitePathFader : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private bool _startFade;
        private float _timer;
        private float _fadeDuration;

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _startFade = false;
            _timer = 0;
            _fadeDuration = 1;
        }

        void Update()
        {
            if (_startFade)
            {
                _timer += Time.deltaTime;
                float alpha = 1 - (_timer / _fadeDuration);
                Color newColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
                _renderer.color = newColor;
            }
            if (_timer >= _fadeDuration)
            {
                Destroy(GetComponent<Transform>().gameObject);
            }
        }

        public void BeginFade(float duration)
        {
            _fadeDuration = duration;
            _startFade = true;
        }
    }
}
