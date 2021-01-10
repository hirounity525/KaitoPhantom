using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleMenu
{
    START,
    CONFIG,
    EXIT
}

public class TitleMenuSelecter : MonoBehaviour
{
    public bool isSelect;
    public TitleMenu nowSelectedTitleMenu;

    [SerializeField] private TitleMenu firstViewTitleMenu;

    [SerializeField] private TitleInputProvider titleInput;

    private void Start()
    {
        nowSelectedTitleMenu = firstViewTitleMenu;
    }

    private void Update()
    {
        if (!isSelect)
        {
            if (titleInput.isMoveButtonDown)
            {
                switch (nowSelectedTitleMenu)
                {
                    case TitleMenu.START:

                        switch (titleInput.moveArrow)
                        {
                            case InputArrow.UP:
                                nowSelectedTitleMenu = TitleMenu.CONFIG;
                                break;
                            case InputArrow.DOWN:
                                break;
                            case InputArrow.RIGHT:
                                nowSelectedTitleMenu = TitleMenu.EXIT;
                                break;
                            case InputArrow.LEFT:
                                break;
                        }

                        break;
                    case TitleMenu.CONFIG:

                        switch (titleInput.moveArrow)
                        {
                            case InputArrow.UP:
                                break;
                            case InputArrow.DOWN:
                                nowSelectedTitleMenu = TitleMenu.EXIT;
                                break;
                            case InputArrow.RIGHT:
                                break;
                            case InputArrow.LEFT:
                                nowSelectedTitleMenu = TitleMenu.START;
                                break;
                        }

                        break;
                    case TitleMenu.EXIT:

                        switch (titleInput.moveArrow)
                        {
                            case InputArrow.UP:
                                nowSelectedTitleMenu = TitleMenu.CONFIG;
                                break;
                            case InputArrow.DOWN:
                                break;
                            case InputArrow.RIGHT:
                                break;
                            case InputArrow.LEFT:
                                nowSelectedTitleMenu = TitleMenu.START;
                                break;
                        }
                        break;
                }
            }

            if (titleInput.isSelectButtonDown)
            {
                isSelect = true;
            }
        }
    }
}
