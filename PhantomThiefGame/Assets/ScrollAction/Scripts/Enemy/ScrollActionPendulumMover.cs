using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPendulumMover : MonoBehaviour
{
    [SerializeField] private float firstSpeed;
    [SerializeField] private Transform rotationCenterTrans;
    [SerializeField] private SEPlayer sEPlayer;
    private Rigidbody rb;
    private bool isFirstAngle0 = true;
    private Vector3 addSpeed;
    private bool isLeft = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.velocity = new Vector3(firstSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float world_angle_z = rotationCenterTrans.eulerAngles.z;

        if (isFirstAngle0)
        {
            if(world_angle_z <= 5)
            {
                addSpeed = rb.velocity;
                isFirstAngle0 = false;
            }
        }

        if(isLeft)
        {
            if(world_angle_z <= 5)
            {
                sEPlayer.Play("Pendulum");
                rb.velocity = addSpeed;
                isLeft = false;
            }
        }
        else
        {
            if (world_angle_z >= 355)
            {
                sEPlayer.Play("Pendulum");
                rb.velocity = -addSpeed;
                isLeft = true;
            }
        }
    }
}
