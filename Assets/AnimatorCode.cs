using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCode : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool needsOtherAnimation;
    public bool playOtherAnimation;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(bool haveBool = false)
    {
        if (!playOtherAnimation)
        {
            animator.SetTrigger("TrOpen");
            if(needsOtherAnimation)
                playOtherAnimation = true;
        }
        else if (playOtherAnimation)
        {
            animator.SetTrigger("TrClose");
            playOtherAnimation = false;
        }

        if(haveBool)
        {
            animator.SetBool("DoFloat", true);
        }
    }
}
