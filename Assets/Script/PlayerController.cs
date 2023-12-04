using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float jumpButtonGracePeriod;
    [SerializeField] private float jumpSpeed;
    private Player playerScript;
    public GameObject Respawn;
    private ball skillScript;
    private bool isAttacking;
    float ySpeed;
    public bool block;
    public bool ultimate;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    public Transform player;
    Quaternion targetRotation;
    private InventoryManager inventoryManager;
    private QuestManager questManager;
    public Transform ekwipunek;
    public Transform MEnu1;
    Menu menu;
    bool anyNPCInteractingNow;

    CameraController cameraController;
    Animator animator;
    public CharacterController characterController;

    private void Start()
    {
        if (player != null)
        {
            ekwipunek = GameObject.Find("Canvas").transform;
            inventoryManager = ekwipunek.GetComponent<InventoryManager>();
            questManager = ekwipunek.GetComponent<QuestManager>();
            NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
            playerScript = player.GetComponent<Player>();
            skillScript = player.GetComponent<ball>();
            cameraController = Camera.main.GetComponent<CameraController>();
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            MEnu1 = GameObject.Find("Player").transform;
            menu = MEnu1.GetComponent<Menu>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            transform.position = Respawn.transform.position;
        }
    }
    private void Update()
    {

        NPCInteractable[] npc = GameObject.FindObjectsOfType<NPCInteractable>();
        anyNPCInteractingNow = false;
        foreach (NPCInteractable singleNPC in npc)
        {
            if (singleNPC.InteractNow)
            {
                anyNPCInteractingNow = true;
                break;
            }
        }
        if (!anyNPCInteractingNow)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (inventoryManager.IsOpen || inventoryManager.IsOpenS || inventoryManager.IsOpenSkills)
            {
                h = 0;
                v = 0;
            }
            float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
            var moveInput = (new Vector3(h, 0, v)).normalized;
            var moveDir = cameraController.PlanarRotation * moveInput;
            if (ySpeed >= Physics.gravity.y && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk") && !isAttacking)
            {
                ySpeed += Physics.gravity.y * Time.deltaTime;
            }
            if (moveAmount <= 0)
            {
                transform.rotation = Quaternion.Euler(0, cameraController.transform.eulerAngles.y, 0);
            }
            if (characterController.isGrounded)
            {
                animator.SetBool("Grounded", true);
                ySpeed = 0;
            }
            else
            {
                animator.SetBool("Grounded", false);
            }
            if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS && !inventoryManager.IsOpenSkills)
            {
                if (ultimate && characterController.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpLand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle") && playerScript.PcurrentMana >= 30)
                {
                    if (skillScript.Enemy)
                    {
                        animator.SetBool("Ultimate", true);
                        isAttacking = true;
                        SkillPrefab.currentCDTime = SkillPrefab.CDTime;
                    }
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Meteor"))
                {
                    animator.SetBool("Ultimate", false);
                    isAttacking = false;
                }
                if (Input.GetButton("Skill1") && characterController.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpLand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle") && playerScript.PcurrentMana >= 10)
                {
                    animator.SetBool("Skill1", true);
                    isAttacking = true;

                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("MagmaWall") || animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
                {
                    animator.SetBool("Skill1", false);
                    isAttacking = false;

                }
                if (Input.GetMouseButton(0) && characterController.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpLand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle"))
                {
                    animator.SetBool("Attack", true);
                    isAttacking = true;
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk"))
                {
                    animator.SetBool("Attack", false);
                    isAttacking = false;
                }
                if (Input.GetMouseButton(1) && characterController.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpLand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle") && playerScript.PcurrentMana >= 10 && menu.warrior1)
                {
                    animator.SetBool("BlockStart", true);
                    isAttacking = true; ;
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block") && !Input.GetMouseButton(1))
                {
                    animator.SetBool("BlockStart", false);
                    isAttacking = false;

                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Block") && playerScript.PcurrentMana < 1)
                {
                    animator.SetBool("BlockStart", false);
                    isAttacking = false;
                }
                if (isAttacking)
                {
                    transform.rotation = Quaternion.Euler(0, cameraController.transform.eulerAngles.y, 0);
                }
            }
            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }
            if (Input.GetButtonDown("Jump") && !isJumping && !inventoryManager.IsOpen && !inventoryManager.IsOpenS && !inventoryManager.IsOpenSkills)
            {
                jumpButtonPressedTime = Time.time;
            }
            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                characterController.stepOffset = originalStepOffset;
                animator.SetBool("Grounded", true);
                animator.SetBool("Jump", false);
                isJumping = false;
                animator.SetBool("FreeFall", false);
                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
                {
                    if (!isJumping)
                    {
                        animator.SetBool("Jump", true);
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    {
                        animator.SetBool("Attack", false);
                        isAttacking = false;
                        ySpeed = jumpSpeed;
                        isJumping = true;
                        jumpButtonPressedTime = null;
                        lastGroundedTime = null;
                    }
                }
            }
            else
            {
                characterController.stepOffset = 0;
                animator.SetBool("Grounded", false);
            }
                if ((isJumping && ySpeed < 0) || ySpeed < -2)
                {
                animator.SetBool("Grounded", false);
                animator.SetBool("FreeFall", true);

                }
            
            if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("MagmaWall") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Meteor") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
            {
                var velocity = moveDir * moveSpeed;
                velocity.y = ySpeed;
                characterController.Move(velocity * Time.deltaTime);
            }
            else
            {
                characterController.Move(Vector3.zero);
            }
            if (moveAmount > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("MagmaWall") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Meteor") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
            {
                targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
        }
    }
}
