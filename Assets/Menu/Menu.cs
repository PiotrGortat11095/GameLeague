using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Menu1;
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
    public GameObject NPC;
    public GameObject AiCloning;
    public GameObject Mutant;
    public GameObject MutantBoss;
    private GameObject wizardInstance;
    public GameObject Skills;
    private GameObject warriorInstance;
    InventoryManager inventoryManager;
    QuestManager questManager;

    public bool wizard1;
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
        if (inventoryManager.IsOpen || inventoryManager.IsOpenS || questManager.IsOpen || inventoryManager.IsOpenSkills)
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
        if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS && !visible && !anyNPCInteractingNow && !questManager.IsOpen && !inventoryManager.IsOpenSkills)
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS && !anyNPCInteractingNow && !questManager.IsOpen && !inventoryManager.IsOpenSkills)
                {
                    visible = !visible;
                }
                if (inventoryManager.IsOpen || inventoryManager.IsOpenS || anyNPCInteractingNow || questManager.IsOpen || inventoryManager.IsOpenSkills)
                {
                    inventoryManager.IsOpen = false;
                    inventoryManager.IsOpenS = false;
                    questManager.IsOpen = false;
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
                Menu1.SetActive(visible);     
            }
        }
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
            Player player = warriorInstance.GetComponentInChildren<Player>();
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
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
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
            Player player = wizardInstance.GetComponentInChildren<Player>();
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
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
        }
    }
}
