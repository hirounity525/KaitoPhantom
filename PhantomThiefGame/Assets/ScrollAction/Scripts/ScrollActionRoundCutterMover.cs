using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionRoundCutterMover : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")] private float moveSpeed;
    [SerializeField, Tooltip("左へどこまで移動するか")] private float moveReachL;
    [SerializeField, Tooltip("右へどこまで移動するか")] private float moveReachR;
    [SerializeField, Tooltip("右から始めるか、左から始めるか")] private bool startsRight;

    private Transform enemyTrans;
    private Rigidbody rb;

    private float firstPosX;
    private float moveLimitPointL;
    private float moveLimitPointR;

    private bool isMoveRight;

    // Start is called before the first frame update
    void Start()
    {
        enemyTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        isMoveRight = startsRight;

        firstPosX = enemyTrans.position.x;
        moveLimitPointL = firstPosX - moveReachL;
        moveLimitPointR = firstPosX + moveReachR;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveRight)
        {
            if (enemyTrans.position.x >= moveLimitPointR)
            {
                isMoveRight = false;
            }
        }
        else
        {
            if (enemyTrans.position.x <= moveLimitPointL)
            {
                isMoveRight = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoveRight)
        {
            rb.velocity = Vector3.right * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.left * moveSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * moveReachL));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * moveReachR));
    }
}