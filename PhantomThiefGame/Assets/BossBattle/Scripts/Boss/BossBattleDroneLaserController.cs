using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleDroneLaserController : MonoBehaviour
{
    public Vector3 droneLaserVec;
    [SerializeField] private float laserSpeed;
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

    public void MoveDroneLaser()
    {
        rb.velocity = droneLaserVec * laserSpeed;
    }
}
