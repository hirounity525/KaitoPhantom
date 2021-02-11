using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossBattleDroneMissileController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 droneMissileVec;
    public bool isChase;
    [SerializeField] private float chaseTime;
    [SerializeField] private float missileSpeed;
    [SerializeField] private float limitSpeed;
    //[SerializeField] private BossBattleBossAttacker bossBattleBossAttacker;
    [SerializeField] private float disappearTime;
    private float disappearTimeTemp;
    private float chaseTimeTemp;
    private Rigidbody rb;
    private SEPlayer sEPlayer;
    private bool isOne;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sEPlayer = GetComponent<SEPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOne)
        {
            sEPlayer.Play("DroneMissile");
            isOne = true;
        }
    }

    private void FixedUpdate()
    {

        disappearTimeTemp += Time.fixedDeltaTime;
        if (disappearTimeTemp > disappearTime)
        {
            disappearTimeTemp = 0;
            gameObject.SetActive(false);
            isOne = false;
        }

        if (isChase)
        {
            chaseTimeTemp += Time.fixedDeltaTime;
            if(chaseTimeTemp < chaseTime)
            {
                this.transform.LookAt(playerTrans);
                rb.AddForce(transform.forward * missileSpeed);
                float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed);
                float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);
                rb.velocity = new Vector3(speedXTemp, speedYTemp);
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

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            disappearTimeTemp = 0;
            chaseTimeTemp = 0;
            gameObject.SetActive(false);
            isOne = false;
        }
    }
}
