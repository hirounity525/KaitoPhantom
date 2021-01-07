using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneBulletController : MonoBehaviour
{
    public int one = 1;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float disappearTime;
    private float disappearTimeTemp;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    public void MoveDroneBullet()
    {
        rb.velocity = new Vector3(one, 0, 0) * bulletSpeed;
    }

    
}
