using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float moveTime;
}

public class SneakingEnemyMover : MonoBehaviour
{
    [SerializeField] private SneakingEnemyMoveData[] moveDatas;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMove;

    private Transform enemyTrans;
    private Rigidbody rb;

    private SneakingEnemyMoveData nowMoveData;
    private int nowMoveDataNum;

    private float moveTimer;

    private bool isLoadMoveData;

    private void Awake()
    {
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (!isLoadMoveData)
            {
                nowMoveData = moveDatas[nowMoveDataNum];

                switch (nowMoveData.rotateDirection)
                {
                    case RotateDirection.FORWORD:
                        enemyTrans.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case RotateDirection.RIGHT:
                        enemyTrans.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case RotateDirection.BACK:
                        enemyTrans.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case RotateDirection.LEFT:
                        enemyTrans.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                }

                isLoadMoveData = true;
            }

            if (moveTimer >= nowMoveData.moveTime)
            {
                nowMoveDataNum++;

                if (nowMoveDataNum == moveDatas.Length)
                {
                    nowMoveDataNum = 0;
                }

                moveTimer = 0;
                isLoadMoveData = false;
            }
            else
            {
                rb.velocity = enemyTrans.forward * moveSpeed;
                moveTimer += Time.fixedDeltaTime;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
