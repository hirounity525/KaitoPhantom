using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DPlayerAnimation : MonoBehaviour
{

    
    [SerializeField] private RunJump3DPlayerInfo playerInfo;
    [SerializeField] private RunJump3DPlayerMover playerMover;
    private Animator playerAnimator;
    private Transform playerTrans;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        //playerTrans = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMover.playerMoveDirection == 0)
        {
            playerTrans.rotation = Quaternion.Euler(0, 270, 0);
        }

        else if (playerMover.playerMoveDirection == 1)
        {
            playerTrans.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (playerMover.playerMoveDirection == 2)
        {
            playerTrans.rotation = Quaternion.Euler(0, 90, 0);
        }

        else if (playerMover.playerMoveDirection == 3)
        {
            playerTrans.rotation = Quaternion.Euler(0, 180, 0);
        }
        playerAnimator.SetBool("isJump", playerInfo.isJump);
        playerAnimator.SetBool("isCrouch", playerInfo.isCrouch);
    }
}
