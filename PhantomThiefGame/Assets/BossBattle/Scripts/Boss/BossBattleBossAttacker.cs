using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [SerializeField] private ObjectPool bigBeamPool;

    [SerializeField] private ObjectPool droneBulletPool;
    [SerializeField] private ObjectPool droneLaserPool;
    [SerializeField] private ObjectPool droneMissilePool;
    [SerializeField] private Transform bossDroneNozzlePosTrans;
    [SerializeField] private float bulletIntervalTime;
    [SerializeField] private int bulletShotNum;
    private int bulletShotNumTemp = 0;
    private Vector3 droneLaserVec2;
    //private Vector3 droneMissileVec2;
    public bool isMissileChase;
    [SerializeField] private float missileIntervalTime;
    [SerializeField] private int missileShotNum;
    private int missileShotNumTemp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        if (isMissileChase)
        {
            GameObject droneMissile = droneMissilePool.GetObject();
            droneMissile.GetComponent<BossBattleDroneMissileController>().playerTrans = playerTrans;
        } 
    }

    public void SummonGuards()
    {
        //Instantiate(guardPrefab, new Vector3(Random.Range(summonGuardsXposMin,summonGuardsXposMax), summonGuardsYpos, 0), Quaternion.identity);
        GameObject guard = guardsPool.GetObject();
        guard.transform.position = new Vector3(Random.Range(summonGuardsXposMin, summonGuardsXposMax), summonGuardsYpos, 0);
        guard.transform.rotation = Quaternion.Euler(0,90,0);
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

    public void BigBeam()
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

    public void DroneAttack()
    {
        if(Random.Range(0,3) == 0)//弾を前方に飛ばす
        {
            GameObject droneBullet = droneBulletPool.GetObject();
            droneBullet.transform.position = bossDroneNozzlePosTrans.position;
            droneBullet.transform.rotation = bossDroneNozzlePosTrans.rotation;
            if(isBossPosRight)//ボスが画面の右側にいるならば
            {
                droneBullet.GetComponent<BossBattleDroneBulletController>().one = -1;
            }

            else if (!isBossPosRight)//ボスが画面の左側にいるならば
            {
                droneBullet.GetComponent<BossBattleDroneBulletController>().one = 1;
            }
            droneBullet.GetComponent<BossBattleDroneBulletController>().MoveDroneBullet();
            StartCoroutine(DroneBulletIntervalTime());
        }

        else if (Random.Range(0, 3) == 1)//プレイヤーを直接狙うレーザーを発射する
        {
            droneLaserVec2 = playerTrans.position - bossDroneNozzlePosTrans.position;
            GameObject droneLaser = droneLaserPool.GetObject();
            droneLaserPool.transform.position = bossDroneNozzlePosTrans.position;
            droneLaserPool.transform.rotation = bossDroneNozzlePosTrans.rotation;
            droneLaser.GetComponent<BossBattleDroneLaserController>().droneLaserVec = droneLaserVec2.normalized;
            droneLaser.GetComponent<BossBattleDroneLaserController>().MoveDroneLaser();
        }

        else if (Random.Range(0, 3) == 2)//追尾するミサイルを発射する
        {
            //droneMissileVec2 = 
            isMissileChase = true;
            GameObject droneMissile = droneMissilePool.GetObject();
            droneMissilePool.transform.position = bossDroneNozzlePosTrans.position;
            droneMissilePool.transform.rotation = bossDroneNozzlePosTrans.rotation;
            //droneMissile.GetComponent<BossBattleDroneMissileController>().droneMissileVec = droneMissileVec2.normalized;
            droneMissile.GetComponent<BossBattleDroneMissileController>().playerTrans = playerTrans;
            droneMissile.GetComponent<BossBattleDroneMissileController>().isChase = true;
            StartCoroutine(DroneMissileIntervalTime());
        }
    }

    private IEnumerator DroneBulletIntervalTime()//弾の撃つ間隔
    {
        bulletShotNumTemp++;
        if (bulletShotNumTemp < bulletShotNum)
        {
            yield return new WaitForSeconds(bulletIntervalTime);

            GameObject droneBullet = droneBulletPool.GetObject();
            droneBullet.transform.position = bossDroneNozzlePosTrans.position;
            droneBullet.transform.rotation = bossDroneNozzlePosTrans.rotation;
            if (isBossPosRight)//ボスが画面の右側にいるならば
            {
                droneBullet.GetComponent<BossBattleDroneBulletController>().one = -1;
            }

            else if (!isBossPosRight)//ボスが画面の左側にいるならば
            {
                droneBullet.GetComponent<BossBattleDroneBulletController>().one = 1;
            }
            droneBullet.GetComponent<BossBattleDroneBulletController>().MoveDroneBullet();
            StartCoroutine(DroneBulletIntervalTime());
        }
        else
        {
            bulletShotNum = 0;
        }
    }

    private IEnumerator DroneMissileIntervalTime()//弾の撃つ間隔
    {
        missileShotNumTemp++;
        if(missileShotNumTemp < missileShotNum)
        {
            yield return new WaitForSeconds(missileIntervalTime);

            isMissileChase = true;
            GameObject droneMissile = droneMissilePool.GetObject();
            droneMissilePool.transform.position = bossDroneNozzlePosTrans.position;
            droneMissilePool.transform.rotation = bossDroneNozzlePosTrans.rotation;
            //droneMissile.GetComponent<BossBattleDroneMissileController>().droneMissileVec = droneMissileVec2.normalized;
            droneMissile.GetComponent<BossBattleDroneMissileController>().playerTrans = playerTrans;
            droneMissile.GetComponent<BossBattleDroneMissileController>().isChase = true;
            StartCoroutine(DroneMissileIntervalTime());
        }

        else
        {
            missileShotNumTemp = 0;
        }
    }
}
