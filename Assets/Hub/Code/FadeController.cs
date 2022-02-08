using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image blackFaderImage;
    public Animator fadeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitUntil(() => blackFaderImage.color.a == 1);
        fadeAnimator.SetBool("Fade", false);

    }

    public void Fade()
    {
        StartCoroutine(FadeIn());
        //FadeIn();
    }

    public void ResetAndFade()
    {
        fadeAnimator.Play("FadeIn");
    }
}
