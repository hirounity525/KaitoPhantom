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
    [SerializeField] private BossBattleBossAttacker bossBattleBossAttacker;
    private float chaseTimeTemp;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isChase)
        {
            chaseTimeTemp = Time.fixedDeltaTime;
            if(chaseTime < chaseTimeTemp)
            {
                this.transform.LookAt(playerTrans);
                rb.AddForce(transform.forward * missileSpeed);
                //rb.AddForce(droneMissileVec * missileSpeed);
                //rb.velocity = droneMissileVec * missileSpeed;
            }

            else if (chaseTime > chaseTimeTemp)
            {
                chaseTimeTemp = 0;
                isChase = false;
                bossBattleBossAttacker.isMissileChase = false;
            }
        }
    }

}
