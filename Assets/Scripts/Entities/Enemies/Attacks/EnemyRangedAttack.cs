using UnityEngine;

public class EnemyRangedAttack : Factory, IAttackBehaviour
{
    [Header("Attack Properties")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 1f; // The number of projectiles per second
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackCooldown = 2f;
    private float _fireCooldown;

    public float AttackRange => _attackRange;

    public bool ShouldAttack(float distanceToTarget)
    {
        return distanceToTarget <= _attackRange && _fireCooldown <= 0;
    }

    public void Attack()
    {
        InstantiateProjectile(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        _fireCooldown = _attackCooldown;
    }

    private void Update()
    {
        _fireCooldown -= Time.deltaTime;
    }

    private void InstantiateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        InstantiatePrefab<GameObject>(projectilePrefab, position, rotation);
    }
}