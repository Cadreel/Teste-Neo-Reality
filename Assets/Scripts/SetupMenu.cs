using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetupMenu : MonoBehaviour
{
    [SerializeField] private CubeManager cubeManager;
    [SerializeField] private GameObject[] menuItems;

    private void Start()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            int index = i;
            EventTrigger trigger = menuItems[i].AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data, index); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data, index); });
            trigger.triggers.Add(entryExit);
        }
    }

    public void OnPointerEnter(PointerEventData eventData, int index)
    {
        if(cubeManager != null)
        {
            cubeManager.HighlightCube(index);
        }
    }

    public void OnPointerExit(PointerEventData eventData, int index)
    {
        if (cubeManager != null)
        {
            cubeManager.HighlightCube(-1);
        }
    }
}
