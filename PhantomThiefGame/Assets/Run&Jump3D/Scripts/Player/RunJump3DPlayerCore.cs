using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DPlayerCore : MonoBehaviour
{
    public bool isGameOver;
    public int life;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            isGameOver = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            life -= 1;
            Debug.Log(life);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            life = 0;
            Debug.Log(life);
        }
    }
}
