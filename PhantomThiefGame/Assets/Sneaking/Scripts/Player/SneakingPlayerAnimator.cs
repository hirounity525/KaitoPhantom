using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingPlayerAnimator : MonoBehaviour
{
    [SerializeField] private float moveToIdleWaitTime;

    private SneakingPlayerCore playerCore;
    private Transform playerTrans;
    private Animator animator;

    private bool isMoveAnim;
    private float moveToIdleTimer;

    private bool isHideAnim;

    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
        playerTrans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCore.isHide)
        {
            if (!isHideAnim)
            {
                playerTrans.LookAt(playerTrans.position + Vector3.forward);
                animator.SetInteger("HideType", (int)playerCore.nowHideObjectType);
                animator.SetBool("isHide", true);
                isHideAnim = true;
            }
        }
        else
        {
            if (isHideAnim)
            {
                animator.SetBool("isHide", false);
                isHideAnim = false;
            }

            if (playerCore.moveVec.magnitude >= 0.1f)
            {
                playerTrans.LookAt(playerTrans.position + playerCore.moveVec);
            }

            if (playerCore.isMove)
            {
                animator.SetBool("isMove", true);
                moveToIdleTimer = 0;
                isMoveAnim = true;
            }
            else
            {
                if (isMoveAnim)
                {
                    moveToIdleTimer += Time.deltaTime;
                    if (moveToIdleTimer >= moveToIdleWaitTime)
                    {
                        animator.SetBool("isMove", false);
                        isMoveAnim = false;
                    }
                }
            }
        }
    }
}
