using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBulletController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private float bulletSpeed;
    private Transform bulletTrans;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveBullet()
    {
        rb.velocity = bulletTrans.right * bulletSpeed;
    }
}

