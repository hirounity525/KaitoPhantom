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

    private bool isJump = true;
    private bool isSliding = false;
    private bool canSliding = true;
    private int jumpCount;
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

        if ((inputProvider.isSlidingButtunDown > 0 && canSliding) || isSliding )
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
            isJump = true;
            jumpCount = 0;
        }
    }

    private IEnumerator StartSliding()
    {
        playerTrans.rotation = Quaternion.Euler(0, 0, rotationAngle);

        isJump = false;
        isSliding = true;

        yield return new WaitForSeconds(slidingTime);

        isJump = true;
        isSliding = false;

        playerTrans.rotation = new Quaternion(0, 0, 0, 0);
    }

}
