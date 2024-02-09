using System.Collections.Generic;
public interface IHealth {
    List<StatusEffect> StatusEffects { get; set; }
    float health {get; set;}
    float remainingHealth {get; set;}
    void TakeDamage(float damage);
    void Die();
}