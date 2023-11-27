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
    public GameObject questactive;
    public GameObject questcomplete;
    public GameObject questend;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject NPC;
    public GameObject RewardMage;
    public GameObject RewardWarrior;
    public Item ItemsMage;
    public Item ItemsWarrior;
    public string questDescription1;
    public string questDescriptionComplete1;
    public string questDescriptionEnd1;
    public int questDescriptionList2;
    public int questDescriptionList3;
    public string MonsterName;
    public string ItemName;
    public int Items = 0;
    public string Dane;
    public int QuestN;
    public int Monster = 0;
    public bool QuestAct = false;
    public bool Change = false;
    public Player player;
    public bool questended = false;
    public Transform playerTransform;
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
        
        quest1.SetActive(true);
    }
    public void Update()
    {
        AiMobs[] aimobs = GameObject.FindObjectsOfType<AiMobs>();
        if (aimobs != null)
        {
            foreach (AiMobs mob in aimobs)
            {
                if (Activequest && mob.MonsterName == MonsterName && mob.death == true && !mob.zliczony)
                {
                    mob.zliczony = true;
                    Monster++;
                    break;
                }
            }
        }

        if (playerTransform != null)
        {
            player = playerTransform.GetComponent<Player>();
        }
        if (questDescriptionList2 > 0)
        {
            if (Activequest)
            {
                if (Monster > questDescriptionList2)
                {
                    Monster = questDescriptionList2;
                }
                Dane = "Defeat " + MonsterName + " " + "Defeated: " + Monster + "/" + questDescriptionList2;
            }
            if (Monster >= questDescriptionList2)
            {
                quest2.SetActive(true);
            }
        }
        if (questDescriptionList3 > 0)
        {
            if (Activequest)
            {
                if (Items > questDescriptionList3)
                {
                    Items = questDescriptionList3;
                }
                Dane = "Collect " + ItemName + " " + "Collecteded: " + Items + "/" + questDescriptionList3;
                if (Items >= questDescriptionList3)
                {
                    quest2.SetActive(true);
                }
            }
        }
        Menu1 = GameObject.Find("Player").transform;
        menu = Menu1.GetComponent<Menu>();

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
        if (questDescriptionList2 > 0) {
            if (Activequest && Monster < questDescriptionList2 && !questended)
            {
                questactive.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                InteractNow = true;
            }
            if (Activequest && Monster >= questDescriptionList2 && !questended)
            {
                questcomplete.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                InteractNow = true;
            }
        }
        if (questDescriptionList3 > 0)
        {
            if (Activequest && Items < questDescriptionList3 && !questended)
            {
                questactive.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                InteractNow = true;
            }
            if (Activequest && Items >= questDescriptionList3 && !questended)
            {
                questcomplete.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                InteractNow = true;
            }
        }

        else if (questended)
        {
            questend.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
    }

}
