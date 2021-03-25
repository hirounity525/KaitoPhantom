using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerAttacker : MonoBehaviour
{
    [SerializeField] private SEPlayer sePlayer;
    [SerializeField] private ShootingInputProvider inputProvider;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Transform gunNozzlePos;

    [SerializeField] private float attackWaitTime;

    private bool isAttack=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack)
        {
            if (inputProvider.isAttackButtunDown > 0)
            {
                GameObject bullet = bulletPool.GetObject();
                bullet.transform.position = gunNozzlePos.position;
                StartCoroutine(AttackWaitTime());
            }
        }

    }

    private IEnumerator AttackWaitTime()
    {
        sePlayer.Play("GunPlay");

        isAttack = true;

        yield return new WaitForSeconds(attackWaitTime);

        isAttack = false;
    }
}
