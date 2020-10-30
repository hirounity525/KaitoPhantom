using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyMover : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    private Rigidbody rb;
    private Transform enemyTrans;
    [SerializeField] private Transform[] moveTrans;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = enemyTrans.right * enemySpeed;
    }
}
