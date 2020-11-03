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
    void OnCollisionEnter(Collision collision)
    {
        GameObject enemyObj=collision.gameObject;
        if (enemyObj.tag == "Enemy")
        {
            enemyObj.GetComponent<DefenseEnemyHPControler>().AddDamage();
            gameObject.SetActive(false);
        }
    }

    public void MoveBullet()
    {
        rb.velocity = bulletTrans.right * bulletSpeed;
    }
}

