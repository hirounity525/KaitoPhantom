using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RunJump3DCameraMover : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private RunJump3DPlayerMover playerMover;
    private int countTemp;
    private int count = 45;
    private float rotateSpeed = 2;
    private float firstYPos;

    // Start is called before the first frame update
    void Start()
    {
        firstYPos = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles);

        Vector3 pos = player.transform.position;
        pos.y = firstYPos;
        transform.position = pos;


        if (playerMover.goPlus)
        {
            if (countTemp < count)
            {
                transform.eulerAngles += new Vector3(0, rotateSpeed, 0);
                countTemp++;
            }

            else
            {
                countTemp = 0;
                playerMover.goPlus = false;
            }
        }

        else if (playerMover.goMinus)
        {
            if (countTemp < count)
            {
                transform.eulerAngles -= new Vector3(0, rotateSpeed, 0);
                countTemp++;
            }

            else
            {
                countTemp = 0;
                playerMover.goMinus = false;
            }
        }
        
    }
}
