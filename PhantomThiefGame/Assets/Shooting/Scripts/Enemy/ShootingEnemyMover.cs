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

    [SerializeField] private float resetRadian;

    private Rigidbody rb;

    private float countTime;
    private float nowReturnMoveTime;

    private bool canMoveStraight=false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nowReturnMoveTime = returnMoveTime;

        this.transform.position = new Vector3(moveRadius*Mathf.Cos(startRadian),0, -moveRadius * Mathf.Sin(startRadian));



    }

    // Update is called once per frame
    void Update()
    {
        if (!canMoveStraight)
        {
            rb.velocity = new Vector3(-enemySpeed * moveRadius * Mathf.Sin(enemySpeed * resetRadian + startRadian), 0, -enemySpeed * moveRadius * Mathf.Cos(enemySpeed * resetRadian + startRadian));
        }
        else
        {
            rb.velocity = new Vector3(-moveStraightSpeed, 0, 0);
        }


        resetRadian = resetRadian+Time.deltaTime;
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
            }
            else
            {
                canMoveStraight = false;
                resetRadian = 10;
            }
        }
    }

}
