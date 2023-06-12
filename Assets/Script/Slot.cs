using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public GameObject przedmiotWslocie
    {
        get
        {
            if(transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }return null;
        }
    }
    public GameObject prefabItem;
    public void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Target.color = EnterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Target.color = NormalColor;
    }
    public void DodajPrzedmiotDoSlotu(Item przedmiot)
    {
        GameObject newItem = Instantiate(prefabItem);
        newItem.transform.SetParent(this.transform);
        newItem.GetComponent<ItemPrefab>().item = przedmiot;
        newItem.GetComponent<Image>().sprite = przedmiot.Icon;
    }

    void Start() 
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (przedmiotWslocie == null)
        {
            ItemPrefab.itemInSlot.transform.SetParent(this.transform, false);
            ItemPrefab.ifDrop = true;
            ItemPrefab.itemInSlot.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    public void UseItem()
    {
        if (przedmiotWslocie != null)
        {
            
            if(przedmiotWslocie.GetComponent<ItemPrefab>().item.Type == Item.ItemType.Food)
            {
                Debug.Log("Dodano statystyki");
                Destroy(przedmiotWslocie);
            }
        }
    }
}
