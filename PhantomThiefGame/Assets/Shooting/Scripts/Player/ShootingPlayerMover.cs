using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;

    [SerializeField] private ShootingInputProvider inputProvider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(inputProvider.moveHorizon, inputProvider.moveVertical, 0) * moveSpeed;
    }
}
