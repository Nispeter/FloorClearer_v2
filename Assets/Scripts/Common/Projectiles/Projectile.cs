using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IAttack
{
    [Header("Projectile params")]
    public float moveSpeed;
    public float lifeTime;
    [SerializeField] private Collider _damageCollider;
    [SerializeField] private float _damage;

    public Collider damageCollider
    {
        get { return _damageCollider; }
        set { _damageCollider = value; }
    }

    public float damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public void DealDamage(IHealth target)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    public virtual void OnTriggerEnter(Collider collision)
    {
        IHealth target = collision.gameObject.GetComponent<IHealth>();
        DealDamage(target);
    }

    public IEnumerator ActivateDamageCollider(float time)
    {
        return null;
    }
}
