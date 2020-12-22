using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingPlayerHPDrawer : MonoBehaviour
{
    [SerializeField] private ShootingPlayerHPControler playerHP;
    [SerializeField] private Transform playerTrans;
    [SerializeField]private float uiPos;

    private Transform uiTrans;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        uiTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float) playerHP.playerNowHitPoint / playerHP.playerMaxHitPoint;

        uiTrans.position = new Vector3(playerTrans.position.x,playerTrans.position.y-uiPos,playerTrans.position.z);
    }
}
