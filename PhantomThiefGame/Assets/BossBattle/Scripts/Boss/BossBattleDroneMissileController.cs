using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneMissileController : MonoBehaviour
{
    public Transform playerTrans;
    //public Vector3 droneMissileVec;
    public bool isChase;
    [SerializeField] private float chaseTime;
    [SerializeField] private float missileSpeed;
    //[SerializeField] private BossBattleBossAttacker bossBattleBossAttacker;
    [SerializeField] private float disappearTime;
    private float disappearTimeTemp;
    private float chaseTimeTemp;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        disappearTimeTemp += Time.fixedDeltaTime;
        if (disappearTimeTemp > disappearTime)
        {
            disappearTimeTemp = 0;
            gameObject.SetActive(false);
        }

        if (isChase)
        {
            chaseTimeTemp += Time.fixedDeltaTime;
            if(chaseTimeTemp < chaseTime)
            {
                this.transform.LookAt(playerTrans);
                rb.AddForce(transform.forward * missileSpeed);
                //rb.AddForce(droneMissileVec * missileSpeed);
                //rb.velocity = droneMissileVec * missileSpeed;
            }

            else if (chaseTimeTemp > chaseTime)
            {
                chaseTimeTemp = 0;
                isChase = false;
                //bossBattleBossAttacker.isMissileChase = false;
            }
        }
    }

}
