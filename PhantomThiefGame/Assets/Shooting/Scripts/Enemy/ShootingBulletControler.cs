using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBulletControler : MonoBehaviour
{
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
        rb.velocity = new Vector3(bulletSpeed, 0, 0);//弾の速さ
    }
}
