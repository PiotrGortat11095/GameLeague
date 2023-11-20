using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Menu : MonoBehaviour
{
    public GameObject Menu1;
    public GameObject Esc;
    public GameObject WizardIMG;
    public GameObject WarriorIMG;
    public GameObject Wizard;
    public GameObject Warrior;
    public GameObject Crosshair;
    public GameObject HP;
    public HealthbarP Phealthbar;
    public ManabarPp Pmanabar;
    public Expbar Pexpbar;
    public Camera maincamera;
    public GameObject AiCloning;
    public GameObject Mutant;
    public GameObject MutantBoss;
    private GameObject wizardInstance;
    public GameObject Skills;
    private GameObject warriorInstance;
    InventoryManager inventoryManager;
    QuestManager questManager;

    public bool wizard1;
    public bool resume = false;
    public bool warrior1;
    public bool visible;
    Animator animator;
    private bool firstEsc = false;
    public GameObject Allquest;
    bool anyNPCInteractingNow = false;

    private void Awake()
    {
        Crosshair.SetActive(false);
        HP.SetActive(false);
        Menu1.SetActive(true);
        Esc.SetActive(false);
        Allquest.SetActive(false);
    }
    private void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
        visible = true;
    }

    void Update()
    {

        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        if (wizardInstance != null)
        {
            inventoryManager.Player = wizardInstance.transform;
            questManager.Player = wizardInstance.transform;
            Player player = wizardInstance.GetComponentInChildren<Player>();
        }
        if (warriorInstance != null)
        {
            inventoryManager.Player = warriorInstance.transform;
            questManager.Player=warriorInstance.transform;
            Player player = warriorInstance.GetComponentInChildren<Player>();
        }

        if (inventoryManager.IsOpen || inventoryManager.IsOpenS || inventoryManager.IsOpenSkills)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        anyNPCInteractingNow = false;
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                anyNPCInteractingNow = true;
                break;
            }
        }
        if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS && !visible && !anyNPCInteractingNow  && !inventoryManager.IsOpenSkills)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (!visible && anyNPCInteractingNow)
        {
            Allquest.SetActive(true);
        }
        if (firstEsc)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || resume)
            {
                if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS && !anyNPCInteractingNow && !inventoryManager.IsOpenSkills)
                {
                    visible = !visible;
                }
                if (inventoryManager.IsOpen || inventoryManager.IsOpenS || anyNPCInteractingNow || inventoryManager.IsOpenSkills)
                {
                    inventoryManager.IsOpen = false;
                    inventoryManager.IsOpenS = false;
                    inventoryManager.IsOpenSkills = false;
                    foreach (NPCInteractable singleNPC in npc)
                    {
                        if (singleNPC.InteractNow)
                        {
                            singleNPC.InteractNow = false;
                            anyNPCInteractingNow = false;
                        }
                    }
                    Allquest.SetActive(false);
                }

                if (visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Allquest.SetActive(false);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                Esc.SetActive(visible);
                resume = false;
            }
        }
        if(visible)
        {
            Time.timeScale = 0.0f;
        }
        else if (!visible)
        {
            Time.timeScale = 1.0f;
        }
    }
    public void ResumeGame()
    {
        resume = true;
        Time.timeScale = 1.0f;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void wizard ()
    {    
        Allquest.SetActive(true);
        firstEsc = true;
        if (warriorInstance != null)
        {
            Destroy(warriorInstance) ;
            warrior1 = false;
            WarriorIMG.SetActive(false);
        }
        if (!wizard1)
        {
            WizardIMG.SetActive(true);
            visible = false;
            wizardInstance = Instantiate(Wizard, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(wizardInstance.transform);
            wizard1 = true;
            Menu1.SetActive(false);
            AiCloning.SetActive(true);
            Mutant.SetActive(true);
            MutantBoss.SetActive(true);
            NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
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
            Ball.Skills = Skills;
            player.mainCamera = maincamera;
            Crosshair.SetActive(true);
            HP.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
                foreach (NPCInteractable npc2 in npc)
            {
                npc2.playerTransform = wizardInstance.transform;
            }
        }
    }
    public void warrior()
    {
        Allquest.SetActive(true);
        firstEsc = true;
        if (wizardInstance != null)
        {
            Destroy(wizardInstance);
            wizard1 = false;
            WizardIMG.SetActive(false);
        }
        if (!warrior1)
        {
            WarriorIMG.SetActive(true);
            visible = false;
            warrior1 = true;
            Menu1.SetActive(false);
            AiCloning.SetActive(true);
            Mutant.SetActive(true);
            MutantBoss.SetActive(true);
            NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
            warriorInstance = Instantiate(Warrior, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(warriorInstance.transform);
            Player player = warriorInstance.GetComponentInChildren<Player>();
            player.Phealthbar = Phealthbar;
            player.Pmanabar = Pmanabar;
            player.Pexpbar = Pexpbar;
            animator = warriorInstance.GetComponent<Animator>();
            animator.enabled = true;
            PlayerController controller = warriorInstance.GetComponentInChildren<PlayerController>();
            controller.player = warriorInstance.transform;
            player.mainCamera = maincamera;
            Crosshair.SetActive(true);
            HP.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            foreach (NPCInteractable npc2 in npc)
            {
                npc2.playerTransform = warriorInstance.transform;
            }
        }

    }
}
