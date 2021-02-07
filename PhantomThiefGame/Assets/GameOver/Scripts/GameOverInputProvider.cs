using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverInputProvider : MonoBehaviour
{
    public bool isRightButtonDown;
    public bool isLeftButtonDown;
    public bool isSelectButtonDown;

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
        isRightButtonDown = actionMap["MoveRight"].triggered;
        isLeftButtonDown = actionMap["MoveLeft"].triggered;

        isSelectButtonDown = actionMap["Select"].triggered;
    }
}
