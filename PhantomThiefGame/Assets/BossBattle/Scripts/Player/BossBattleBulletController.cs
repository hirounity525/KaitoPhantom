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
        if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {

        }
    }

    public void MoveBullet()
    {
        if (playerMover.playerDirectionIsRight)
        {
            rb.velocity = bulletTrans.right * bulletSpeed;
        }
        else if (!playerMover.playerDirectionIsRight)
        {
            rb.velocity = bulletTrans.right * -bulletSpeed;
        }
    }
}
