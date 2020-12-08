using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObjectTuner : MonoBehaviour
{
    [SerializeField] private float tuneSpeed;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, tuneSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
