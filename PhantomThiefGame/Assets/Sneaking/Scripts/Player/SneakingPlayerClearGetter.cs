using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingPlayerClearGetter : MonoBehaviour
{
    private SneakingPlayerCore playerCore;

    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            other.gameObject.SetActive(false);
            playerCore.isClear = true;
        }
    }
}
