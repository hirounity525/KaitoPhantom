using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingInputProvider : MonoBehaviour
{
    public bool canInput;

    public float moveHorizon;
    public float moveVertical;
    public float isAttackButtunDown;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        actionMap = playerInput.currentActionMap;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            isAttackButtunDown = actionMap["Attack"].ReadValue<float>();

            moveHorizon = actionMap["MoveHorizon"].ReadValue<float>();
            moveVertical = actionMap["MoveVertical"].ReadValue<float>();
        }
    }
}
