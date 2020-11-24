using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPendulumRotationCenterPosition : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
