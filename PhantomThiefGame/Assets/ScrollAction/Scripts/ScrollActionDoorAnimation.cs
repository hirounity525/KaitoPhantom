using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionDoorAnimation : MonoBehaviour
{
    [SerializeField] private SEPlayer sEPlayer2;
    [SerializeField] private Animator doorAnimator;
    private bool isOneActionFinished;

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
            if (!isOneActionFinished)
            {
                sEPlayer2.Play("Door");
                doorAnimator.SetBool("isOpen",true);
                isOneActionFinished = true;
            }
        }
    }
}
