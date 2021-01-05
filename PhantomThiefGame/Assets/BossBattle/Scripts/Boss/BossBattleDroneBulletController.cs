using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneBulletController : MonoBehaviour
{
    public int one = 1;
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveDroneBullet()
    {
        rb.velocity = new Vector3(one, 0, 0) * bulletSpeed;
    }
}
