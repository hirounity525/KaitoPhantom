using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Update()
    {

    }

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

    public void DisableEventSystem()
    {
        eventSystem.enabled = false;
    }

    public void EnableEventSystem()
    {
        eventSystem.enabled = true;
        eventSystem.SetSelectedGameObject(null);
    }

    public GameObject GetSelectedGameObject()
    {
        return eventSystem.currentSelectedGameObject;
    }
}
