﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerMover : MonoBehaviour
{
    [SerializeField] private DefenseInputProvider inputProvider;
    [SerializeField] private Transform[] moveTrans;
    [SerializeField] private int row, column;
    [SerializeField] private int  nowPosNum = 0;
    private int movePosNum;
    private Transform playerTrans;
    private bool canMove=true;
    [SerializeField] private float moveWaitTime;
    private DefensePlayerCore playerCore;
    private bool moveDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<DefensePlayerCore>();
        movePosNum = nowPosNum;
        playerTrans = GetComponent<Transform>();
        playerTrans.position = moveTrans[nowPosNum].position;
    }

    // Update is called once per frame
    void Update()
    {
        moveDelay = playerCore.canMove;

        if (moveDelay)
        {
            if (canMove)
            {
                if (inputProvider.isMoveButtonDown)
                {
                    switch (inputProvider.moveArrow)
                    {
                        case InputArrow.UP:
                            movePosNum -= row;
                            if (movePosNum < 0) movePosNum = nowPosNum;
                            break;
                        case InputArrow.DOWN:
                            movePosNum += row;
                            if (movePosNum >= (row * column)) movePosNum = nowPosNum;
                            break;
                        case InputArrow.LEFT:
                            playerCore.isRight = false;
                            movePosNum--;
                            if (nowPosNum % row == 0) movePosNum = nowPosNum;
                            break;
                        case InputArrow.RIGHT:
                            playerCore.isRight = true;
                            movePosNum++;
                            if (nowPosNum % row == row - 1) movePosNum = nowPosNum;
                            break;
                    }

                    if (movePosNum != nowPosNum)
                    {
                        nowPosNum = movePosNum;
                        playerTrans.position = moveTrans[nowPosNum].position;

                        StartCoroutine(StartMove());
                    }


                }
            }
        }

        
    }

    private IEnumerator StartMove()
    {
        canMove = false;

        yield return new WaitForSeconds(moveWaitTime);

        canMove = true;
    }
}
