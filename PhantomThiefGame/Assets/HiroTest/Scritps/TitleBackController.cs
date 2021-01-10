using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleBackController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap actionMap;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.currentActionMap;
    }

    private void Update()
    {
        if (actionMap["Back"].triggered)
        {
            DebugModeInfo.Instance.isBackTitle = true;
            SceneManager.LoadScene("Title&StageSelect");
        }
    }
}
