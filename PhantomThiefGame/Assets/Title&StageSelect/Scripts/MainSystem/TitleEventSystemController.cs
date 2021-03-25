using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//EventSystemを扱う
public class TitleEventSystemController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedSaveDataButton;
    [SerializeField] private GameObject firstSelectedNameInputButton;
    [SerializeField] private GameObject firstSelectedCheckButton;

    EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    //EventSystemのfirstSelectedButtonに各ステートに対応するGameObjectをセットする
    public void SetSelected(TitleState titleState)
    {
        GameObject firstSelectedButton = null;

        switch (titleState)
        {
            case TitleState.SAVEDATASELECT:
                firstSelectedButton = firstSelectedSaveDataButton;
                break;
            case TitleState.NAMEINPUT:
                firstSelectedButton = firstSelectedNameInputButton;
                break;
            case TitleState.CHECK:
                firstSelectedButton = firstSelectedCheckButton;
                break;
        }

        if(firstSelectedButton != null)
        {
            eventSystem.SetSelectedGameObject(firstSelectedButton);
        }
    }

    //EventSystemを使用不可にする
    public void DisableEventSystem()
    {
        eventSystem.enabled = false;
    }

    //EventSystemを使用可にする
    public void EnableEventSystem()
    {
        eventSystem.enabled = true;
        eventSystem.SetSelectedGameObject(null);
    }

    //現在選んでいるGameObject
    public GameObject GetSelectedGameObject()
    {
        return eventSystem.currentSelectedGameObject;
    }
}
