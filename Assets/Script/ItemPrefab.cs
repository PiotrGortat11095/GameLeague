using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPrefab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    static public GameObject itemInSlot;
    static public bool ifDrop;
    public Item item;
    Vector2 startPosition;
    Transform startParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemInSlot = this.gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.parent.parent.parent,false);
        ifDrop = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ifDrop == false)
        {
            transform.position = startPosition;
            transform.SetParent(startParent,false);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
