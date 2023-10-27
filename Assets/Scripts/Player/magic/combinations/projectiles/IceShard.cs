using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : Projectile
{
    private Rigidbody rb;
    private Vector3 moveDirection;

    public void Setup(Transform cam, float damage){
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
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile"){
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            Destroy(gameObject, lifeTime);
        }
        
        // Optionally, add code here to handle other collision-related behavior
    }
}
