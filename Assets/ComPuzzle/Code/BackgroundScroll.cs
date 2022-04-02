using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    
    float speed = 0.0005f;
    float startPos = 0.0f;
    float yPos = 0.0f;
    float newPos = 0.0f;
    bool moveLeft = true;
    int direction = 0;
    bool rightActive = true;
    GameObject scrollBackground;
    bool scroll = true;
    float xPos = 0.0f;
    int leftLimit = 11;
    int rightLimit = -11;
    float time;

    public bool getScroll
    {
        get { return scroll; }
        set { scroll = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
        scrollBackground = GameObject.Find("ScrollBackground");
        startPos = scrollBackground.transform.position.x;
        yPos = scrollBackground.transform.position.y;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);

        //Debug.Log("delay");
        scroll = true;

        if (moveLeft)
        {
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(scroll);
        if (scroll)
        {
            xPos = scrollBackground.transform.position.x;
            if (moveLeft)
            {
                //Debug.Log("move left");
                
                if (xPos < leftLimit)
                {
                    startPos += speed;

                }
                else
                {
                    scroll = false;
                    StartCoroutine(Delay());
                    
                }
            }

            if (!moveLeft)
            {
                //Debug.Log("move right");
                
                if (xPos > rightLimit)
                {
                    startPos -= speed;

                }
                else
                {
                    scroll = false;
                    StartCoroutine(Delay());
                }
            }  
            
            scrollBackground.transform.position = new Vector2(startPos, yPos);
        }
    }
}
