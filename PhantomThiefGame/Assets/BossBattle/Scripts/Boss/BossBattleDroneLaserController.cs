using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneLaserController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 droneLaserVec;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float disappearTime;
    private float disappearTimeTemp;
    private Rigidbody rb;
    private SEPlayer sEPlayer;
    private bool isOne;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sEPlayer = GetComponent<SEPlayer>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOne)
        {
            sEPlayer.Play("DroneLaser");
            isOne = true;
        }
    }

    private void FixedUpdate()
    {
        disappearTimeTemp += Time.fixedDeltaTime;
        if(disappearTimeTemp > disappearTime)
        {
            disappearTimeTemp = 0;
            gameObject.SetActive(false);
            isOne = false;
        }
    }

    public void MoveDroneLaser()
    {
        rb.velocity = droneLaserVec * laserSpeed;
    }
}
