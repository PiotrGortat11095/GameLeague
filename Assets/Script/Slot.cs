using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Target.color = EnterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Target.color = NormalColor;
    }

    void Start() 
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
    }
    

}
