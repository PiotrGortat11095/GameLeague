using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickY : MonoBehaviour, IPointerClickHandler
{
    public GameObject questWindow;
    public GameObject Quest;
    public GameObject questlist;

    public void OnPointerClick(PointerEventData eventData)
    {
        questWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                GameObject quest1 = singleNPC.transform.Find("Mark/quest1").gameObject;
                singleNPC.Activequest = true;
                GameObject newQuest = Instantiate(Quest);
                Transform Questposition = questlist.transform.Find("Image/Image/Scroll/Panel");
                newQuest.transform.SetParent(Questposition, false);

                quest1.SetActive(false);
                singleNPC.InteractNow = false;
                break;
            }
        }
    }

}
