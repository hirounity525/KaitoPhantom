using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMover : MonoBehaviour
{
    [SerializeField] private float startTransY;
    [SerializeField] private float startDegree;
    [SerializeField] private float moveRadius;
    [SerializeField] private float moveStraightSpeed;
    [SerializeField] private float moveCircleSpeed;
    [SerializeField] private float returnMoveTime;

    private Rigidbody rb;

    private Transform enemyTrans;

    private Vector3 enemyLastTrans;
    private Vector3 enemyVector;

    private float plusDegree;
    private float countTime;
    private float nowReturnMoveTime;

    private bool canMoveStraight=false;
    // Start is called before the first frame update
    void Start()
    {
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        nowReturnMoveTime = returnMoveTime;

        enemyLastTrans = enemyTrans.position;

        this.transform.position = new Vector3(moveRadius*Mathf.Cos(Mathf.PI/180*startDegree),startTransY, -moveRadius * Mathf.Sin(Mathf.PI / 180 * startDegree));

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMoveStraight)
        {
            this.transform.position = new Vector3(moveRadius * Mathf.Cos(Mathf.PI / 180 * (startDegree+plusDegree)), startTransY, -moveRadius * Mathf.Sin(Mathf.PI / 180 * (startDegree+plusDegree)));
        }
        else
        {
            rb.velocity = new Vector3(-moveStraightSpeed, 0, 0);
        }

        plusDegree = plusDegree + moveCircleSpeed;

        countTime = Time.time;
        if (countTime >= nowReturnMoveTime)
        {
            plusDegree = plusDegree * -1;
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
                startDegree = 180;
                plusDegree = 0;
            }
        }
    }

}
