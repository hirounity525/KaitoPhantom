using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerAttacker : MonoBehaviour
{
    [SerializeField]private ShootingInputProvider inputProvider;
    [SerializeField]private ObjectPool bulletPool;
    [SerializeField] private Transform gunNozzlePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputProvider.isAttackButtonDown == true)
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = gunNozzlePos.position;
        }
    }
}
