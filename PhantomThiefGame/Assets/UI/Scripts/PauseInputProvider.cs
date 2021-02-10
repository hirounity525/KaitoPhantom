using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInputProvider : MonoBehaviour
{
    public bool isUpButtonDown;
    public bool isDownButtonDown;
    public bool isSelectButtonDown;
    public bool isPauseButtonDown;

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
        isUpButtonDown = actionMap["MoveUp"].triggered;
        isDownButtonDown = actionMap["MoveDown"].triggered;

        isSelectButtonDown = actionMap["Select"].triggered;
        isPauseButtonDown = actionMap["Pause"].triggered;
    }
}
