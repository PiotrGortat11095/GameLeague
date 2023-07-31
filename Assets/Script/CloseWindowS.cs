using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseWindowS : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;
    public void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryManager.IsOpenS = false;
    }
}
