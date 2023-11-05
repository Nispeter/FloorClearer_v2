using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private MeshCollider attackCollider;
    public float damage;

    private void Awake()
    {
        attackCollider = GetComponent<MeshCollider>();
        DeactivateAttack();
    }

    public void ActivateAttack(float damageAmount)
    {
        damage = damageAmount;
        attackCollider.enabled = true;
    }

    public void DeactivateAttack()
    {
        attackCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
            Debug.Log($"Damaged player for {damage} damage!");
            DeactivateAttack(); // Deactivate immediately after hitting the player to prevent multiple hits.
        }
    }
}
