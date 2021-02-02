using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossAnimation : MonoBehaviour
{
    [SerializeField] private BossBattleBossInfo bossInfo;
    private Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bossAnimator.SetBool("isMove", bossInfo.isMove);
        bossAnimator.SetBool("isJump", bossInfo.isJump);
    }
}
