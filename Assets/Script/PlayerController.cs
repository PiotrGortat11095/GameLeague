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
    private ball skillScript;
    private bool isAttacking;
    float ySpeed;
    public bool block;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    public Transform player;
    Quaternion targetRotation;
    public Transform NPC;
    private InventoryManager inventoryManager;
    private NPCInteractable npcInteractable;
    public Transform ekwipunek;
    public Transform MEnu1;
    Menu menu;

    CameraController cameraController;
    Animator animator;
    public CharacterController characterController;

    private void Start()
    {
        ekwipunek = GameObject.Find("Canvas").transform;
        inventoryManager = ekwipunek.GetComponent<InventoryManager>();
        NPC = GameObject.Find("Ch34_nonPBR").transform;
        npcInteractable = NPC.GetComponentInChildren<NPCInteractable>();
        playerScript = player.GetComponent<Player>();
        skillScript = player.GetComponent<ball>();

        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        MEnu1 = GameObject.Find("Player").transform;
        menu = MEnu1.GetComponent<Menu>();
    }
    private void Update()
    {

        if (!npcInteractable.InteractNow)
        {

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (inventoryManager.IsOpen || inventoryManager.IsOpenS)
            {
                h = 0;
                v = 0;
            }
            float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));


            var moveInput = (new Vector3(h, 0, v)).normalized;
            var moveDir = cameraController.PlanarRotation * moveInput;



            ySpeed += Physics.gravity.y * Time.deltaTime;
            if (moveAmount <= 0)
            {
                transform.rotation = Quaternion.Euler(0, cameraController.transform.eulerAngles.y, 0);
            }
            if (characterController.isGrounded)
            {
                animator.SetBool("Grounded", true);
            }
            else
            {
                animator.SetBool("Grounded", false);
            }
           
            if (!inventoryManager.IsOpen && !inventoryManager.IsOpenS)
            {
                if (Input.GetButton("Ultimate") && characterController.isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("JumpLand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Idle") && playerScript.PcurrentMana >= 30)
                {
                    if (skillScript.Enemy)
                    {
                        animator.SetBool("Ultimate", true);
                        isAttacking = true;
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

            if (Input.GetButtonDown("Jump") && !isJumping && !inventoryManager.IsOpen && !inventoryManager.IsOpenS)
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

                if ((isJumping && ySpeed < 0) || ySpeed < -2)
                {
                    animator.SetBool("FreeFall", true);
                }
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
