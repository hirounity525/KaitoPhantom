using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMissileControler : MonoBehaviour
{
    public Transform playerTrans;
    [SerializeField] private float missileSpeed;
    [SerializeField] private float chaseTime;

    private Rigidbody rb;
    private bool chasePlayer=true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer)
        {
            this.transform.LookAt(playerTrans);
            rb.AddForce(transform.forward * missileSpeed);
            StartCoroutine(ChaseTime());
        }

    }

    private IEnumerator ChaseTime()//追従時間
    {
        yield return new WaitForSeconds(chaseTime);

        chasePlayer = false;
    }
}
