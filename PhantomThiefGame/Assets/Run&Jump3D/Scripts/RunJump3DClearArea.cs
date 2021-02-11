using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DClearArea : MonoBehaviour
{
    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Clear!!!");
            isClear = true;
        }
    }
}
