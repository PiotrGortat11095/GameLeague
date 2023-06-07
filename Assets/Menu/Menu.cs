using System.Collections;
using System.Collections.Generic;
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
    private GameObject warriorInstance;
    InventoryManager inventoryManager;

    public bool wizard1;
    public bool warrior1;
    public bool visible;
    Animator animator;
    private bool firstEsc = false;
    public GameObject questlist;
    public GameObject Allquest;
    public bool questactive;

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
        visible = true;
    }

    void Update()
    {
        if (wizardInstance != null)
        {
            Player player = wizardInstance.GetComponentInChildren<Player>();
            if (player.Activequest)
            {
                questactive = true;
            }
            else
            {
                questactive= false;
            }

        }
        if (warriorInstance != null)
        {
            Player player = warriorInstance.GetComponentInChildren<Player>();
            if (player.Activequest)
            {
                questactive = true;
            }
            else
            {
                questactive = false;
            }
        }
        if (inventoryManager.IsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!inventoryManager.IsOpen && !visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (firstEsc)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                visible = !visible;

                if (visible)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    questlist.SetActive(false);
                    Allquest.SetActive(false);
                }
                else if (!visible)
                {
                    Allquest.SetActive(true);
                    if (questactive)
                    {
                        questlist.SetActive(true);
                    }
                    else if (!questactive)
                    {
                        questlist.SetActive(false);
                    }

                }
                Menu1.SetActive(visible); 
                
            }
        }



    }
    public void wizard ()
    {
        questlist.SetActive(false);
        Allquest.SetActive(true);
        firstEsc = true;
        if (warriorInstance != null)
        {
            Destroy(warriorInstance) ;
            warrior1 = false;
            WarriorIMG.SetActive(false);
            Player player = warriorInstance.GetComponentInChildren<Player>();
            player.Activequest = false;
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
            NPC.SetActive(true);
            ball Ball = wizardInstance.GetComponentInChildren<ball>();
            Player player = wizardInstance.GetComponentInChildren<Player>();
            player.Phealthbar = Phealthbar;
            player.Pmanabar = Pmanabar;
            player.Pexpbar = Pexpbar;
            if (player.Activequest)
            {
                questactive = true;
            }
            else if (!player.Activequest)
            {
                questactive = false;
            }
            animator = wizardInstance.GetComponent<Animator>();
            animator.enabled = true;
            PlayerController controller = wizardInstance.GetComponentInChildren<PlayerController>();
            controller.player = wizardInstance.transform;
            Ball.mainCamera = maincamera;
            player.mainCamera = maincamera;
            Crosshair.SetActive(true);
            HP.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            NPCInteractable npc = NPC.GetComponent<NPCInteractable>();
            npc.playerTransform = wizardInstance.transform;
        }
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
        }
    }
    public void warrior()
    {
        questlist.SetActive(false);
        Allquest.SetActive(true);
        firstEsc = true;
        if (wizardInstance != null)
        {
            Destroy(wizardInstance);
            wizard1 = false;
            WizardIMG.SetActive(false);
            Player player = wizardInstance.GetComponentInChildren<Player>();
            player.Activequest = false;
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
            NPC.SetActive(true);
            warriorInstance = Instantiate(Warrior, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(warriorInstance.transform);
            Player player = warriorInstance.GetComponentInChildren<Player>();
            player.Phealthbar = Phealthbar;
            player.Pmanabar = Pmanabar;
            player.Pexpbar = Pexpbar;
            if (player.Activequest)
            {
                questactive = true;
            }
            else if (!player.Activequest)
            {
                questactive = false;
            }
            animator = warriorInstance.GetComponent<Animator>();
            animator.enabled = true;
            PlayerController controller = warriorInstance.GetComponentInChildren<PlayerController>();
            controller.player = warriorInstance.transform;
            player.mainCamera = maincamera;
            Crosshair.SetActive(true);
            HP.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            NPCInteractable npc = NPC.GetComponent<NPCInteractable>();
            npc.playerTransform = warriorInstance.transform;
        }
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
        }
    }

}
