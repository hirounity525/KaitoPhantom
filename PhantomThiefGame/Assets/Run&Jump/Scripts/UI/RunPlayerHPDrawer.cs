using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunPlayerHPDrawer : MonoBehaviour
{
    [SerializeField] private RunPlayerHPController playerHP;
    [SerializeField] private Transform playerTrans;
    [SerializeField]private float uiPos;

    private Transform uiTrans;
    private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        uiTrans = GetComponent<Transform>();

        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value =(float) playerHP.playerNowHP / playerHP.playerMaxHP;

        uiTrans.position = new Vector3(playerTrans.position.x, playerTrans.position.y - uiPos, playerTrans.position.z);
    }
}
