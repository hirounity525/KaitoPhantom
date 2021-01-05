using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunObjectRotation : MonoBehaviour
{
    private Transform objTrans;

    private int objRotation;
    // Start is called before the first frame update
    void Start()
    {
        objTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        objTrans.rotation = new Quaternion(0, 0, 0, 0);
    }
}
