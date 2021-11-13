using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDataActions : MonoBehaviour
{

    public bool signalAnimation = false;
    GameObject signalObject;
    GameObject letterObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool animationOn
    {
        get { return signalAnimation; }
        set { signalAnimation = value; }
    }

    public void SendData()
    {
        Debug.Log("send data");
        FindObjectOfType<SendDataActions>().animationOn = true;

        StartCoroutine(AnimationTimer());

        StartCoroutine(SignalAnimation());

    }

    IEnumerator AnimationTimer()
    {
        FindObjectOfType<SendDataActions>().animationOn = true;
        yield return new WaitForSeconds(7f);
        FindObjectOfType<SendDataActions>().animationOn = false;
    }

    IEnumerator SignalAnimation()
    {
        // add 3 second delay

        signalObject = GameObject.Find("signals");        
        Color letterColor = new Color32(246, 34, 250, 255);
        Color hiddenColor = new Color32(246, 34, 250, 0);
        
        int backgroundChild = 0;
        int textChild = 0;

        int x = 0;
        int y = 0;
        signalAnimation = FindObjectOfType<SendDataActions>().animationOn;
        while (signalAnimation)
        {
            
            if (x == 19)
            {               
                x = 0;
                y++;
                if (x < 7)
                {

                    Debug.Log(y.ToString() + "_Button");
                    letterObject = GameObject.Find(y.ToString() + "_Button");
                    letterObject.transform.GetChild(backgroundChild).GetChild(textChild).GetComponent<UnityEngine.UI.Text>().color = letterColor;
                }
            }

            signalObject.transform.GetChild(x).GetComponent<SpriteRenderer>().sortingLayerName = "Board";

            yield return new WaitForSeconds(0.05f);

            signalObject.transform.GetChild(x).GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
                        
            x++;
            signalAnimation = FindObjectOfType<SendDataActions>().animationOn;
        }
    }
}
