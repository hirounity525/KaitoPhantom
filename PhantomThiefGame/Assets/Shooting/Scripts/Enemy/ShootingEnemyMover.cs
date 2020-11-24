using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMover : MonoBehaviour
{
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
        rb.velocity = new Vector3(-1 * enemySpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        countTime = Time.time;
        if (countTime >= nowReturnMoveTime)
        {
            rb.velocity = rb.velocity * -1;
            nowReturnMoveTime += returnMoveTime;
        }
    }
}
