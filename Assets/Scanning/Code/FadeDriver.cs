using UnityEngine;

public class FadeDriver : MonoBehaviour {

    public Animator animator;

    public void TriggerFade() {
        animator.SetTrigger("FadeOut");
    }
}
