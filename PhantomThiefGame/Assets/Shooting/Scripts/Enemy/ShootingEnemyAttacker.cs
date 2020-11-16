using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAttacker : MonoBehaviour
{
    [SerializeField] private Transform gunNozzlePos;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float attackWaitTime;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int attackNum;

    private ShootingAttackRange attackRange;
    private bool isShoot = false;
    private bool rapidFire = true;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        attackRange = GetComponent<ShootingAttackRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShoot)//攻撃速度
        {
            if (attackRange.isAttack == true)//攻撃範囲にはいったら
            {
                for (i = 0;i < attackNum;i++ )//攻撃回数
                {
                    if (rapidFire)
                    {
                        GameObject bullet = bulletPool.GetObject();
                        bullet.transform.position = gunNozzlePos.position;
                        StartCoroutine(AttackSpeed());
                    }
                }
                StartCoroutine(AttackWaitTime());
            }
        }

    }

    private IEnumerator AttackWaitTime()//攻撃頻度
    {
        isShoot = true;

        yield return new WaitForSeconds(attackWaitTime);

        isShoot = false;
    }

    private IEnumerator AttackSpeed()
    {
        rapidFire = false;

        yield return new WaitForSeconds(attackSpeed);

        rapidFire = true;
    }
}
