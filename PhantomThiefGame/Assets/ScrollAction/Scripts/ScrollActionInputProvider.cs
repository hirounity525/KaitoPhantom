using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollActionInputProvider : MonoBehaviour
{
    public float moveHorizontal;
    public bool isJumpButtonDown;

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
        moveHorizontal = actionMap["Move"].ReadValue<float>();
        isJumpButtonDown = actionMap["Jump"].triggered;
        //Debug.Log(isJumpButtonDown);
    }
}
