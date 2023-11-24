using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Transform Player;
    public PlayerController pc;
    CharacterController characterController;
    public GameObject QuestList;
    public bool IsOpen;
    public bool up = false;
    Menu menu;
    Transform Menu1;

    private void Start()
    {
        QuestList.SetActive(false);
        IsOpen = false;
    }
    void Update()
    {
        if (Player != null)
        {
            Menu1 = GameObject.Find("Player").transform;
            menu = Menu1.GetComponent<Menu>();
            pc = Player.GetComponent<PlayerController>();
            characterController = Player.GetComponent<CharacterController>();
            if (IsOpen)
            {
                QuestList.SetActive(true);

            }
            else if (!IsOpen)
            {
                QuestList.SetActive(false);
            }
            Quest[] quests = GameObject.FindObjectsOfType<Quest>();
            Array.Sort(quests, (x, y) => x.questNumber.CompareTo(y.questNumber));
            NPCInteractable[] npcs = GameObject.FindObjectsOfType<NPCInteractable>();

            foreach (NPCInteractable npc in npcs)
            {
                if (npc.Activequest && !npc.QuestAct)
                {

                    foreach (Quest quest in quests)
                    {
                        if (!quest.fullslot)
                        {
                            TextMeshProUGUI tekstComponent = quest.transform.Find("Text").GetComponentInChildren<TextMeshProUGUI>();
                           
                                tekstComponent.text = npc.Dane;
                                quest.fullslot = true;
                                npc.QuestAct = true;
                                quest.questNumber = npc.QuestN;
                                npc.Change = true;
                                break;
                            
                        }
                    }
                }
                else if (!npc.Activequest && npc.QuestAct)
                {
                    foreach (Quest quest in quests)
                    {
                        if (quest.fullslot && npc.QuestN == quest.questNumber)
                        {
                            Destroy(quest.gameObject);
                            break;

                        }
                    }
                }
                else if (npc.Activequest && npc.Change && npc.QuestAct)
                {
                    foreach (Quest quest in quests)
                    {
                        if (quest.fullslot)
                        {
                            TextMeshProUGUI tekstComponent = quest.transform.Find("Text").GetComponentInChildren<TextMeshProUGUI>();
                            if (npc.QuestN == quest.questNumber)
                            {
                                tekstComponent.text = npc.Dane;
                                if (npc.questDescriptionList2 > 0)
                                {
                                    if (npc.Monster == npc.questDescriptionList2)
                                    {
                                        tekstComponent.color = Color.green;
                                    }
                                }
                                if(npc.questDescriptionList3 > 0)
                                {
                                    if(npc.Items == npc.questDescriptionList3)
                                    {
                                        tekstComponent.color = Color.green;
                                    }
                                }
                                break;
                            }

                        }
                    }


                }

            }





            if (Input.GetKeyDown(KeyCode.Q))
            {
                IsOpen = !IsOpen;

            }
        }
    }
}

