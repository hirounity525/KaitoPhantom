using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PauseMenu
{
    RETURNGAME,
    TOSTAGESELECT
}

public class PauseController : MonoBehaviour
{
    public bool canPause;
    public bool isPause;

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] private Transform arrowTrans;
    [SerializeField] private Transform returnGameButtonTrans;
    [SerializeField] private Transform toStageSelectButtonTrans;

    [SerializeField] private Image whitePanel;

    [SerializeField] private float arrowPosX;
    [SerializeField] private float fadeTime;

    private PauseInputProvider inputProvider;
    private CommonDataController commonDataController;
    private SceneLoader sceneLoader;
    private SEPlayer sEPlayer;

    private bool isSelect;

    private void Awake()
    {
        inputProvider = GetComponent<PauseInputProvider>();
        commonDataController = GetComponent<CommonDataController>();
        sceneLoader = GetComponent<SceneLoader>();
        sEPlayer = GetComponent<SEPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPause)
        {
            if (inputProvider.isPauseButtonDown)
            {
                Time.timeScale = 0;

                isSelect = false;
                isPause = true;
                pauseCanvas.SetActive(true);
                canPause = false;
            }
        }

        if (isPause)
        {
            if (!isSelect)
            {
                if (inputProvider.isUpButtonDown)
                {
                    sEPlayer.Play("移動");
                    pauseMenu = PauseMenu.RETURNGAME;
                    arrowTrans.localPosition = returnGameButtonTrans.localPosition + Vector3.right * arrowPosX;
                }
                else if (inputProvider.isDownButtonDown)
                {
                    sEPlayer.Play("移動");
                    pauseMenu = PauseMenu.TOSTAGESELECT;
                    arrowTrans.localPosition = toStageSelectButtonTrans.localPosition + Vector3.right * arrowPosX;
                }

                if (inputProvider.isSelectButtonDown)
                {

                    switch (pauseMenu)
                    {
                        case PauseMenu.RETURNGAME:
                            sEPlayer.Play("キャンセル");

                            Time.timeScale = 1;
                            pauseCanvas.SetActive(false);
                            isPause = false;

                            break;
                        case PauseMenu.TOSTAGESELECT:
                            sEPlayer.Play("決定");

                            Time.timeScale = 1;

                            whitePanel.DOFade(1, fadeTime).OnComplete(() =>
                            {
                                commonDataController.BackTitle();
                                sceneLoader.LoadScene("Title");
                            });

                            break;
                    }

                    isSelect = true;
                }
            }
        }
    }
}
