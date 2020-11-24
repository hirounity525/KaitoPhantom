using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGuardMover : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private Vector3 speedTemp;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(minSpeed,maxSpeed), 0, 0);
        speedTemp = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
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
