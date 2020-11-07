using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollActionPendulumRotationCenterDrawer : MonoBehaviour
{
  
    [SerializeField] private float gizmoSize = 0.3f;
    private Color gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
