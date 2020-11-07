using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPendulumMover2 : MonoBehaviour
{
    [SerializeField] private float maxAngularVelocity;
    [SerializeField] private float rotateAcceleration;
    private Rigidbody rb;
    private float angularVelocity;
    private bool isRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if(angularVelocity >= maxAngularVelocity)
        {
            isRight = false;
        }
        else if(angularVelocity <= -maxAngularVelocity)
        {
            isRight = true;
        }

        if (isRight)
        {
            angularVelocity += rotateAcceleration * Time.fixedDeltaTime;
        }
        else
        {
            angularVelocity -= rotateAcceleration * Time.fixedDeltaTime;
        }

        rb.angularVelocity = Vector3.forward * angularVelocity;

    }
}//角度
//最大速度
//秒数
//これらを変えるだけで実装する
