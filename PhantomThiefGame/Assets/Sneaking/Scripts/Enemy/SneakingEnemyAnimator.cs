using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingEnemyAnimator : MonoBehaviour
{
    private SneakingEnemyCore enemyCore;
    private Animator animator;

    private void Awake()
    {
        enemyCore = GetComponent<SneakingEnemyCore>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMove", enemyCore.isMove);
    }
}
