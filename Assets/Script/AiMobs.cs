using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AiMobs : MonoBehaviour
{
    [SerializeField] Healthbar healthbar;
    public GameObject FloatingTextPrefab;
    public NavMeshAgent agent;
    public int AIdamage = 40;
    public Transform player;
    public float exp;
    public LayerMask whatIsGround, whatIsPlayer;
    private Vector3 walkPoint;
    bool walkPointSet;

    private AiCloning aicloning;
    public float walkPointRange;
    private NPCInteractable interactable;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool Triggernow = false;
    public GameObject projectile;
    private bool death;
    private float DeathTime = 1f;
    public float health = 2;
    private float currentHealth;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Animator animator;
    private Player player1;
    public Transform NPC;
    public Transform maincamera;
    private Menu menu;
    public Transform MEnu1;


    public static int Bandyci = 0;

    private Rigidbody rb;

    private void Start()
    {
        maincamera = GameObject.Find("MainCamera").transform;
        if (menu.wizard1)
        {
            player = GameObject.Find("Wizard(Clone)").transform;
        }
        if (menu.warrior1)
        {
            player = GameObject.Find("Warrior(Clone)").transform;
        }
        aicloning = FindObjectOfType<AiCloning>();
        currentHealth = health;
        player1 = player.GetComponent<Player>();
        interactable = NPC.GetComponent<NPCInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        MEnu1 = GameObject.Find("Player").transform;
        menu = MEnu1.GetComponent<Menu>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        

    }
    private void Update()
    {
        if (menu.wizard1)
        {
            player = GameObject.Find("Wizard(Clone)").transform;
        }
        if (menu.warrior1)
        {
            player = GameObject.Find("Warrior(Clone)").transform;
        }
        player1 = player.GetComponent<Player>();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange && !death)
        {
            Patroling();
            animator.SetFloat("Speed", 0.5f);
        }
        if (playerInSightRange && !playerInAttackRange && !death)
        {
            ChasePlayer();
            animator.SetFloat("Speed", 1f);
        }
        if (playerInSightRange && playerInAttackRange && !death)
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("MutantAttack"))
        {
            agent.SetDestination(player.position);
        }
    }
    public void FireProjectile()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Rigidbody rb = Instantiate(projectile, transform.position + directionToPlayer, Quaternion.LookRotation(-directionToPlayer)).GetComponent<Rigidbody>();
        rb.AddForce(directionToPlayer * 3.2f, ForceMode.Impulse);
        rb.AddForce(transform.up * 0.5f, ForceMode.Impulse);
        
    }
    private void AttackPlayer()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("MutantAttack"))
        {
            agent.SetDestination(transform.position);
        }
            Vector3 playerPositionSameY = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(playerPositionSameY);

            if (!alreadyAttacked && playerInSightRange && playerInAttackRange && !death)
            {
                animator.SetBool("Attack", true);

                alreadyAttacked = true;
            
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else
            {

                animator.SetBool("Attack", false);
            }
        
    }
    private void AttackTrigger()
    {
        Triggernow = true;
    }
    private void EndAttackTrigger()
    {
        Triggernow = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (alreadyAttacked && Triggernow)
            {
                player.TakeDamage(AIdamage);
            }
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        if (!death)
        {
            currentHealth -= damage;
            if (FloatingTextPrefab)
            {
                Vector3 pop = new Vector3(transform.position.x, transform.position.y +2, transform.position.z);
                var go = Instantiate(FloatingTextPrefab, pop, Quaternion.identity, transform);
                go.GetComponent<TextMesh>().text = damage.ToString();
                go.transform.LookAt(maincamera);
                go.transform.Rotate(0, 180, 0);
                go.transform.position += new Vector3(Random.Range(-0.2f, 0.2f),
                Random.Range(0, 0),
                Random.Range(0, 0));


            }
        }
        if (currentHealth <= 0 && !death)
        {
            death = true;
            player1.currentexp += exp;
            animator.SetBool("Death", true);
            agent.enabled = false;
            if (player1.Activequest)
            {
                if (AiMobs.Bandyci < 10)
                {
                    AiMobs.Bandyci++;
                }
                
            }
            else if (!player1.Activequest)
            {
                AiMobs.Bandyci = 0;
            }
        }
    }

    private void DestroyEnemy()
    {
        Invoke(nameof(DestroyE), DeathTime);
    }
    private void DestroyE()
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
