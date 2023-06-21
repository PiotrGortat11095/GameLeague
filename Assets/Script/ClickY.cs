using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickY : MonoBehaviour, IPointerClickHandler
{
    public GameObject questWindow;

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
                GameObject quest1 = singleNPC.transform.Find("Canvas/quest1").gameObject;
                singleNPC.Activequest = true;
                quest1.SetActive(false);
                singleNPC.InteractNow = false;
                break;
            }
        }
    }

}
