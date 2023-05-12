using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float jumpButtonGracePeriod;
    [SerializeField] bool isAttacking;
    [SerializeField] private float jumpSpeed;


    float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
        if (Input.GetMouseButton(0) && characterController.isGrounded)
        {
            animator.SetBool("Attack", true);
            isAttacking = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk"))
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
        }


        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            animator.SetBool("Grounded", true);
            isGrounded = true;
            animator.SetBool("Jump", false);
            isJumping = false;
            animator.SetBool("FreeFall", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                animator.SetBool("Jump", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
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
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("FreeFall", true);
            }
        }

        if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attackk"))
        {
            var velocity = moveDir * moveSpeed;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.zero);
        }

        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }
        if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }   
        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

}
