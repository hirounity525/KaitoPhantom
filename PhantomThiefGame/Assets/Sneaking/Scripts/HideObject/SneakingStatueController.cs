using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingStatueController : MonoBehaviour
{
    [SerializeField] private GameObject disableStatueObj;

    SneakingHideObjectInfo hideObjectInfo;
    SneakingPlayerCore playerCore;

    private void Awake()
    {
        hideObjectInfo = GetComponent<SneakingHideObjectInfo>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCore != null)
        {
            if(playerCore.isHide && hideObjectInfo.isHideTarget)
            {
                disableStatueObj.SetActive(false);
            }
            else
            {
                disableStatueObj.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerCore = other.GetComponent<SneakingPlayerCore>();
    }
}
