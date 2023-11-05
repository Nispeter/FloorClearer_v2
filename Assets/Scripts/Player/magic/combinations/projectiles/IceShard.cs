using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : PlayerProjectile
{
    private Rigidbody rb;
    private Vector3 moveDirection;

    public void Setup(Transform cam, float damage)
    {
        transform.rotation = cam.rotation;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 10f;
        lifeTime = 30f;
        moveDirection = cam.forward.normalized;
        proyectileDamage = damage;

        rb.velocity = moveDirection * moveSpeed;
        // float angle = Mathf.Atan2(moveDirection.y, moveDirection.x);
        // transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }
    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile")
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            Destroy(gameObject, lifeTime);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(proyectileDamage);
            }
        }
    }
}
