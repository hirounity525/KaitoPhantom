using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleBigBeam : MonoBehaviour
{
    [SerializeField] private float bigBeamSpeed;
    [SerializeField] private Vector3 firstPos;
    [SerializeField] private Vector3 secondPos;
    [SerializeField] private Vector3 thirdPos;
    [SerializeField] private Vector3 fourthPos;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-1,0,0) * bigBeamSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveBeam();
    }

    private void MoveBeam()
    {
        if(gameObject.transform.position == (firstPos))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, 1, 0) * bigBeamSpeed);
        }

        else if (gameObject.transform.position == (secondPos))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(1, 0, 0) * bigBeamSpeed);
        }

        else if (gameObject.transform.position == (thirdPos))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, -1, 0) * bigBeamSpeed);
        }

        else if (gameObject.transform.position == (fourthPos))
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
