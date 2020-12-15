using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerMover : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private RunInputProvider inputProvider;
    [SerializeField] private float rotationAngle;

    private Rigidbody rb;
    private Transform playerTrans;

    private bool isJump=true;
    private bool canSliding=true;
    [SerializeField] private int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isJump)
        {
            if (inputProvider.isJumpButtunDown)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
                jumpCount++;
                canSliding = false;

                if (jumpCount >= 2)
                {
                    isJump = false;
                }
               
            }
        }

        if (inputProvider.isSlidingButtunDown > 0 && canSliding)
        {
            playerTrans.rotation =Quaternion.Euler(0,0,rotationAngle);
        }
        else
        {
            playerTrans.rotation = new Quaternion(0, 0, 0, 0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject groundObj = collision.gameObject;
        if (groundObj.tag == "Ground")
        {
            canSliding = true;
            isJump = true;
            jumpCount = 0;
        }
    }
}
