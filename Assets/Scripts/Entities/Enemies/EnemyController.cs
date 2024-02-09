
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Enemy
{
    [Header("Pathfinding params")]
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patroling params")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking params")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Animator animator;
    private bool _isDead = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_isDead)
            return;
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) 
            Patroling();
        else 
            animator.SetBool("Walk Forward", false);

        if (playerInSightRange && !playerInAttackRange) 
            ChasePlayer();
        else 
            animator.SetBool("Run Forward", false);

        if (playerInAttackRange && playerInSightRange) 
            AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        //Action Animation
        animator.SetBool("Walk Forward", walkPointSet);
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        //Action Animation
        animator.SetBool("Run Forward", true);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        float heightOffset = 0f;
        Vector3 adjustedPosition = new Vector3(player.position.x, player.position.y + heightOffset, player.position.z);
        transform.LookAt(adjustedPosition);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            //Action Animation
            animator.SetTrigger("Stab Attack");
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public override void TakeDamage(float damage)
    {
        if(_isDead)
            return;
        _remainingHealth -= damage;

        //Action Animation Deactivate
        animator.SetBool("Run Forward", false);
        animator.SetBool("Walk Forward", false);
        animator.SetTrigger("Take Damage");

        if (_remainingHealth <= 0)
        {
            //Action Animation
            animator.SetTrigger("Die");

            StatusEffects.Add(StatusEffect.Dead);
            _isDead = true;

            Die();
            agent.enabled = false;
            InGameManager.Instance.pointCounter.AddPoints(pointsOnKill);
        }
    }
}
