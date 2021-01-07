using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObjectTuner : MonoBehaviour
{
    [SerializeField] private float tuneSpeed;

    private Transform objTrans;
    // Start is called before the first frame update
    void Start()
    {
        objTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        objTrans.Rotate(Vector3.up * tuneSpeed);

    }
}
