using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    public GameObject questWindow;
    public GameObject questactive;
    public GameObject questcomplete;
    public GameObject questlist;
    public GameObject questend;
    [SerializeField] private Text QuestText;
    private Player player;
    private AiMobs aimobs;
    private bool questended = false;
    public Transform playerTransform;
    public Transform Ai;

    public bool InteractNow = false;
    public void Start()
    {
        aimobs = Ai.GetComponent<AiMobs>();
        player = playerTransform.GetComponent<Player>();
    }
    public void Update()
    {
        QuestText.text = "Pokonaj Bandytów " + "Pokonano: " + AiMobs.Bandyci + "/" + "10";
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
        else if (player.Activequest && AiMobs.Bandyci < 10 && !questended)
        {
            questactive.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            InteractNow = true;
        }
        else if (player.Activequest && AiMobs.Bandyci >= 10 && !questended)
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
        questcomplete.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractNow = false;
        AiMobs.Bandyci = 0;
        player.Activequest = false;
        player.currentexp += 100;
        questended = true;
        if (!player.Activequest)
        {
            questlist.SetActive(false);
        }
    }

}
