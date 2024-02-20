
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
    protected bool alreadyAttacked;
    public GameObject attack;
    private IAttack _iattack;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    protected Animator animator;
    private bool _isDead = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        _iattack = attack.GetComponent<IAttack>();
    }

    private void Update()
    {
        if (_isDead)
            return;
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        bool _isBeingDamaged = animator.GetCurrentAnimatorStateInfo(0).IsName("Take Damage");

        if (!playerInSightRange && !playerInAttackRange && !_isBeingDamaged)
            Patroling();
        else
            animator.SetBool("Walk Forward", false);

        if (playerInSightRange && !playerInAttackRange && !_isBeingDamaged)
            ChasePlayer();
        else
            animator.SetBool("Run Forward", false);

        if (playerInAttackRange && playerInSightRange && !_isBeingDamaged)
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

    public virtual void AttackPlayer()
    {

        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Stab Attack");
            StartCoroutine(_iattack.ActivateDamageCollider(0.1f, 0.5f));

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }
    protected void ResetAttack()
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
        if (_isDead)
            return;
        _remainingHealth -= damage;

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
        animator.SetTrigger("Take Damage");
    }
}
