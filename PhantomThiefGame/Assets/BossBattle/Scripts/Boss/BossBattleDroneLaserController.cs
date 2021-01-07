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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        this.transform.LookAt(playerTrans);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        disappearTimeTemp += Time.fixedDeltaTime;
        if(disappearTimeTemp > disappearTime)
        {
            disappearTimeTemp = 0;
            gameObject.SetActive(false);
        }
    }

    public void MoveDroneLaser()
    {
        rb.velocity = droneLaserVec * laserSpeed;
    }
}
