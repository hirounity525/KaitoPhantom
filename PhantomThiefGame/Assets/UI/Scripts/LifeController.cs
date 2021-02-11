using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private GameObject lifeFrontObj;

    public void DeleteLife()
    {
        lifeFrontObj.SetActive(false);
    }
}
