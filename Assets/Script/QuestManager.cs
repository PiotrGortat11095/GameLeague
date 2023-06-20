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
    Menu menu;
    bool foundMatchingNPC = false;
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
            if (IsOpen && menu.visible)
            {
                QuestList.SetActive(false);
            }
            else if (IsOpen && !menu.visible)
            {
                QuestList.SetActive(true);

            }
            else if (!IsOpen)
            {
                QuestList.SetActive(false);
            }
            Quest[] quest = GameObject.FindObjectsOfType<Quest>();
            Debug.Log(quest.Length);
            foreach (Quest quest1 in quest)
            {
                Debug.Log(quest1);
                    if (quest1.fullslot == false) { }
                    TextMeshProUGUI tekstComponent = quest1.transform.Find("Text").GetComponentInChildren<TextMeshProUGUI>();
                    if (tekstComponent.text != null && string.IsNullOrEmpty(tekstComponent.text))
                    {
                        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
                        foreach (NPCInteractable npc1 in npc)
                        {
                            if (npc1.Activequest && !foundMatchingNPC)
                            {
                                tekstComponent.text = npc1.QuestText.text;
                                foundMatchingNPC = true;
                            quest1.fullslot = true;
                            break;
                            }
                        }
                    }
                break;
            }
            if (Input.GetKeyDown(KeyCode.Q) && pc.characterController.isGrounded)
            {
                IsOpen = !IsOpen;
               
            }
        }
    }
}
