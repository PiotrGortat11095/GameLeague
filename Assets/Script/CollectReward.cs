using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectReward : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager ekwipunek;
    Menu menu;
    public void Start()
    {
        menu = GameObject.Find("Player").GetComponent<Menu>();
        ekwipunek = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                if (menu.wizard1)
                {
                    ekwipunek.DodajPrzedmiot(singleNPC.ItemsMage);
                }
                else if (menu.warrior1)
                {
                    ekwipunek.DodajPrzedmiot(singleNPC.ItemsWarrior);
                }
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                singleNPC.InteractNow = false;
                Destroy(this.gameObject);
            }
        }
    }

}

