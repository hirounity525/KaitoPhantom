using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemyControler : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    private Rigidbody rb;
    private Transform enemyTrans;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = enemyTrans.right * enemySpeed;
    }
}
