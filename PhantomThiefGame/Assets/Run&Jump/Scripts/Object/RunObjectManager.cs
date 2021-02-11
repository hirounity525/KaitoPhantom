using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunObjectManager : MonoBehaviour
{
    [SerializeField] private RunObjectMover[] objectMovers;

    public void StartMove()
    {
        foreach(RunObjectMover objectMover in objectMovers)
        {
            objectMover.canMove = true;
        }
    }

    public void StopMove()
    {
        foreach (RunObjectMover objectMover in objectMovers)
        {
            objectMover.canMove = false;
        }
    }
}
