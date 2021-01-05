using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGuardMover : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private Vector3 speedTemp;
    private Rigidbody rb;
    private Transform guardTrans;

    // Start is called before the first frame update
    void Start()
    {
        guardTrans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(minSpeed,maxSpeed), 0, 0);
        speedTemp = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 0)
        {
            guardTrans.rotation = Quaternion.Euler(0, 90, 0);
        }

        else if (rb.velocity.x < 0)
        {
            guardTrans.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.velocity = -speedTemp;
            speedTemp = -speedTemp;
        }
    }
}
