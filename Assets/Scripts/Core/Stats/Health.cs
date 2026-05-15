using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 100;

    float currentHealth;

    Rigidbody rb;
    FighterController controller;
    FighterAnimator fighterAnimator;

    public bool IsDead { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action<Health> OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();

        controller = GetComponent<FighterController>();

        fighterAnimator = GetComponent<FighterAnimator>();
    }

    public void TakeHit(
        float damage,
        Vector3 hitDirection,
        float knockbackForce,
        float hitstun)
    {
        if (IsDead) return;

        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        fighterAnimator?.PlayHit();

        rb.AddForce(
            hitDirection * knockbackForce,
            ForceMode.Impulse
        );

        if (controller != null)
        {
            StartCoroutine(
                controller.ApplyHitstun(hitstun)
            );
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (IsDead) return;

        IsDead = true;

        Debug.Log($"{gameObject.name} died.");

        OnDeath?.Invoke(this);

        if (controller != null)
            controller.enabled = false;

        FighterCombat combat = GetComponent<FighterCombat>();

        if (combat != null)
            combat.enabled = false;
    }

    public void ResetHealth()
    {
        IsDead = false;

        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (controller != null)
            controller.enabled = true;

        FighterCombat combat = GetComponent<FighterCombat>();

        if (combat != null)
            combat.enabled = true;
    }
}