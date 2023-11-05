using UnityEngine;

public interface IAttackBehaviour
{
    float AttackRange { get; }
    bool ShouldAttack(float distanceToPlayer);
    void Attack();
}

public class Enemy_03 : Enemy
{
    [Header("Movement and Interaction")]
    [SerializeField] private float followRange = 300f;
    [SerializeField] private float moveSpeed = 3f;
    private IAttackBehaviour attackBehaviour;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackBehaviour = GetComponent<IAttackBehaviour>();
    }

    public void Start()
{
    remainingHealth = health;
}

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (attackBehaviour.ShouldAttack(distanceToPlayer))
        {
            attackBehaviour.Attack();
        }
        else if (distanceToPlayer <= followRange && distanceToPlayer > attackBehaviour.AttackRange)
        {
            FollowPlayer();
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        Debug.Log("Enemy_03 has died!");
        Destroy(this.gameObject);
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
