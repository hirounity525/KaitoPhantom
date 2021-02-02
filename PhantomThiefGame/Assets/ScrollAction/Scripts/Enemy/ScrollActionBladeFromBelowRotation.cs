using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionBladeFromBelowRotation : MonoBehaviour
{
    [SerializeField] private float pushRotateSpeed;
    [SerializeField] private float pullRotateSpeed;
    [SerializeField] private ScrollActionBladeFromBelowMover bladeFromBelowMover;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bladeFromBelowMover.isPush)
        {
            transform.eulerAngles += new Vector3(0, pushRotateSpeed, 0);
        }

        else if (bladeFromBelowMover.isPull)
        {
            transform.eulerAngles += new Vector3(0, -pullRotateSpeed, 0);
        }
    }
}
