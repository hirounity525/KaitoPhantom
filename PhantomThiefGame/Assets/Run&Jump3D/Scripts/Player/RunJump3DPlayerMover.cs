using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DPlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    //[SerializeField] private Transform rotationPointTrans;

    [Header("Ray")]
    [SerializeField] private float hitRadius;
    [SerializeField] private float hitDistance;
    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private float clouchTime;
    private float clouchTimeTemp;

    [SerializeField] private RunJump3DPlayerInfo playerInfo;

    private Transform playerTrans;
    private Rigidbody rb;
    private bool isGround;
    private bool isCrouch;
    private CapsuleCollider capsuleCollider;
    private BoxCollider boxCollider;
    public int playerMoveDirection = 1;
    /*
     * 0 : -x
     * 1 : +z
     * 2 : +x
     * 3 : -z
     */

    [SerializeField] private RunJump3DInputProvider inputProvider;

    ///シーン統合したら消す***************
    [SerializeField] bool isInputJTemp;
    [SerializeField] bool isInputCTemp;
    [SerializeField] bool isInputLTemp;
    [SerializeField] bool isInputRTemp;
    ///***********************************

    private bool goLeft;
    private bool goRight;

    public bool goPlus;
    public bool goMinus;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTrans = GetComponent<Transform>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.isJump = !isGround;
        playerInfo.isCrouch = isCrouch;
    }

    private void FixedUpdate()
    {
        isGround = IsGround();

        if(playerMoveDirection == 0)
        {
            rb.velocity = new Vector3(-1 * moveSpeed, rb.velocity.y, 0);
        }
        else if (playerMoveDirection == 1)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 1 * moveSpeed);
        }
        else if (playerMoveDirection == 2)
        {
            rb.velocity = new Vector3( 1 * moveSpeed, rb.velocity.y, 0);
        }
        else if (playerMoveDirection == 3)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, -1 * moveSpeed);
        }

        Jump();//上入力時
        Crouch();//下入力時
        if (isCrouch)
        {
            clouchTimeTemp += Time.fixedDeltaTime;
            //Debug.Log(clouchTimeTemp);
            if(clouchTime <= clouchTimeTemp)
            {
                capsuleCollider.enabled = true;
                boxCollider.enabled = false;
                clouchTimeTemp = 0;
                isCrouch = false;
            }
        }
    }

    private void Jump()
    {
        if (isInputJTemp || inputProvider.isJumpButton3)
        {
            if (isGround)
            {
                if (!isCrouch)
                {
                    rb.AddForce(Vector3.up * jumpPower);
                }
            }
        }
    }

    private bool IsGround()
    {
        RaycastHit hit;
        bool isGround = Physics.SphereCast(playerTrans.position, hitRadius, Vector3.down, out hit, hitDistance, hitLayer);
        return isGround;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.down * hitDistance, hitRadius);
    }

    private void Crouch()
    {
        if (isInputCTemp || inputProvider.isCrouchButton)
        {
            //Debug.Log("isInputCTemp");
            if (isGround)
            {
                //Debug.Log("isGround");
                if (!isCrouch)
                {
                    //Debug.Log("!isCrouch");
                    //Debug.Log(isCrouch);
                    //rotationPointTrans.localEulerAngles = new Vector3(90, 0, 0);
                    capsuleCollider.enabled = false;
                    boxCollider.enabled = true;
                    isCrouch = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InputLR"))
        {
            if (isInputLTemp || inputProvider.isLeftButton)
            {
                goLeft = true;
                goRight = false;
            }

            else if (isInputRTemp || inputProvider.isRightButton)
            {
                goRight = true;
                goLeft = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InputLR"))
        {
            if (goRight)
            {
                playerMoveDirection++;
                goPlus = true;
                if(playerMoveDirection == 4)
                {
                    playerMoveDirection = 0;
                }
                goRight = false;
            }

            else if (goLeft)
            {
                playerMoveDirection--;
                goMinus = true;
                if(playerMoveDirection == -1)
                {
                    playerMoveDirection = 3;
                }
                goLeft = false;
            }
        }
    }

}
