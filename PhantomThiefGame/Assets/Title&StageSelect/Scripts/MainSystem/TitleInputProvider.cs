using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//タイトル時の入力を受け取る
public class TitleInputProvider : MonoBehaviour
{
    public InputArrow moveArrow;
    public bool isMoveButtonDown;
    public bool isSelectButtonDown;
    public bool isCancelButtonDown;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private bool isUpButtonDown, isDownButtonDown, isRightButtonDown, isLeftButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.currentActionMap;
    }

    // Update is called once per frame
    void Update()
    {
        isUpButtonDown = actionMap["MoveUp"].triggered;
        isDownButtonDown = actionMap["MoveDown"].triggered;
        isRightButtonDown = actionMap["MoveRight"].triggered;
        isLeftButtonDown = actionMap["MoveLeft"].triggered;

        if (isUpButtonDown)
        {
            moveArrow = InputArrow.UP;
            isMoveButtonDown = true;
        }
        else if (isDownButtonDown)
        {
            moveArrow = InputArrow.DOWN;
            isMoveButtonDown = true;
        }
        else if (isRightButtonDown)
        {
            moveArrow = InputArrow.RIGHT;
            isMoveButtonDown = true;
        }
        else if (isLeftButtonDown)
        {
            moveArrow = InputArrow.LEFT;
            isMoveButtonDown = true;
        }
        else
        {
            isMoveButtonDown = false;
        }

        isSelectButtonDown = actionMap["Select"].triggered;
        isCancelButtonDown = actionMap["Cancel"].triggered;
    }
}
