using UnityEngine;

public class FadeBanner : MonoBehaviour
{
   public Animator animator;

    public void TriggerFade() {
        animator.SetTrigger("FadeIn");
    }
}
