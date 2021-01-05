using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPlayerAnimation : MonoBehaviour
{

    private Animator playerAnimator;
    [SerializeField] private ScrollActionPlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("isRun", playerInfo.isMove);
        playerAnimator.SetBool("isJump", playerInfo.isJump);
    }
}
