using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IAttack
{
    public float moveSpeed;
    public float lifeTime;
    public float projectileDamage;
    
    public void DealDamage(IHealth target)
    {
        if (target != null)
        {
            target.TakeDamage(projectileDamage);
        }
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        IHealth target = collision.gameObject.GetComponent<IHealth>();
        DealDamage(target);
    }

    public IEnumerator ActivateDamageCollider(float time) {
        return null;
     }
}
