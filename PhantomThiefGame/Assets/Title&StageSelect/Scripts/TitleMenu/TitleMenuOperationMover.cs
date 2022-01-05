using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//タイトルメニュー時の操作説明の移動
public class TitleMenuOperationMover : MonoBehaviour
{
    [SerializeField] private TitleMenuSelecter titleMenuSelecter;
    [SerializeField] private GameObject[] movePlaceObjs;

    private RectTransform objTrans;

    private TitleMenu nowSelectedTitleMenu;

    private void Awake()
    {
        objTrans = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(nowSelectedTitleMenu != titleMenuSelecter.nowSelectedTitleMenu)
        {
            //移動先の子オブジェクトになり、Positionを移動させる
            objTrans.parent = movePlaceObjs[(int)titleMenuSelecter.nowSelectedTitleMenu].transform;
            objTrans.position = movePlaceObjs[(int)titleMenuSelecter.nowSelectedTitleMenu].transform.position;

            //大きさをリセット
            objTrans.localScale = Vector3.one;

            //更新
            nowSelectedTitleMenu = titleMenuSelecter.nowSelectedTitleMenu;
        }
    }
}
