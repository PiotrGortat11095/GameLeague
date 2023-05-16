using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AiMobs : MonoBehaviour
{
    [SerializeField] Healthbar healthbar;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private bool death;

    public float health = 2;
    private float currentHealth;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Animator animator;
    private void Start()
    {
        currentHealth = health;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetFloat("Speed", 0.5f);
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            animator.SetFloat("Speed", 1f);
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            animator.SetFloat("Speed", 0f);
        }
        healthbar.UpdateHealthBar(health, currentHealth);

    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    public void FireProjectile()
    {
        Rigidbody rb = Instantiate(projectile, transform.position + transform.forward, Quaternion.LookRotation(-transform.forward)).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 6f, ForceMode.Impulse);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked && playerInSightRange && playerInAttackRange && !death)
        {
            animator.SetTrigger("Attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            death = true;
            animator.SetBool("Death", true);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
