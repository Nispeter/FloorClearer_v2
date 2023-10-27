using UnityEngine;

public class Enemy_02 : Enemy
{
    public float moveSpeed = 3f;
    public float damageOnContact = 20f;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        remainingHealth = health;
    }

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthSystem playerHealth = other.gameObject.GetComponent<PlayerHealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageOnContact);
            }
        }
    }

     public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }


    public override void Die()
    {
        Destroy(this.gameObject);
    }
}