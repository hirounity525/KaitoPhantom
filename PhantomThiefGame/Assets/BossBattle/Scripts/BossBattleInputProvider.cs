using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossBattleInputProvider : MonoBehaviour
{
    public float moveHorizontal2;
    public bool isJumpButtonDown2;
    public bool isJumpButtonUp2;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private float jumpButtonValue;
    private bool isJumpButton;

    private float defaultButtonPressPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.currentActionMap;

        defaultButtonPressPoint = InputSystem.settings.defaultButtonPressPoint;
    }

    // Update is called once per frame
    void Update()
    {

        //移動の入力
        moveHorizontal2 = actionMap["Move"].ReadValue<float>();

        //ジャンプボタンを押した瞬間
        isJumpButtonDown2 = actionMap["Jump"].triggered;

        //ジャンプボタンを離した瞬間
        jumpButtonValue = actionMap["Jump"].ReadValue<float>();

        if (jumpButtonValue >= defaultButtonPressPoint)
        {
            isJumpButton = true;
        }

        if (isJumpButton)
        {
            if (jumpButtonValue < defaultButtonPressPoint)
            {
                isJumpButtonUp2 = true;
                isJumpButton = false;
            }
        }
        else
        {
            isJumpButtonUp2 = false;
        }
    }
}
