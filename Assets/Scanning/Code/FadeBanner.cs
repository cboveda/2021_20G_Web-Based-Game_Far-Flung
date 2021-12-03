using UnityEngine;

public class FadeBanner : MonoBehaviour
{
   public Animator animator;

    public virtual void TriggerFade() { // virtual added to allow unit testing
        animator.SetTrigger("FadeIn");
    }
}
