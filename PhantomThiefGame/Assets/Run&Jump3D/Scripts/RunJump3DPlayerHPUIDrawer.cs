using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunJump3DPlayerHPUIDrawer : MonoBehaviour
{
    [SerializeField] private RunJump3DPlayerCore playerCore;
    [SerializeField] private GameObject hart1;
    [SerializeField] private GameObject hart2;
    [SerializeField] private GameObject hart3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerCore.life == 2)
        {
            Destroy(hart1);
        }
        else if (playerCore.life == 1)
        {
            Destroy(hart2);
        }
        else if (playerCore.life == 0)
        {
            Destroy(hart1);
            Destroy(hart2);
            Destroy(hart3);
        }
        
    }
}
