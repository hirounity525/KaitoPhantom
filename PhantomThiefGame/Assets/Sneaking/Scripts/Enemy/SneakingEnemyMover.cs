using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum RotateDirection
{
    FORWORD,
    RIGHT,
    BACK,
    LEFT
}

[System.Serializable]
public struct SneakingEnemyMoveData
{
    public RotateDirection rotateDirection;
    public float moveDistance;
}

public class SneakingEnemyMover : MonoBehaviour
{
    [SerializeField] private SneakingEnemyMoveData[] moveDatas;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private float rotateTime;

    private SneakingEnemyCore enemyCore;
    private Transform enemyTrans;
    private Rigidbody rb;

    private Vector3 firstPos;
    private Quaternion firstRot;

    private SneakingEnemyMoveData nowMoveData;
    private int nowMoveDataNum;

    private bool isMove;

    private float moveTime;
    private float moveTimer;

    private Tweener rotationTweener;

    private bool isLoadMoveData;

    //Gizmos
    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        enemyCore = GetComponent<SneakingEnemyCore>();
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        firstPos = enemyTrans.position;
        firstRot = enemyTrans.rotation;
    }

    private void Update()
    {
        enemyCore.isMove = isMove;

        if (enemyCore.isReset)
        {
            ResetMove();

            enemyCore.isReset = false;
        }
    }

    private void FixedUpdate()
    {
        if (!enemyCore.isDiscovery)
        {
            if (!isLoadMoveData)
            {
                nowMoveData = moveDatas[nowMoveDataNum];

                moveTime = nowMoveData.moveDistance / moveSpeed;

                float rotationTemp = 0;

                switch (nowMoveData.rotateDirection)
                {
                    case RotateDirection.FORWORD:
                        rotationTemp = 0;
                        break;
                    case RotateDirection.RIGHT:
                        rotationTemp = 90;
                        break;
                    case RotateDirection.BACK:
                        rotationTemp = 180;
                        break;
                    case RotateDirection.LEFT:
                        rotationTemp = 270;
                        break;
                }

                rotationTweener = enemyTrans.DORotate(Vector3.up * rotationTemp, rotateTime)
                   .OnComplete(() =>
                   {
                       isMove = true;
                   });

                isLoadMoveData = true;
            }

            if (isMove)
            {
                if (moveTimer >= moveTime)
                {
                    isMove = false;
                    StartCoroutine(StopWalking());
                }
                else
                {
                    rb.velocity = enemyTrans.forward * moveSpeed;
                    moveTimer += Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            isMove = false;
            rotationTweener.Kill();
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private IEnumerator StopWalking()
    {
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(waitTime);

        nowMoveDataNum++;
        if(nowMoveDataNum >= moveDatas.Length)
        {
            nowMoveDataNum = 0;
        }

        moveTimer = 0;

        isLoadMoveData = false;
    }

    private void ResetMove()
    {
        enemyTrans.position = firstPos;
        enemyTrans.rotation = firstRot;

        rb.constraints = RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ;

        moveTimer = 0;
        nowMoveDataNum = 0;
        isLoadMoveData = false;
    }

    private void OnDrawGizmosSelected()
    {
        startPos = transform.position;
        endPos = Vector3.zero;

        for(int i = 0; i < moveDatas.Length; i++)
        {
            startPos += endPos;

            Vector3 dir = Vector3.zero;

            switch (moveDatas[i].rotateDirection)
            {
                case RotateDirection.FORWORD:
                    dir = Vector3.forward;
                    break;
                case RotateDirection.RIGHT:
                    dir = Vector3.right;
                    break;
                case RotateDirection.BACK:
                    dir = Vector3.back;
                    break;
                case RotateDirection.LEFT:
                    dir = Vector3.left;
                    break;
            }

            endPos = dir.normalized * moveDatas[i].moveDistance;

            Gizmos.DrawLine(startPos, startPos + endPos);
        }
    }
}
