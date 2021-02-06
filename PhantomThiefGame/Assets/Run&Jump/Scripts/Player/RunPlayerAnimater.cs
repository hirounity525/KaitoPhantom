using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerAnimater : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private RunPlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = GetComponent<RunPlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
