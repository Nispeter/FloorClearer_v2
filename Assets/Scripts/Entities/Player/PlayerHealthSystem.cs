using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IHealth
{
    public float health { get; set; }
    public float remainingHealth { get; set; }

    private HealthBar HealthBar;
    private PlayerCamera PlayerCameraManager;

    public void Start()
    {
        PlayerCameraManager = GetComponent<FirstPersonMovement>().cameraTransform.GetComponent<PlayerCamera>(); 
        HealthBar = InGameManager.Instance.healthBar;
        health = 20;
        remainingHealth = health;
        HealthBar.UpdateHealthBar(remainingHealth, health);
    }

    public void TakeDamage(float damage)
    {
        remainingHealth -= damage;
        PlayerCameraManager.CameraShake(0.5f, 0.1f);
        HealthBar.UpdateHealthBar(remainingHealth, health);
        if (remainingHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        InGameManager.Instance.GameOver();
    }
}
