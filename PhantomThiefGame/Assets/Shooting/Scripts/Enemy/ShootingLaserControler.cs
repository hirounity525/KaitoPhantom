using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLaserControler : MonoBehaviour
{
    public Transform playerTrans;
    [SerializeField]private float laserSpeed;

    private Rigidbody rb;
    private bool chasePlayer=true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePlayer == true)
        {
            this.transform.LookAt(playerTrans);
            chasePlayer = false;
        }

        rb.velocity = transform.forward * laserSpeed;//弾の速度
    }
}
