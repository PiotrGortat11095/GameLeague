using System;
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
    public GameObject currentItemInSlot;

    void Start()
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
        playerT = GameObject.Find("Player").transform;
        if (playerT != null)
        {
            player = playerT.GetComponentInParent<Player>();
        }
    }
    void Update()
    {
        playerT = GameObject.Find("Player").transform;
        if (playerT != null)
        {
            player = playerT.GetComponentInParent<Player>();
            if (currentItemInSlot != null && przedmiotWslocie == null)
            {

                player.Phealth -= currentItemInSlot.GetComponent<ItemPrefab>().item.strength;
                player.PcurrentHealth -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
                player.damage -= currentItemInSlot.GetComponent<ItemPrefab>().item.AttackDamage;
                player.Armor -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.ArmorValue;
                player.CriticalHitChance -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitChance/100;
                player.CriticalHitStrength -= ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitStrength/100;
                currentItemInSlot = null;
            }
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
        if (przedmiotWslocie != null)
        {
            ItemPrefab.itemInSlot = przedmiotWslocie.gameObject;
        }
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
            player.damage += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.AttackDamage;
            player.Armor += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.ArmorValue;
            player.CriticalHitChance += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitChance /100;
            player.CriticalHitStrength += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitStrength /100;
        }
    }
    public void UseItem()
    {
        if (przedmiotWslocie != null)
        {
            Slot[] slotObjects = GameObject.FindObjectsOfType<Slot>();
            Array.Sort(slotObjects, (x, y) => x.slotNumber.CompareTo(y.slotNumber));
            foreach (Slot slotObject in slotObjects)
            {
                if (slotObject.przedmiotWslocie == null)
                {
                    przedmiotWslocie.transform.SetParent(slotObject.transform, false);
                    break;
                }
            }
        }
    }
}
