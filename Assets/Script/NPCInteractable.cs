using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool InteractNow = false;
    public Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void Awake()
    {
        NPC.SetActive(false);
    }
    public void Start()
    {
        aimobs = Ai.GetComponent<AiMobs>();
        player = playerTransform.GetComponent<Player>();
        quest1.SetActive(true);
        questDescription.text = questDescription1;
        questDescriptionActive.text = questDescription1;
        questDescriptionComplete.text = questDescriptionComplete1;
        questDescriptionend.text = questDescriptionEnd1;
    }
    public void Update()
    {
        
        QuestText.text = questDescriptionList1 + "Pokonano: " + AiMobs.Monster + "/" + questDescriptionList2;
        if (AiMobs.Monster >= Int32.Parse(questDescriptionList2))
        {
            quest2.SetActive(true);
        }
    
    }

    public void YesButton()
    {
        questWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractNow = false;
        player.Activequest = true;
        if (player.Activequest)
        {
            quest1.SetActive(false);
            questlist.SetActive(true);
        }
    }
    public void NoButton()
    {
        questWindow.SetActive(false);
        questend.SetActive(false);
        questactive.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractNow = false;
    }
    public void Interact()
    {
        if (!player.Activequest && !questended)
        {
            questWindow.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (player.Activequest && AiMobs.Monster < Int32.Parse(questDescriptionList2) && !questended)
        {
            questactive.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (player.Activequest && AiMobs.Monster >= Int32.Parse(questDescriptionList2) && !questended)
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
        player.Activequest = false;
        player.currentexp += 100;
        questended = true;
        if (!player.Activequest)
        {
            questlist.SetActive(false);
        }
    }

}
