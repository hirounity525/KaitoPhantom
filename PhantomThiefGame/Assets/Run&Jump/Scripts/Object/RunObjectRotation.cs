using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationDegree;

    private Transform objTrans;

    // Start is called before the first frame update
    void Start()
    {
        objTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //objTrans.rotation = Quaternion.AngleAxis(rotationDegree, Vector3.right);
        objTrans.Rotate(Vector3.right * rotationDegree);
    }
}
