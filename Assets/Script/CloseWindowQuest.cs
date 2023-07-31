using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseWindowQuest : MonoBehaviour, IPointerClickHandler
{
    private QuestManager questManager;
    public void Start()
    {
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        questManager.IsOpen = false;
    }
}
