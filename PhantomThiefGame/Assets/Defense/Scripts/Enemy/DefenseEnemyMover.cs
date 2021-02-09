using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyMover : MonoBehaviour
{
    [SerializeField] public float enemySpeed;
    public Rigidbody rb;
    private Transform enemyTrans;
    [SerializeField] private Transform[] moveTrans;
    private bool isRight = false;
    private bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        enemyTrans = GetComponent<Transform>();

        isRight = false;
        isLeft = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (isRight)
        {
            rb.velocity = new Vector3(enemySpeed, 0, 0);
        }
        else if (isLeft)
        {
            rb.velocity = new Vector3(-enemySpeed, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, -enemySpeed * 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject MoveStraight = other.gameObject;
        if (MoveStraight.tag == "MoveStraight")
        {

            if (enemyTrans.position.x >= 0)
            {
                enemyTrans.rotation = Quaternion.Euler(0, 180, 0);
                isLeft = true;
            }
            else
            {
                enemyTrans.rotation = Quaternion.Euler(0, 0, 0);
                isRight = true;
            }
        }
    }

}
