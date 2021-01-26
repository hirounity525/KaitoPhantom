using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiroPendulumMover : MonoBehaviour
{
    [SerializeField] private Transform rotationAxisTrans;
    [SerializeField] private float halfCycleTime;
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private bool startsRightRotation;

    private Rigidbody rb;

    private bool isRotationPositive;

    private float maxRotationRad;
    private float cycleTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rotationAxisTrans.position;

        maxRotationRad = (maxRotationAngle / 180) * Mathf.PI;

        isRotationPositive = startsRightRotation;

        if (isRotationPositive)
        {
            rb.angularVelocity = Vector3.forward * maxRotationRad;
        }
        else
        {
            rb.angularVelocity = -(Vector3.forward * maxRotationRad);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isRotationPositive)
        {
            if(rb.angularVelocity.z > -maxRotationRad)
            {
                rb.angularVelocity -= (maxRotationRad / halfCycleTime) * Vector3.forward * Time.fixedDeltaTime;
            }
            else
            {
                isRotationPositive = false;
            }
        }
        else
        {
            if (rb.angularVelocity.z < maxRotationRad)
            {
                rb.angularVelocity += (maxRotationRad / halfCycleTime) * Vector3.forward * Time.fixedDeltaTime;
            }
            else
            {
                isRotationPositive = true;
            }
        }
    }
}
