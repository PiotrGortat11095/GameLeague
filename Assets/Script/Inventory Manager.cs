using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    GameObject Inventory;
    public bool IsOpen;
    void Start()
    {
        Inventory = transform.Find("Inventory").gameObject;
        IsOpen = false;
        Inventory.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.SetActive(!Inventory.activeInHierarchy);
            IsOpen = !IsOpen;

            for (int i = 0; i < Inventory.transform.childCount; i++)
            {
                Slot slot = Inventory.transform.GetChild(i).GetComponent<Slot>();

                if (slot != null)
                {

                    if (slot.Target != null)
                    {
                        slot.Target.color = slot.NormalColor;
                    }
                    else
                    {
                        return;
                    }

                }

            }
            UpdateSlots();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ItemsDatabase.Instance.PlayerItems.Add(ItemsDatabase.Instance.Items[0]);

            UpdateSlots();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ItemsDatabase.Instance.PlayerItems.Add(ItemsDatabase.Instance.Items[1]);
            UpdateSlots();

        }
    }
    void UpdateSlots()
    {

    }
}
