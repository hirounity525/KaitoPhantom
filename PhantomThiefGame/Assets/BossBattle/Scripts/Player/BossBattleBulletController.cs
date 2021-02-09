using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBulletController : MonoBehaviour
{
    public BossBattlePlayerMover playerMover;
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;
    private Transform bulletTrans;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletTrans = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameObject.SetActive(false);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            gameObject.SetActive(false);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            gameObject.SetActive(false);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            gameObject.SetActive(false);
        }
    }


    public void MoveBullet()
    {/*
        if (playerMover.playerDirectionIsRight)
        {
            rb.velocity = bulletTrans.right * bulletSpeed;
        }
        else if (!playerMover.playerDirectionIsRight)
        {
            rb.velocity = bulletTrans.right * -bulletSpeed;
        }
        */
        rb.velocity = bulletTrans.up * -bulletSpeed;
    }
}
