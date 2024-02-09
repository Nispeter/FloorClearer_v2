using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHealth
{
    [Header("Enemy params")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _remainingHealth;
    public List<StatusEffect> StatusEffects { get; set; } = new List<StatusEffect>();
    public int pointsOnKill;
    public int cost = 10;

    public float health
    {
        get => _health;
        set => _health = value;
    }

    public float remainingHealth
    {
        get => _remainingHealth;
        set => _remainingHealth = value;
    }

    public virtual void TakeDamage(float damage)
    {
        _remainingHealth -= damage;

        if (_remainingHealth <= 0)
        {
            Die();
            InGameManager.Instance.pointCounter.AddPoints(pointsOnKill);
        }
    }

    public virtual void Die(){
        Destroy(this.gameObject, 1.5f);
    }
}
