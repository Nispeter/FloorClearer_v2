using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrystalShard : PlayerProjectile
{
    public float projectileSpeed = 20f;
    public float randomDeviation = 0.1f;
    public float bounceMultiplier = 0.8f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Destroy the projectile after the set lifetime
        Destroy(gameObject, lifeTime);
    }

    public void Launch(Transform cam, Vector3 direction)
    {
        Vector3 randomizedDirection = direction + new Vector3(
            Random.Range(-randomDeviation, randomDeviation),
            Random.Range(-randomDeviation, randomDeviation),
            Random.Range(-randomDeviation, randomDeviation)
        );

        randomizedDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(randomizedDirection);
        rb.AddForce(randomizedDirection * projectileSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.AddForce(reflectedVelocity * projectileSpeed * bounceMultiplier, ForceMode.VelocityChange);
    }

}
