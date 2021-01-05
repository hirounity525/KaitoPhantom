using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SneakingInputProvider : MonoBehaviour
{
    public Vector2 moveVec;
    public bool isHideButtonDown;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.currentActionMap;
    }

    // Update is called once per frame
    void Update()
    {
        moveVec = actionMap["Move"].ReadValue<Vector2>();
        isHideButtonDown = actionMap["Hide"].triggered;
    }
}
