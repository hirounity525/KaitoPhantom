using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMover : MonoBehaviour
{
    [SerializeField] private float positionX;
    [SerializeField] private float positionZ;
    [SerializeField] private float moveRadius;
   [SerializeField] private float enemySpeed;
   [SerializeField] private float returnMoveTime;

    private Rigidbody rb;

    private float countTime;
    private float nowReturnMoveTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nowReturnMoveTime = returnMoveTime;

        this.transform.position = new Vector3(moveRadius*Mathf.Sin(positionX),0, moveRadius * Mathf.Sin(positionZ));



    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(-enemySpeed * moveRadius * Mathf.Sin(enemySpeed * Time.time + positionX), 0, -enemySpeed * moveRadius * Mathf.Cos(enemySpeed * Time.time + positionX));

        countTime = Time.time;
        if (countTime >= nowReturnMoveTime)
        {
            rb.velocity = rb.velocity * -1;
            nowReturnMoveTime += returnMoveTime;
        }
    }
}
