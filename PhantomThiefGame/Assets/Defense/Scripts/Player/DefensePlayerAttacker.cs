using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerAttacker : MonoBehaviour
{
    [SerializeField] private SEPlayer sePlayer;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private DefenseInputProvider inputProvider;
    [SerializeField]private Transform gunNozzlePosTrans;
    [SerializeField] private float shootWaitTime;
    public bool isShoot;
    private DefensePlayerCore playerCore;
    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<DefensePlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShoot)
        {
            if (inputProvider.isAttackButtonDown)
            {
                GameObject bullet = bulletPool.GetObject();
                bullet.transform.position = gunNozzlePosTrans.position;
                bullet.transform.rotation = gunNozzlePosTrans.rotation;
                bullet.GetComponent<DefenseBulletController>().MoveBullet();
                StartCoroutine(StartMove());
            }
        }

        playerCore.isShoot = isShoot;
    }
    private IEnumerator StartMove()
    {
        sePlayer.Play("GunPlay");

        isShoot = true;
        
        yield return new WaitForSeconds(shootWaitTime);

        isShoot = false;
    }
}
