using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    public GameObject questWindow;
    public Text questDescription;
    public GameObject questactive;
    public Text questDescriptionActive;
    public GameObject questcomplete;
    public Text questDescriptionComplete;
    public GameObject questlist;
    public GameObject questend;
    public Text questDescriptionend;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject NPC;
    public string questDescription1;
    public string questDescriptionComplete1;
    public string questDescriptionEnd1;
    public string questDescriptionList1;
    public string questDescriptionList2;
    public string MonsterName;
    [SerializeField] private Text QuestText;
    private Player player;
    private AiMobs aimobs;
    private bool questended = false;
    public Transform playerTransform;
    public Transform Ai;
    public bool Activequest;
    Menu menu;
    Transform Menu1;

    public bool InteractNow = false;
    public Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void Start()
    {
        if (Ai != null)
        {
            aimobs = Ai.GetComponent<AiMobs>();
        }
        
        quest1.SetActive(true);
        questDescription.text = questDescription1;
        questDescriptionActive.text = questDescription1;
        questDescriptionComplete.text = questDescriptionComplete1;
        questDescriptionend.text = questDescriptionEnd1;
    }
    public void Update()
    {
        if (playerTransform != null)
        {
            player = playerTransform.GetComponent<Player>();
        }
        if (aimobs != null)
        {
            QuestText.text = questDescriptionList1 + "Pokonano: " + AiMobs.Monster + "/" + questDescriptionList2;
            if (AiMobs.Monster >= Int32.Parse(questDescriptionList2))
            {
                quest2.SetActive(true);
            }
        }
        Menu1 = GameObject.Find("Player").transform;
        menu = Menu1.GetComponent<Menu>();
        if (Activequest && !menu.visible)
        {
            questlist.SetActive(true);
        }

    }

    public void YesButton()
    {
        questWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractNow = false;
        Activequest = true;
        if (Activequest)
        {
            quest1.SetActive(false);
            questlist.SetActive(true);
        }
    }

    public void Interact()
    {
        if (!Activequest && !questended)
        {
            questWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (Activequest && AiMobs.Monster < Int32.Parse(questDescriptionList2) && !questended)
        {
            questactive.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (Activequest && AiMobs.Monster >= Int32.Parse(questDescriptionList2) && !questended)
        {
            questcomplete.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (questended)
        {
            questend.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
    }
    public void Rewards()
    {
        quest2.SetActive(false);
        questcomplete.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractNow = false;
        AiMobs.Monster = 0;
        Activequest = false;
        player.currentexp += 100;
        questended = true;
        if (!Activequest)
        {
            questlist.SetActive(false);
        }
    }

}
