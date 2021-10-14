using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private float _t;
    private Vector2 _position;
    private bool _unlocked;
    public Transform Path;
    public float Speed;

    // Start is called before the first frame update
    public void Start()
    {
        _t = 0f;
        _unlocked = true;
        transform.position = Path.GetChild(0).position;
    }

    public void BeginMovement()
    {
        if (_unlocked)
        {
            StartCoroutine(Move());
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

        while (_t < 1)
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
    }
}
