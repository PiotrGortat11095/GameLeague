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
    public GameObject quest1;
    public GameObject quest2;
    public GameObject NPC;
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
    }
    public void Update()
    {
        QuestText.text = "Pokonaj Bandytów " + "Pokonano: " + AiMobs.Bandyci + "/" + "10";
        if (AiMobs.Bandyci >= 10)
        {
            quest2.SetActive(true);
        }
        if (target != null)
        {
            transform.LookAt(target);
        }
        if (player.Activequest)
        {
            questlist.SetActive(true);
        }
        else if (!player.Activequest)
        {
            questlist.SetActive(false);
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
        quest2.SetActive(false);
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
