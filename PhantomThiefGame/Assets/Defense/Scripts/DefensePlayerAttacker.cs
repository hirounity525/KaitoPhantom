using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerAttacker : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private DefenseInputProvider inputProvider;
    [SerializeField]private Transform gunNozzlePosTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputProvider.isAttackButtonDown) 
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = gunNozzlePosTrans.position;
            bullet.transform.rotation = gunNozzlePosTrans.rotation;
            bullet.GetComponent<DefenseBulletController>().MoveBullet();
        }
    }
}
