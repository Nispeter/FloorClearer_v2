using System.Collections;
using UnityEngine;

public interface IAttack
{
    /*
    REMINDER TO SELF:
    Attack logic does not include activate standarization function because the logic is controlled from its instantiator,
    i.e add force for projectiles (instantiate or pooling), activateDamageColider for melee attacks (child object)
    */ 
    public Collider damageCollider {get; set;}
    public float damage {get; set;}
    IEnumerator ActivateDamageCollider(float delayBeforeActivation, float durationOfActivation);
    void DealDamage(IHealth target);
}