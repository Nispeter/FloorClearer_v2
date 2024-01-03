using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHealth, IEntity
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _remainingHealth;
    [SerializeField] private int _pointsOnKill;
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

    public void ModifyMovementSpeed(float modifier)
    {
        Debug.Log("Slowed");
    }

    public virtual void TakeDamage(float damage)
    {
        _remainingHealth -= damage;

        if (_remainingHealth <= 0)
        {
            Die();
            InGameManager.Instance.pointCounter.AddPoints(_pointsOnKill);
        }
    }

    public abstract void Die();
}
