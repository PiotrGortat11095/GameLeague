using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public GameObject Player;
    public Text questDescription;
    public Text questDescriptionActive;
    public Text questDescriptionComplete;
    public Text questDescriptionend;
    private Menu menu;
    void Update()
    {
        menu = Player.GetComponent<Menu>();
        menu.enabled = true;
        NPCInteractable[] npcs = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable npc in npcs)
        {
            if (npc.InteractNow)
            {
                questDescription.text = npc.questDescription1;
                questDescriptionActive.text = npc.questDescription1;
                questDescriptionComplete.text = npc.questDescriptionComplete1;
                questDescriptionend.text = npc.questDescriptionEnd1;
                break;
            }
        }
    }
}
