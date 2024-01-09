using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile :  Projectile
{
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IHealth target = collision.gameObject.GetComponent<IHealth>();
            DealDamage(target);
        }
        else if(collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
