using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingInputProvider : MonoBehaviour
{
    public float moveHorizon;
    public float moveVertical;
    public bool isAttackButtonDown=false;

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


        isAttackButtonDown = actionMap["Attack"].triggered;
    }
}
