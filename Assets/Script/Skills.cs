using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(Image))]
public class Skills : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public Item.ItemType Item;
    public Player player;
    public PlayerController playerController;
    public Transform playerT;
    public Item item;
    public string Name;
    public bool use = false;
    public GameObject currentItemInSlot;

    void Start()
    {
        Target = GetComponent<Image>();
        Target.color = NormalColor;
        playerT = GameObject.Find("Player").transform;
        if (playerT != null)
        {
            player = playerT.GetComponentInParent<Player>();
            playerController = playerT.GetComponentInParent<PlayerController>();
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
            ItemPrefab.itemInSlot = przedmiotWslocie;
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
        if (ItemPrefab.itemInSlot != null && !ItemPrefab.ifDrop)
        {
            if (przedmiotWslocie == null && ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.Type == Item)
            {
                ItemPrefab.itemInSlot.transform.SetParent(this.transform, false);
                ItemPrefab.ifDrop = true;
                ItemPrefab.itemInSlot.GetComponent<CanvasGroup>().blocksRaycasts = true;
                currentItemInSlot = ItemPrefab.itemInSlot;
            }
        }
        else
        {
            if (przedmiotWslocie == null && !SkillPrefab.ifDrop)
            {
                SkillPrefab.ThisSkill.transform.SetParent(this.transform, false);
                SkillPrefab.ifDrop = true;
                SkillPrefab.ThisSkill.GetComponent<CanvasGroup>().blocksRaycasts = true;
                currentItemInSlot = SkillPrefab.ThisSkill;
            }
        }
    }
    void Update()
    {
        playerT = GameObject.Find("Player").transform;
        if (playerT != null)
        {
            player = playerT.GetComponentInParent<Player>();
            playerController = playerT.GetComponentInParent<PlayerController>();
        }
        if (przedmiotWslocie != null)
        {
            if (przedmiotWslocie.transform.Find("Ilosc") != null)
            {
                TextMeshProUGUI tekstComponent = przedmiotWslocie.transform.Find("Ilosc").GetComponentInChildren<TextMeshProUGUI>();
                tekstComponent.text = przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc.ToString();
            }
            if (Input.GetButton(Name) && !use)
            {
                UseItem();
                use = true;
            }
            if (!Input.GetButton(Name))
            {
                playerController.ultimate = false;
                use = false;
            }  
        }
    }
    public void UseItem()
    {
        if (przedmiotWslocie != null && przedmiotWslocie.transform.Find("Ilosc") != null)
        {
            playerT = GameObject.Find("Player").transform;
            if (playerT != null)
            {
                player = playerT.GetComponentInParent<Player>();
            }
            if (przedmiotWslocie.GetComponent<ItemPrefab>().item.Type == Item)
            {
                if (player.PcurrentHealth < player.Phealth && przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc > 1)
                {
                    player.PcurrentHealth += 10;
                    przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc--;
                }
                else if (player.PcurrentHealth < player.Phealth && przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc == 1)
                {
                    player.PcurrentHealth += 10;
                    Destroy(przedmiotWslocie);
                }
            }
        }
        if (przedmiotWslocie != null && przedmiotWslocie.GetComponent<SkillPrefab>() != null && !use && SkillPrefab.skillbool)
        {
            playerController.ultimate = true;
        }
    }
}
