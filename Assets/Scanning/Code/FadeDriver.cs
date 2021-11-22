using UnityEngine;

public class FadeDriver : MonoBehaviour {

    public Animator animator;

    public virtual void TriggerFade() {  // virtual added to allow unit testing
        animator.SetTrigger("FadeOut");
    }
}
