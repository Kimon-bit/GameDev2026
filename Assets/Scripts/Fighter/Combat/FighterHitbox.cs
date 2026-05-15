using System.Collections.Generic;
using UnityEngine;

public class FighterHitbox : MonoBehaviour
{
    [HideInInspector]
    public AttackData currentAttack;

    HashSet<IDamageable> hitTargets = new();

    void OnEnable()
    {
        hitTargets.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();

        if (target == null) return;

        if (hitTargets.Contains(target)) return;

        hitTargets.Add(target);

        Vector3 hitDirection =
            (other.transform.position - transform.root.position).normalized;

        CombatResolver.Instance.ApplyHit(
            currentAttack,
            target,
            hitDirection
        );
    }
}