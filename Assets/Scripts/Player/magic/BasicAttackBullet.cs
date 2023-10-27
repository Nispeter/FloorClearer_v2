using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBullet : Projectile
{
    public float lifetime = 30f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(proyectileDamage);
            }
        }   
        if(other.gameObject.tag != "Player"){
            Destroy(gameObject);
        }

    }
}
