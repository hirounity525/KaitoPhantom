using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerMover : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private RunInputProvider inputProvider;

    private Rigidbody rb;

    private bool isJump=true;
    [SerializeField] private int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isJump)
        {
            if (inputProvider.isJumpButtunDown)
            {
                rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
                jumpCount++;

                if (jumpCount >= 2)
                {
                    isJump = false;
                }
               
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject groundObj = collision.gameObject;
        if (groundObj.tag == "Ground")
        {
            isJump = true;
            jumpCount = 0;
        }
    }
}
