using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Click : MonoBehaviour, IPointerClickHandler
{
    public GameObject questWindow;
    public GameObject questend;
    public GameObject questactive;

    public void OnPointerClick(PointerEventData eventData)
    {
        questWindow.SetActive(false);
        questend.SetActive(false);
        questactive.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                singleNPC.InteractNow = false;
                break;
            }
        }
    }

    
}

