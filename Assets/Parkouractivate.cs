using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkouractivate : MonoBehaviour
{
    public GameObject Parkour;
    public GameObject Npc;
    void Update()
    {
        NPCInteractable npc = Npc.GetComponent<NPCInteractable>();
        if (npc.Activequest)
        {
            Parkour.SetActive(true);
        }
        else
        {
            Parkour.SetActive(false);
        }
    }
}
