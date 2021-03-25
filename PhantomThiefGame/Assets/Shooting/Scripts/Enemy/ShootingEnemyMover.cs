using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMover : MonoBehaviour
{
    [SerializeField] private Transform moveStartTrans;

    [Header("設定")]
    [SerializeField] private float startTransY;
    [SerializeField] private float startDegree;
    [SerializeField] private float moveRadius;
    [SerializeField] private float moveStraightSpeed;
    [SerializeField] private float moveCircleSpeed;

    private Rigidbody rb;

    private Transform enemyTrans;

    private Vector3 enemyLastTrans;
    private Vector3 enemyVector;

    private float plusDegree;
    private float countTime;
    private float nowReturnMoveTime;

    private int accelaration = 1;

    private bool canMoveStraight=false;
    // Start is called before the first frame update
    void Start()
    {
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        enemyLastTrans = enemyTrans.position;

        enemyTrans.position = new Vector3(moveRadius * Mathf.Cos(Mathf.PI / 180 * startDegree), startTransY, -moveRadius * Mathf.Sin(Mathf.PI / 180 * startDegree));

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMoveStraight)
        {
            enemyTrans.position = new Vector3(moveRadius * Mathf.Cos(Mathf.PI / 180 * (startDegree + plusDegree * accelaration)), startTransY, -moveRadius * Mathf.Sin(Mathf.PI / 180 * (startDegree + plusDegree * accelaration)));
        }

    }

    private void FixedUpdate()
    {
        if (!canMoveStraight)
        {
            plusDegree += moveCircleSpeed;
        }
        else
        {
            rb.velocity = new Vector3(-moveStraightSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject moveStraight = other.gameObject;

        if (moveStraight.tag == "MoveStraight")
        {
            if (!canMoveStraight)
            {
                enemyTrans.position = new Vector3(moveStartTrans.position.x, enemyTrans.position.y, 0);

                canMoveStraight = true;
            }
            else
            {
                startDegree = 180;
                plusDegree = 0;
                accelaration = 4;

                canMoveStraight = false;
            }
        }
    }

}
