using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingPlayerCheckPointUpdater : MonoBehaviour
{
    [SerializeField] private Transform firstCheckPoint;

    private SneakingPlayerCore playerCore;

    private int nowCheckPointNum;

    private void Awake()
    {
        playerCore = GetComponent<SneakingPlayerCore>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCore.checkPoint = firstCheckPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        SneakingCheckPointCore checkPointCore = other.GetComponent<SneakingCheckPointCore>();

        if(checkPointCore != null)
        {
            if(checkPointCore.checkPointNum > nowCheckPointNum)
            {
                playerCore.checkPoint = checkPointCore.transform;
            }
        }
    }
}
