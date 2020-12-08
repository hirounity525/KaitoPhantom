﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBossAttacker : MonoBehaviour
{
    //[SerializeField] private GameObject guardPrefab;
    [SerializeField] private float summonGuardsXposMax;
    [SerializeField] private float summonGuardsXposMin;
    [SerializeField] private float summonGuardsYpos;

    //[SerializeField] private GameObject rockPrefabs;
    [SerializeField] private float summonRockXposMax;
    [SerializeField] private float summonRockXposMin;
    [SerializeField] private float summonRockYpos;

    [SerializeField] private ObjectPool guardsPool;

    [SerializeField] private ObjectPool rocksPool;

    [SerializeField] private ObjectPool bossBulletsPool;
    [SerializeField] private Transform bossGunNozzlePosTrans;
    [SerializeField] private Transform playerTrans;
    public Vector3 bossBulletVec2;

    private Rigidbody rb;
    private bool isBossPosRight = true;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxLeftBossPos;
    [SerializeField] private float maxRightBossPos;
    [SerializeField] private Vector3 bossJumpPowerToLeft;
    [SerializeField] private Vector3 bossJumpPowerToRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*SummonGuards();
        SummonGuards();
        SummonGuards();
        SummonGuards();
        SummonRock();
        SummonRock();
        SummonRock();
        SummonRock();
        GunAttack();*/
        //BossMove();
        //BossJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < maxLeftBossPos)
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.localEulerAngles = new Vector3(0,180,0);
            isBossPosRight = false;
        }

        else if (gameObject.transform.position.x > maxRightBossPos)
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.localEulerAngles = new Vector3(0,0,0);
            isBossPosRight = true;
        }
    }

    public void SummonGuards()
    {
        //Instantiate(guardPrefab, new Vector3(Random.Range(summonGuardsXposMin,summonGuardsXposMax), summonGuardsYpos, 0), Quaternion.identity);
        GameObject guard = guardsPool.GetObject();
        guard.transform.position = new Vector3(Random.Range(summonGuardsXposMin, summonGuardsXposMax), summonGuardsYpos, 0);
        guard.transform.rotation = Quaternion.identity;
    }

    public void SummonRock()
    {
        //Instantiate(rockPrefabs, new Vector3(Random.Range(summonRockXposMin, summonRockXposMax), summonRockYpos, 0), Quaternion.identity);
        GameObject rock = rocksPool.GetObject();
        rock.transform.position = new Vector3(Random.Range(summonRockXposMin, summonRockXposMax), summonRockYpos, 0);
        rock.transform.rotation = Quaternion.Euler(Random.Range(0,90), Random.Range(0, 90), Random.Range(0, 90));
    }

    public void GunAttack()
    {
        bossBulletVec2 = playerTrans.position - gameObject.transform.position;
        GameObject bossBullet = bossBulletsPool.GetObject();
        bossBullet.transform.position = bossGunNozzlePosTrans.position;
        bossBullet.transform.rotation = bossGunNozzlePosTrans.rotation;
        bossBullet.GetComponent<BossBattleBossBulletCore>().bossBalletVec = bossBulletVec2.normalized;
        bossBullet.GetComponent<BossBattleBossBulletCore>().MoveBossBullet();
    }

    public void BossMove()
    {
        if (isBossPosRight)
        {
            rb.velocity = new Vector3(-1, 0, 0) * moveSpeed;
            /*if(gameObject.transform.position.x < maxLeftBossPos)
            {
                rb.velocity = Vector3.zero;
                isBossPosRight = false;
            }*/
        }

        else if (!isBossPosRight)
        {
            rb.velocity = new Vector3(1, 0, 0) * moveSpeed;
            /*if(gameObject.transform.position.x > maxRightBossPos)
            {
                rb.velocity = Vector3.zero;
                isBossPosRight = true;
            }*/
        }
    }

    public void BossJump()
    {
        if (isBossPosRight)
        {
            rb.AddForce(bossJumpPowerToLeft);
        }

        else if (!isBossPosRight)
        {
            rb.AddForce(bossJumpPowerToRight);
        }
    }
}
