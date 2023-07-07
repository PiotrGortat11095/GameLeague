using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public GameObject przedmiotWslocie
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    void Start()
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Target.color = EnterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Target.color = NormalColor;
    }

}
