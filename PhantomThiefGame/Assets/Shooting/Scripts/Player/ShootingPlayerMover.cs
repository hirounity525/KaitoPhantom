using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerMover : MonoBehaviour
{
    [SerializeField] private ShootingInputProvider inputProvider;
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;

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
