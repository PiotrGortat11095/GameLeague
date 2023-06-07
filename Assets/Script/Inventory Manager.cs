using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventory;
    public Slot[] slots;
    public bool IsOpen;
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
        IsOpen = false;
        inventory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
            IsOpen = !IsOpen;

            foreach (Slot slot in slots)
            {
                if (slot != null && slot.Target != null)
                {
                    slot.Target.color = slot.NormalColor;
                }
            }

            if (IsOpen)
            {
                UpdateSlots();
            }
        }
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            if (i < ItemsDatabase.Instance.PlayerItems.Count)
            {
                Slot slot = inventory.transform.GetChild(i).GetComponent<Slot>();
                if (slot != null)
                {
                    Item item = ItemsDatabase.Instance.PlayerItems[i];
                    if (!slot.przedmiotwslocie)
                    {
                        slot.AddItem(item);
                        GameObject itemObject = slot.przedmiotwslocie;
                        if (itemObject != null)
                        {
                            Image itemImage = itemObject.GetComponent<Image>();
                            if (itemImage != null)
                            {
                                itemImage.sprite = item.Icon;
                            }
                        }
                    }
                }
            }
        }
    }
}
