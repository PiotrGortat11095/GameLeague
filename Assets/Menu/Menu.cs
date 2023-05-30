using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Menu1;
    public GameObject Wizard;
    public GameObject Warrior;
    public GameObject Crosshair;
    public GameObject HP;
    public HealthbarP Phealthbar;
    public ManabarPp Pmanabar;
    public Expbar Pexpbar;
    public Camera maincamera;
    public GameObject NPC;
    public GameObject AiCloning;
    public GameObject Mutant;
    public GameObject MutantBoss;
    public bool wizard1;
    public bool warrior1;
    Animator animator;

    public void Update()
    {

    }
    public void wizard ()
    {
        wizard1 = true;
        Menu1.SetActive(false);
        AiCloning.SetActive(true);
        Mutant.SetActive(true);
        MutantBoss.SetActive(true);
        NPC.SetActive(true);
        GameObject wizardInstance = Instantiate(Wizard, transform.position, Quaternion.identity);
        gameObject.transform.SetParent(wizardInstance.transform);
        ball Ball = wizardInstance.GetComponentInChildren<ball>();
        Player player = wizardInstance.GetComponentInChildren<Player>();
        player.Phealthbar = Phealthbar;
        player.Pmanabar = Pmanabar;
        player.Pexpbar = Pexpbar;
        animator = wizardInstance.GetComponent<Animator>();
        animator.enabled = true;
        PlayerController controller = wizardInstance.GetComponentInChildren<PlayerController>();
        controller.player = wizardInstance.transform;
        Ball.mainCamera = maincamera;
        Crosshair.SetActive(true);
        HP.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        NPCInteractable npc = NPC.GetComponent<NPCInteractable>();
        npc.playerTransform = wizardInstance.transform;
    }
    public void warrior()
    {
        warrior1 = true;
        Menu1.SetActive(false);
        AiCloning.SetActive(true);
        Mutant.SetActive(true);
        MutantBoss.SetActive(true);
        NPC.SetActive(true);
        GameObject warriorInstance = Instantiate(Warrior, transform.position, Quaternion.identity);
        gameObject.transform.SetParent(warriorInstance.transform);
        Player player = warriorInstance.GetComponentInChildren<Player>();
        player.Phealthbar = Phealthbar;
        player.Pmanabar = Pmanabar;
        player.Pexpbar = Pexpbar;
        animator = warriorInstance.GetComponent<Animator>();
        animator.enabled = true;
        PlayerController controller = warriorInstance.GetComponentInChildren<PlayerController>();
        controller.player = warriorInstance.transform;
        Crosshair.SetActive(true);
        HP.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        NPCInteractable npc = NPC.GetComponent<NPCInteractable>();
        npc.playerTransform = warriorInstance.transform;
    }

}
