using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunClearManager : MonoBehaviour
{
    private bool isClear;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isClear = true;
        }
    }

    public bool IsClear()
    {
        return isClear;
    }
}
