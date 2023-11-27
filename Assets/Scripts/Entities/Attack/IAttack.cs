using System.Collections;
using UnityEngine;

public interface IAttack
{
    IEnumerator ActivateDamageCollider(float time);
    void DealDamage(IHealth target);
}