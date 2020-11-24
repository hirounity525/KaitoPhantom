using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionLaserAttacker : MonoBehaviour
{
    public bool isLaserHit;
    [Header("Playerレイヤー")] [SerializeField] private LayerMask playerLayer;
    [Header("レーザーの長さ")] [SerializeField] private float rayLength;
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    //private Vector3 hitPos;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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

        lineRenderer.SetPosition(0,gameObject.transform.position);
        lineRenderer.SetPosition(1, gameObject.transform.position + -transform.up * rayLength);

        /*if (Physics.Raycast(ray,out hit, rayLength))
        {
            hitPos = hit.point;
            lineRenderer.SetPosition(1, hitPos);
        }
        else
        {
            lineRenderer.SetPosition(1,gameObject.transform.position + -transform.up * rayLength);
        }*/

    }
}
