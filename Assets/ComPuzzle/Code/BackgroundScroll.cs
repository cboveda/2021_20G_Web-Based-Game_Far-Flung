using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    public float scrollSpeed = 0.1f;
    Vector2 startPos;
    float newPos = 0.0f;
    int moveRight = 0;
    int direction = 0;
    bool rightActive = true;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Mathf.Repeat(Time.time * scrollSpeed, 20);
        transform.position = startPos + Vector2.right * newPos; 

    }
}
