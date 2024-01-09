using System.Collections;
using UnityEngine;

public interface IAttack
{
    public Collider damageCollider {get; set;}
    public float damage {get; set;}
    IEnumerator ActivateDamageCollider(float time);
    void DealDamage(IHealth target);
}