using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunInputProvider : MonoBehaviour
{
    public bool isJumpButtunDown;
    public float isSlidingButtunDown;

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
        isJumpButtunDown = actionMap["Jump"].triggered;

        isSlidingButtunDown = actionMap["Sliding"].ReadValue<float>();
    }
}
