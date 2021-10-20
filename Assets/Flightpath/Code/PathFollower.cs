using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private float _t;
    private Vector2 _position;
    private bool _unlocked;
    private bool _resetRequested;
    private bool _stopRequested;
    public Transform Path;
    public float Speed;

    // Start is called before the first frame update
    public void Start()
    {
        InitializePosition();
    }

    private void FixedUpdate() 
    {
        if (_stopRequested)
        {
            StopCoroutine("Move");
        }
        if (_resetRequested)
        {
            InitializePosition();
        }    
    }

    public void BeginMovement()
    {
        if (_unlocked)
        {
            StartCoroutine("Move");
        }
    }

    private IEnumerator Move()
    {
        // Prevent other Coroutines from starting.
        _unlocked = false;

        Vector2 start = Path.GetChild(0).position;
        Vector2 startDir = Path.GetChild(1).position;
        Vector2 end = Path.GetChild(2).position;
        Vector2 endDir = Path.GetChild(3).position;

        while (_t <= 1)
        {
            _t += Time.deltaTime * Speed;

            // Determine and set new position
            _position = Mathf.Pow(1 - _t, 3) * start +
                3 * Mathf.Pow(1 - _t, 2) * _t * startDir +
                3 * (1 - _t) * Mathf.Pow(_t, 2) * endDir +
                Mathf.Pow(_t, 3) * end;
            transform.position = _position;

            //Sync with framerate
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
        _unlocked = true;
    }

    public void ResetPosition()
    {
        _stopRequested = true;
        _resetRequested = true;
    }

    public void StopPosition()
    {
        _stopRequested = true;
    }

    public void InitializePosition()
    {
        _t = 0f;
        _unlocked = true;
        _resetRequested = false;
        _stopRequested = false;
        transform.position = Path.GetChild(0).position;
    }

    public bool IsUnlocked()
    {
        return _unlocked;
    }
}
