using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform path;
    private float t;
    public float speed;
    private Vector2 position;
    private bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        unlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked) {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move() {
        // Prevent other Coroutines from starting.
        unlocked = false;

        Vector2 start = path.GetChild(0).position;
        Vector2 startDir = path.GetChild(1).position;
        Vector2 end = path.GetChild(2).position;
        Vector2 endDir = path.GetChild(3).position;

        while (t < 1) {
            t += Time.deltaTime * speed;

            // Determine and set new position
            position = Mathf.Pow(1-t, 3) * start +
                3 * Mathf.Pow(1-t, 2) * t * startDir +
                3 * (1-t) * Mathf.Pow(t, 2) * endDir +
                Mathf.Pow(t, 3) * end;
            transform.position = position;
            
            //Sync with framerate
            yield return new WaitForEndOfFrame();
        }
    
        t = 0f;

        // Release lock on Coroutines.
        unlocked = true;
    }
}
