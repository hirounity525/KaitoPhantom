using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerMover : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private RunInputProvider inputProvider;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float slidingTime;

    private Rigidbody rb;
    private Transform playerTrans;
    private RunPlayerCore playerCore;

    private bool canJump = true;
    private bool canSliding = true;
    private int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<RunPlayerCore>();
        rb = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canJump)
        {
            if (inputProvider.isJumpButtunDown)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

                jumpCount++;

                canSliding = false;
                playerCore.firstJump = true;

                if (jumpCount >= 2)
                {
                    canJump = false;
                    playerCore.secondJump = true;
                }
               
            }
        }

        if ((inputProvider.isSlidingButtunDown > 0 && canSliding)&&!playerCore.isSliding)
        {


            StartCoroutine(StartSliding());


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject groundObj = collision.gameObject;
        if (groundObj.tag == "Ground")
        {
            canSliding = true;
            canJump = true;
            playerCore.firstJump = false;
            playerCore.secondJump = false;

            jumpCount = 0;
        }
    }

    private IEnumerator StartSliding()
    {
        playerCore.isSliding = true;

        canJump = false;

        playerTrans.rotation = Quaternion.Euler(0, 0, rotationAngle);

        yield return new WaitForSeconds(slidingTime);

        playerTrans.rotation = Quaternion.Euler(0, -35, 0);

        canJump = true;

        playerCore.isSliding = false;
    }

}
