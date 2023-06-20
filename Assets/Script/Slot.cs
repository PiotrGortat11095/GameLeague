using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector] public Image Target;
    public Color32 NormalColor;
    public Color32 EnterColor;
    public string Name;
    public Player player;
    public Transform playerT;
    public int slotNumber;

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
            Eq[] eqObjects = GameObject.FindObjectsOfType<Eq>();
            playerT = GameObject.Find("Player").transform;
            if (playerT != null)
            {
                player = playerT.GetComponentInParent<Player>();
            }


                foreach (Eq eqObject in eqObjects)
                {
                    if (eqObject.Item == przedmiotWslocie.GetComponent<ItemPrefab>().item.Type && eqObject.przedmiotWslocie == null)
                    {
                        przedmiotWslocie.transform.SetParent(eqObject.transform, false);
                        eqObject.currentItemInSlot = ItemPrefab.itemInSlot;
                        player.Phealth += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
                        player.PcurrentHealth += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.strength;
                        player.damage += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.AttackDamage;
                        player.Armor += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.ArmorValue;
                        player.CriticalHitChance += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitChance / 100;
                        player.CriticalHitStrength += ItemPrefab.itemInSlot.GetComponent<ItemPrefab>().item.CriticalHitStrength / 100;
                        break;
                    }
                }
            
        }
    }
}
