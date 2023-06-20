using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReward : MonoBehaviour, IPointerClickHandler
{

    public GameObject questlist;
    public GameObject questcomplete;

    public void OnPointerClick(PointerEventData eventData)
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                GameObject quest2 = singleNPC.transform.Find("Canvas/quest2").gameObject;
                quest2.SetActive(false);
                questcomplete.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                singleNPC.InteractNow = false;
                singleNPC.Monster = 0;
                singleNPC.Activequest = false;
                singleNPC.player.currentexp += 100;
                singleNPC.questended = true;
                questlist.SetActive(false);
                
                break;
            }
        }
    }
}
