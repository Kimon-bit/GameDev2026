using UnityEngine;

public class FighterHitbox : MonoBehaviour
{
    public float damage = 10;

    void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null)
            CombatResolver.Instance.ApplyDamage(damage, target);
    }
}