using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SneakingEnemyDiscover : MonoBehaviour
{
    public bool isLightOn = true;

    [SerializeField] private Light flashLight;
    [SerializeField] private float turnTime;

    [SerializeField] private Transform centerTrans;
    [SerializeField] private Vector3 halfExtents;
    [SerializeField] private float maxDistance;

    private SneakingEnemyCore enemyCore;
    private Transform enemyTrans;

    private RaycastHit raycastHit;

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
        else
        {
            flashLight.color = Color.red;
        }
    }

    private void FixedUpdate()
    {
        if (!enemyCore.isDiscovery)
        {
            if (isLightOn)
            {
                Physics.BoxCast(centerTrans.position, halfExtents, enemyTrans.forward, out raycastHit, Quaternion.identity, maxDistance);

                if (raycastHit.transform != null)
                {
                    if (raycastHit.transform.gameObject.tag == "Player")
                    {
                        enemyCore.isDiscovery = true;

                        Vector3 lookDir = raycastHit.transform.position - enemyTrans.position;
                        Quaternion lookQuaternion = Quaternion.LookRotation(lookDir);
                        lookQuaternion = Quaternion.Euler(0, lookQuaternion.eulerAngles.y, 0);
                        enemyTrans.DORotateQuaternion(lookQuaternion, turnTime);
                    }
                }
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (!enemyCore.isDiscovery)
        {
            GameObject collisionObj = collision.gameObject;

            if (collisionObj.tag == "Player")
            {
                enemyCore.isDiscovery = true;

                Vector3 lookDir = collisionObj.transform.position - enemyTrans.position;
                Quaternion lookQuaternion = Quaternion.LookRotation(lookDir);
                lookQuaternion = Quaternion.Euler(0, lookQuaternion.eulerAngles.y, 0);
                enemyTrans.DORotateQuaternion(lookQuaternion, turnTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(centerTrans.position + transform.forward * maxDistance, halfExtents);
    }
}
