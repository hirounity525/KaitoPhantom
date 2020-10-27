using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionBladeFromBelowMover : MonoBehaviour
{
    [SerializeField] private float appearanceStartTime;
    [SerializeField] private float disappearanceStartTime;
    [SerializeField] private float upVelocityY;
    [SerializeField] private float downVelocityY;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, upVelocityY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TopWait()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(disappearanceStartTime);
        rb.velocity = new Vector3(0, downVelocityY, 0);
    }

    IEnumerator BottomWait()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(appearanceStartTime);
        rb.velocity = new Vector3(0, upVelocityY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("BladeReverseTop"))
        {
            StartCoroutine("TopWait");
        }

        else if(other.gameObject.layer == LayerMask.NameToLayer("BladeReverseBottom"))
        {
            StartCoroutine("BottomWait");
        }
    }
}
