using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionLaserAttacker : MonoBehaviour
{
    [Header("Playerレイヤー")] [SerializeField] private LayerMask playerLayer;
    [Header("レーザーの長さ")] [SerializeField] private float rayLength;
    public bool isLaserHit;
    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit, rayLength, playerLayer))
        {
            isLaserHit = true;
            Debug.Log("hit");
        }
        else
        {
            isLaserHit = false;
        }
    }
}
