using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    private GameObject inventory;
    private GameObject stats;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public Slot[] slotyEkwipunku;
    public Eq[] eq;
    public PlayerController pc;
    CharacterController characterController;
    public Transform Player;
    public Player playerscript;
    public bool IsOpen;
    public bool IsOpenS;
    private float dodanepunkty = 1;


    private void Start()
    {
        eq = GetComponentsInChildren<Eq>();
        slotyEkwipunku = GetComponentsInChildren<Slot>();
        inventory = transform.Find("Inventory").gameObject;
        stats = transform.Find("Stats").gameObject;
        stats.SetActive(false);
        IsOpenS = false;
        IsOpen = false;
        inventory.SetActive(false);
    }

    private void Update()
    {
        if (Player != null)
        {
            pc = Player.GetComponent<PlayerController>();
            characterController = Player.GetComponent<CharacterController>();
            playerscript = Player.GetComponent<Player>();
            if (playerscript.lvl > dodanepunkty)
            {
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
            }
            else
            {
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
                button4.SetActive(false);
            }
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
            if (Input.GetKeyDown(KeyCode.G) && pc.characterController.isGrounded)
            {
                stats.SetActive(!stats.activeInHierarchy);
                IsOpenS = !IsOpenS;
            }
            TextMeshProUGUI tekstComponent4 = stats.transform.Find("HP").GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI tekstComponent5 = stats.transform.Find("Mana").GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI tekstComponent6 = stats.transform.Find("Damage").GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI tekstComponent7 = stats.transform.Find("Armor").GetComponentInChildren<TextMeshProUGUI>();

            if (tekstComponent4 != null)
            {
                tekstComponent4.text = "HP: " + playerscript.PcurrentHealth.ToString() + "/" + playerscript.Phealth.ToString();

            }
            if (tekstComponent5 != null)
            {
                tekstComponent5.text = "Mana: " + playerscript.PcurrentMana.ToString() + "/" + playerscript.Pmana.ToString();
            }
            if (tekstComponent6 != null)
            {
                tekstComponent6.text = "Damage: " + playerscript.damage.ToString();

            }
            if (tekstComponent7 != null)
            {
                tekstComponent7.text = "Armor: " + playerscript.Armor.ToString();
            }
        }
    }
    public void Click1()
    {
        playerscript = Player.GetComponent<Player>();
        playerscript.Phealth += 4;
        dodanepunkty++;
    }
    public void Click2()
    {
        playerscript = Player.GetComponent<Player>();
        playerscript.Pmana += 2;
        dodanepunkty++;
    }
    public void Click3()
    {
        playerscript = Player.GetComponent<Player>();
        playerscript.damage += 1;
        dodanepunkty++;
    }
    public void Click4()
    {
        playerscript = Player.GetComponent<Player>();
        playerscript.Armor += 2;
        dodanepunkty++;
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
