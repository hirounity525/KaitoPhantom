using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunObjectMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private RunPlayerCore playerCore;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(-moveSpeed, 0, 0);

        if (playerCore.isDead)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

    }

}
