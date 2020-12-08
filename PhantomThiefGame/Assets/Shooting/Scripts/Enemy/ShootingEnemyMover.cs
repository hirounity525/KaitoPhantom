using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMover : MonoBehaviour
{
    [SerializeField] private float startTransY;
    [SerializeField] private float startRadian;
    [SerializeField] private float moveRadius;
   [SerializeField] private float enemySpeed;
   [SerializeField] private float returnMoveTime;
    [SerializeField] private float moveStraightSpeed;
    [SerializeField] private float moveCircleSpeed;
    [SerializeField] private float resetRadian;

    private Rigidbody rb;

    private Transform enemyTrans;

    private float countTime;
    private float nowReturnMoveTime;
    private float nowRadian;
    private int resetTrans;

    private bool canMoveStraight=false;
    // Start is called before the first frame update
    void Start()
    {
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        nowReturnMoveTime = returnMoveTime;

        this.transform.position = new Vector3(moveRadius*Mathf.Cos(startRadian),startTransY, -moveRadius * Mathf.Sin(startRadian));

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMoveStraight)
        {
            rb.velocity = new Vector3(-enemySpeed * moveRadius * Mathf.Sin(moveCircleSpeed * nowRadian + startRadian), 0, -enemySpeed * moveRadius * Mathf.Cos(moveCircleSpeed * nowRadian + startRadian));
        }
        else
        {
            rb.velocity = new Vector3(-moveStraightSpeed, 0, 0);
        }


        nowRadian = nowRadian+Time.deltaTime;
        countTime = Time.time;
        if (countTime >= nowReturnMoveTime)
        {
            rb.velocity = rb.velocity * -1;
            nowReturnMoveTime += returnMoveTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject moveStraight = other.gameObject;

        if (moveStraight.tag == "MoveStraight")
        {
            if (!canMoveStraight)
            {
                canMoveStraight = true;
                this.transform.position = new Vector3(enemyTrans.position.x, enemyTrans.position.y, 0);
            }
            else
            {
                canMoveStraight = false;
                nowRadian = resetRadian;
                startRadian = 0;
            }
        }
    }

}
