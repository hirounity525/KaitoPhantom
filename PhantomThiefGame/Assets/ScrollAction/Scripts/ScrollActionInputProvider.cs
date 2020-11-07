using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollActionInputProvider : MonoBehaviour
{
    public float moveHorizontal;
    public bool isJumpButtonDown;
    public bool isJumpButtonUp;

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
        moveHorizontal = actionMap["Move"].ReadValue<float>();

        //ジャンプボタンを押した瞬間
        isJumpButtonDown = actionMap["Jump"].triggered;

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
                isJumpButtonUp = true;
                isJumpButton = false;
            }
        }
        else
        {
            isJumpButtonUp = false;
        }
    }
}