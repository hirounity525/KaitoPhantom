using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakingCameraChanger : MonoBehaviour
{
    [SerializeField] private GameObject mainVC;

    public void ChangeMainCamera(GameObject changeVC)
    {
        if (mainVC.activeSelf)
        {
            ChangeCamera(mainVC, changeVC);
        }
        else
        {
            ChangeCamera(changeVC, mainVC);
        }
    }

    public void ChangeCamera(GameObject nowVC, GameObject changeVC)
    {
        nowVC.SetActive(false);
        changeVC.SetActive(true);
    }
}
