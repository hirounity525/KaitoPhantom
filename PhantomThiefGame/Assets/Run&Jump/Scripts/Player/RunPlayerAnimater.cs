using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerAnimater : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Animator colliderAnimator;
    private RunPlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        colliderAnimator = GetComponent<Animator>();
        playerCore = GetComponent<RunPlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("FirstJump", playerCore.firstJump);
        animator.SetBool("SecondJump", playerCore.secondJump);
        animator.SetBool("IsGround", playerCore.isGround);
        animator.SetBool("IsSliding", playerCore.isSliding);

        colliderAnimator.SetBool("FirstJump", playerCore.firstJump);
        colliderAnimator.SetBool("SecondJump", playerCore.secondJump);
        colliderAnimator.SetBool("IsGround", playerCore.isGround);
        colliderAnimator.SetBool("IsSliding", playerCore.isSliding);
    }
}
