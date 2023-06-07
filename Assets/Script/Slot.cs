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
    public GameObject przedmiotwslocie
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
    public GameObject prefabItem;
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
    public void AddItem(Item przedmiot)
    {

        GameObject newitem = Instantiate(prefabItem);
        newitem.transform.SetParent(this.transform);
        newitem.GetComponent<ItemPrefab>().item = przedmiot;
        newitem.GetComponent<Image>().sprite = przedmiot.Icon;
    }

}
