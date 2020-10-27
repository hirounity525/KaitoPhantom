using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionRoundCutterMover : MonoBehaviour
{
    
    [SerializeField] private float roundCutterVelocityX;
    private Rigidbody rb;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(roundCutterVelocityX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3(roundCutterVelocityX, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("RoundCutterReverse"))
        {
            rb.velocity *= -1;
            //roundCutterVelocityX = roundCutterVelocityX * -1;
        }
    }
}
