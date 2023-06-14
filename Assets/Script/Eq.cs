using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Eq : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public Item.ItemType Item;
    public Player player;
    public Transform playerT;
    public Item item;
    private GameObject currentItemInSlot;

    void Start()
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
        playerT = GameObject.Find("Player").transform;
        player = playerT.GetComponentInParent<Player>();
    }
    void Update()
    {
        playerT = GameObject.Find("Player").transform;
        player = playerT.GetComponentInParent<Player>();
        if (currentItemInSlot != null && przedmiotWslocie == null)
        {
            
            player.Phealth -= currentItemInSlot.GetComponent<ItemPrefab>().item.strength;
            player.PcurrentHealth -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
            player.damageboost -= currentItemInSlot.GetComponent<ItemPrefab>().item.AttackDamage;
            player.Armor -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.ArmorValue;


            currentItemInSlot = null;
        }

    }

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
    public void DodajPrzedmiotDoSlotu(Item przedmiot)
    {
        GameObject newItem = Instantiate(prefabItem);
        newItem.transform.SetParent(this.transform);
        newItem.GetComponent<ItemPrefab>().item = przedmiot;
        newItem.GetComponent<Image>().sprite = przedmiot.Icon;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (przedmiotWslocie == null && ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.Type == Item)
        {
            ItemPrefab.itemInSlot.transform.SetParent(this.transform, false);
            ItemPrefab.ifDrop = true;
            ItemPrefab.itemInSlot.GetComponent<CanvasGroup>().blocksRaycasts = true;
            currentItemInSlot = ItemPrefab.itemInSlot;
            player.Phealth += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
            player.PcurrentHealth += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
            player.damageboost += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.AttackDamage;
            player.Armor += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.ArmorValue;
        }

    }

}
