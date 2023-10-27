using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrystalShard : MonoBehaviour
{
    public float projectileSpeed = 20f; // Increased speed for rapid movement.
    public float randomDeviation = 0.1f; // The amount of random deviation in the projectile direction.

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Transform cam, Vector3 direction)
    {
        // Add a slight random deviation to direction.
        Vector3 randomizedDirection = direction + new Vector3(
            Random.Range(-randomDeviation, randomDeviation),
            Random.Range(-randomDeviation, randomDeviation),
            Random.Range(-randomDeviation, randomDeviation)
        );

        randomizedDirection.Normalize(); // Ensure the direction vector has a magnitude of 1.

        transform.rotation = Quaternion.LookRotation(randomizedDirection); // Make the projectile face its direction.
        
        rb.AddForce(randomizedDirection * projectileSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Here you can add any condition, for example, if you want to ignore certain tags.
        // if(other.CompareTag("SomeTag")) return;

        Destroy(gameObject); // Destroy the shard upon triggering an event.
    }
}
