using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : PlayerProjectile
{
    [SerializeField] private GameObject iceSurfacePrefab; // Make sure to link this in the inspector or set it up before the shard is launched

    private Rigidbody rb;
    private Vector3 moveDirection;

    public void Setup(Transform cam, float damage)
    {
        transform.rotation = cam.rotation;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 30f;
        lifeTime = 30f;
        moveDirection = cam.forward.normalized;
        damage = damage;

        rb.velocity = moveDirection * moveSpeed;
    }

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile")
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                Debug.Log($"Collided with: {collision.gameObject.name}, Tag: {collision.gameObject.tag}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
                SpawnIceSurface();
            }
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            Destroy(gameObject, lifeTime);
        }
    }

    void SpawnIceSurface()
    {
        GameObject surface = Instantiate(iceSurfacePrefab, transform.position, Quaternion.identity);
        surface.GetComponent<ISurface>().SetLifetime(5f);
    }
}