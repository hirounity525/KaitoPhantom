using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameOverMenu
{
    CONTINUE,
    TOTITLE
}

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameOverMenu selectMenu;

    [SerializeField] private TimelineController timelineController;
    [SerializeField] private SEPlayer sEPlayer;

    [SerializeField] private Transform arrowTrans;
    [SerializeField] private Transform continueButtonTrans;
    [SerializeField] private Transform toTitleButtonTrans;

    [SerializeField] private float arrowPosX;

    private GameOverInputProvider inputProvider;
    private SceneLoader sceneLoader;

    private bool isSelect;

    private void Awake()
    {
        inputProvider = GetComponent<GameOverInputProvider>();
        sceneLoader = GetComponent<SceneLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSelect)
        {
            if (inputProvider.isRightButtonDown)
            {
                sEPlayer.Play("移動");
                selectMenu = GameOverMenu.TOTITLE;
                arrowTrans.localPosition = toTitleButtonTrans.localPosition + Vector3.right * arrowPosX;
            }
            else if (inputProvider.isLeftButtonDown)
            {
                sEPlayer.Play("移動");
                selectMenu = GameOverMenu.CONTINUE;
                arrowTrans.localPosition = continueButtonTrans.localPosition + Vector3.right * arrowPosX;
            }

            if (inputProvider.isSelectButtonDown)
            {
                sEPlayer.Play("決定");

                switch (selectMenu)
                {
                    case GameOverMenu.CONTINUE:
                        timelineController.PlayByName("Continue");
                        break;
                    case GameOverMenu.TOTITLE:
                        timelineController.PlayByName("ToTitle");
                        break;
                }

                timelineController.isFinish = false;

                isSelect = true;
            }
        }
        else
        {
            if (timelineController.isFinish)
            {
                switch (selectMenu)
                {
                    case GameOverMenu.CONTINUE:
                        sceneLoader.LoadScene(CommonData.Instance.selectedStageName);
                        break;
                    case GameOverMenu.TOTITLE:
                        sceneLoader.LoadScene("Title");
                        break;
                }

            }
        }
    }
}
