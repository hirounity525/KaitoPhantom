using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlayerAnimater : MonoBehaviour
{
    private DefensePlayerCore playerCore;
    private Transform playertrans;
    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<DefensePlayerCore>();
        playertrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCore.isRight)
        {
            playertrans.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            playertrans.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
