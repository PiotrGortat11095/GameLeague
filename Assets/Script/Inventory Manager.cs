using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    private GameObject inventory;
    public Slot[] slotyEkwipunku;
    public Eq[] eq;
    public PlayerController pc;
    CharacterController characterController;
    public Transform Player;
    public bool IsOpen;


    private void Start()
    {
        eq = GetComponentsInChildren<Eq>();
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
                        if (slot.przedmiotWslocie != null)
                        {
                            Transform ramkaTransform = slot.przedmiotWslocie.transform.Find("Ramka");
                            ramkaTransform.gameObject.SetActive(false);
                            TextMeshProUGUI tekstComponent = ramkaTransform.transform.Find("Nazwa").GetComponentInChildren<TextMeshProUGUI>();
                            TextMeshProUGUI tekstComponent2 = ramkaTransform.transform.Find("Opis").GetComponentInChildren<TextMeshProUGUI>();

                            if (tekstComponent != null)
                            {
                                tekstComponent.text = slot.przedmiotWslocie.GetComponent<ItemPrefab>().item.Name;
                               
                            }
                            if (tekstComponent2 != null)
                            {
                                tekstComponent2.text = slot.przedmiotWslocie.GetComponent<ItemPrefab>().item.Description;
                            }
                        }
                    }
                }
                foreach (Eq eq1 in eq)
                {
                    if (eq1 != null && eq1.Target != null)
                    {
                        eq1.Target.color = eq1.NormalColor;
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
