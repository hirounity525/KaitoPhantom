using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMissileControler : MonoBehaviour
{
    public Transform playerTrans;

    [SerializeField] private float missilePower;
    [SerializeField] private float limitMoveSpeed;
    [SerializeField] private float chaseTime;

    private Rigidbody rb;
    private Transform bulletTrans;

    private bool chasePlayer = true;

    private bool isFirstPlayCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer)
        {
            bulletTrans.LookAt(playerTrans);

            if (!isFirstPlayCoroutine)
            {
                StartCoroutine(ChaseTime());
                isFirstPlayCoroutine = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (chasePlayer)
        {
            rb.AddForce(transform.forward * missilePower);

            float moveSpeedXTemp = Mathf.Clamp(rb.velocity.x, -limitMoveSpeed, limitMoveSpeed);
            float moveSpeedYTemp = Mathf.Clamp(rb.velocity.y, -limitMoveSpeed, limitMoveSpeed);

            rb.velocity = new Vector3(moveSpeedXTemp, moveSpeedYTemp);
        }
    }

    private IEnumerator ChaseTime()//追従時間
    {
        yield return new WaitForSeconds(chaseTime);

        chasePlayer = false;
    }
}
