using UnityEngine;

public class CombatResolver : MonoBehaviour
{
    public static CombatResolver Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ApplyDamage(float damage, IDamageable target)
    {
        target.TakeDamage(damage);
    }
}