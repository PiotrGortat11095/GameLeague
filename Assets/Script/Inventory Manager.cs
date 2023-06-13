using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventory;
    public Slot[] slotyEkwipunku;
    public PlayerController pc;
    CharacterController characterController;
    public Transform Player;
    public bool IsOpen;


    private void Awake()
    {

    }

    private void Start()
    {
        slotyEkwipunku = GetComponentsInChildren<Slot>();
        inventory = transform.Find("Inventory").gameObject;
        IsOpen = false;
        inventory.SetActive(false);
    }

    private void Update()
    {
        if (Player != null)
        {
            pc = Player.GetComponent<PlayerController>();
            characterController = Player.GetComponent<CharacterController>();

            if (Input.GetKeyDown(KeyCode.E) && pc.characterController.isGrounded)
            {
                inventory.SetActive(!inventory.activeInHierarchy);
                IsOpen = !IsOpen;

                foreach (Slot slot in slotyEkwipunku)
                {
                    if (slot != null && slot.Target != null)
                    {
                        slot.Target.color = slot.NormalColor;
                    }
                }

            }
        }
    }
    public void DodajPrzedmiot(Item przedmiot)
    {
        for(int i = 0; i < slotyEkwipunku.Length; i++)
        {
            if (slotyEkwipunku[i].przedmiotWslocie == null)
            {
                slotyEkwipunku[i].DodajPrzedmiotDoSlotu(przedmiot);
                break;
            }
        }
    }

   
}
