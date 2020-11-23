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
    void Update()
    {
        if (inputProvider.moveHorizon > 0 )
        {
            rb.velocity = new Vector3(moveSpeed, 0, 0);
        }
        else if (inputProvider.moveHorizon < 0)
        {
            rb.velocity = new Vector3(-moveSpeed,0,0);
        }
        else if (inputProvider.moveVertical > 0)
        {
            rb.velocity = new Vector3(0, moveSpeed, 0);
        }
        else if (inputProvider.moveVertical < 0)
        {
            rb.velocity = new Vector3(moveSpeed,0, 0);
        }
    }
}
