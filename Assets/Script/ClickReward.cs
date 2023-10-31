using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReward : MonoBehaviour, IPointerClickHandler
{

    public GameObject questlist;
    public GameObject questcomplete;
    public GameObject Canvas;
    Menu menu;
    public void Start()
    {
        menu = GameObject.Find("Player").GetComponent<Menu>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                GameObject quest2 = singleNPC.transform.Find("Canvas/quest2").gameObject;
                quest2.SetActive(false);
                questcomplete.SetActive(false);
                if (menu.warrior1)
                {
                    GameObject Reward = Instantiate(singleNPC.RewardWarrior);
                    Reward.transform.SetParent(Canvas.transform);
                }
                else if (menu.wizard1)
                {
                    GameObject Reward = Instantiate(singleNPC.RewardMage);
                    Reward.transform.SetParent(Canvas.transform);
                }
                singleNPC.Monster = 0;
                singleNPC.Activequest = false;
                singleNPC.player.currentexp += 100;
                singleNPC.questended = true;
                break;
            }
        }
    }
}
