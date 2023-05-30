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
    private GameObject wizardInstance;
    private GameObject warriorInstance;
    public bool wizard1;
    public bool warrior1;
    public bool visible;
    Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            Cursor.visible = visible;
            if (visible)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else if(!visible) 
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Menu1.SetActive(visible);
        }
    }
    public void wizard ()
    {
        if (warriorInstance != null)
        {
            Destroy(warriorInstance) ;
            warrior1 = false;
        }
        if (!wizard1)
        {
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
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
        }
    }
    public void warrior()
    {
        if(wizardInstance != null)
        {
            Destroy(wizardInstance);
            wizard1 = false;
        }
        if (!warrior1)
        {
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
        else
        {
            Debug.Log("Aktualnie grasz t¹ klas¹");
        }
    }

}
