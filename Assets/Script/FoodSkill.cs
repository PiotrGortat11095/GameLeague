using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public string Name;
    public Player player;
    public bool use = false;
    public Transform playerT;
    public int slotNumber;

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
    void Update()
    {
        if (przedmiotWslocie != null)
        {
            TextMeshProUGUI tekstComponent = przedmiotWslocie.transform.Find("Ilosc").GetComponentInChildren<TextMeshProUGUI>();
            tekstComponent.text = przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc.ToString();
            if (Input.GetButton(Name) && !use)
            {
                UseItem();
                use = true;
            }
            if (!Input.GetButton(Name))
            {
                use = false;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (przedmiotWslocie == null)
        {
            ItemPrefab.itemInSlot.transform.SetParent(this.transform, false);
            ItemPrefab.ifDrop = true;
            ItemPrefab.itemInSlot.GetComponent<CanvasGroup>().blocksRaycasts = true;


        }
        else if (przedmiotWslocie != null && przedmiotWslocie.GetComponent<ItemPrefab>().item.Type == Item.ItemType.Food && przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc < 100)
        {
            if (ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().item.Type == Item.ItemType.Food)
            {
                if (przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc + ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().Ilosc <= 100)
                {
                    przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc += ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().Ilosc;
                    Destroy(ItemPrefab.itemInSlot);
                }
                else if (przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc + ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().Ilosc > 100)
                {
                    ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().Ilosc = przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc + ItemPrefab.itemInSlot.gameObject.GetComponent<ItemPrefab>().Ilosc - 100;
                    przedmiotWslocie.GetComponent<ItemPrefab>().Ilosc = 100;
                }
            }
        }
    }

    public void UseItem()
    {
        if (przedmiotWslocie != null)
        {
            playerT = GameObject.Find("Player").transform;
            if (playerT != null)
            {
                player = playerT.GetComponentInParent<Player>();
            }
            if (przedmiotWslocie.GetComponent<ItemPrefab>().item.Type == Item.ItemType.Food)
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
    }
}

