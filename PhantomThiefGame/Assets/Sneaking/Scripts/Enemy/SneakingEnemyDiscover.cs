using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingEnemyDiscover : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private float turnTime;

    private SneakingEnemyCore enemyCore;
    private Transform enemyTrans;


    private void Awake()
    {
        enemyCore = GetComponent<SneakingEnemyCore>();
        enemyTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!enemyCore.isDiscovery)
        {
            flashLight.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enemyCore.isDiscovery)
        {
            GameObject collisionObj = other.gameObject;

            if (collisionObj.tag == "Player")
            {
                enemyCore.isDiscovery = true;
                flashLight.color = Color.red;

                Vector3 lookDir = collisionObj.transform.position - enemyTrans.position;
                Quaternion lookQuaternion = Quaternion.LookRotation(lookDir);
                lookQuaternion = Quaternion.Euler(0, lookQuaternion.eulerAngles.y, 0);
                enemyTrans.DORotateQuaternion(lookQuaternion, turnTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enemyCore.isDiscovery)
        {
            GameObject collisionObj = collision.gameObject;

            if (collisionObj.tag == "Player")
            {
                enemyCore.isDiscovery = true;
                flashLight.color = Color.red;

                Vector3 lookDir = collisionObj.transform.position - enemyTrans.position;
                Quaternion lookQuaternion = Quaternion.LookRotation(lookDir);
                lookQuaternion = Quaternion.Euler(0, lookQuaternion.eulerAngles.y, 0);
                enemyTrans.DORotateQuaternion(lookQuaternion, turnTime);
            }
        }
    }
}
