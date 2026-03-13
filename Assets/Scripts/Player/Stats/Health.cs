using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 100;

    float currentHealth;

    public event Action<float, float> OnHealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}