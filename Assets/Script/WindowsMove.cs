using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 startMousePosition;
    private Vector2 startObjectPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePosition = eventData.position;
        startObjectPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - startMousePosition;
        Vector2 newPosition = startObjectPosition + diff;
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

}
