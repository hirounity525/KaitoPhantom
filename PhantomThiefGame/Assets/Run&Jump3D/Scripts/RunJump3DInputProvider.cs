using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunJump3DInputProvider : MonoBehaviour
{
    public bool canInput;

    public bool isJumpButton3;
    public bool isCrouchButton;
    public bool isLeftButton;
    public bool isRightButton;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private float isJumpButton3Value;
    private float isCrouchButtonValue;
    private float isLeftButtonValue;
    private float isRightButtonValue;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.currentActionMap;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {

            //右移動の入力
            isRightButtonValue = actionMap["Right3D"].ReadValue<float>();

            //左移動の入力
            isLeftButtonValue = actionMap["Left3D"].ReadValue<float>();

            //ジャンプボタンの入力
            isJumpButton3Value = actionMap["Jump3D"].ReadValue<float>();

            //しゃがみボタンの入力
            isCrouchButtonValue = actionMap["Crouch3D"].ReadValue<float>();



            if (isRightButtonValue > 0)
            {
                isRightButton = true;
            }
            else
            {
                isRightButton = false;
            }

            if (isLeftButtonValue > 0)
            {
                isLeftButton = true;
            }
            else
            {
                isLeftButton = false;
            }

            if (isJumpButton3Value > 0)
            {
                isJumpButton3 = true;
            }
            else
            {
                isJumpButton3 = false;
            }

            if (isCrouchButtonValue > 0)
            {
                isCrouchButton = true;
            }
            else
            {
                isCrouchButton = false;
            }
        }

    }
}
