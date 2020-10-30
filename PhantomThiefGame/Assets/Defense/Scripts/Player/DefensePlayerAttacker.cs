using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerAttacker : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private DefenseInputProvider inputProvider;
    [SerializeField]private Transform gunNozzlePosTrans;
    [SerializeField] private float shootWaitTime;
    private bool canShoot=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot)
        if (inputProvider.isAttackButtonDown) 
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = gunNozzlePosTrans.position;
            bullet.transform.rotation = gunNozzlePosTrans.rotation;
            bullet.GetComponent<DefenseBulletController>().MoveBullet();
                StartCoroutine(StartMove());
            }
    }
    private IEnumerator StartMove()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootWaitTime);

        canShoot = true;
    }
}
