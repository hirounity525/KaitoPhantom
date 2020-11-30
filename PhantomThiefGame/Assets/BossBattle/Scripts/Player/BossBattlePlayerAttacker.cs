using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattlePlayerAttacker : MonoBehaviour
{
    [SerializeField] private BossBattleInputProvider inputProvider2;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform gunNozzlePosTrans;
    [SerializeField] private float shootRate;
    private float shootRateTimeTemp;
    private bool isShoot;
    private BossBattlePlayerMover playerMover;

    // Start is called before the first frame update
    void Start()
    {
        playerMover = GetComponent<BossBattlePlayerMover>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inputProvider2.isAttackButtonDown > 0)
        {

            if (shootRateTimeTemp >= shootRate)
            {
                shootRateTimeTemp = 0;
                isShoot = false;
            }

            if (!isShoot)
            {
                ShootAttack();
                isShoot = true;
                
            }
            shootRateTimeTemp += Time.deltaTime;
        }
        
        else
        {
            shootRateTimeTemp = 0;
            isShoot = false;
        }
    }

    void ShootAttack()
    {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = gunNozzlePosTrans.position;
        bullet.transform.rotation = gunNozzlePosTrans.rotation;
        bullet.GetComponent<BossBattleBulletController>().playerMover = playerMover;
        bullet.GetComponent<BossBattleBulletController>().MoveBullet();
    }
}
