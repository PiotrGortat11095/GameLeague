using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisItem : MonoBehaviour
{
    public Item przedmiotDoDodania;
    public void Update()
    {
        NPCInteractable[] npcs = GameObject.FindObjectsOfType<NPCInteractable>();
        foreach (NPCInteractable npc in npcs)
        {
            if (npc.questended && npc.ItemName == przedmiotDoDodania.Name)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
