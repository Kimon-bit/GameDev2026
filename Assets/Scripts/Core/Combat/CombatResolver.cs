using UnityEngine;

public class CombatResolver : MonoBehaviour
{
    public static CombatResolver Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHit(AttackData attack,
        IDamageable target, Vector3 hitDirection)
    {
        target.TakeHit(
            attack.damage, hitDirection,
            attack.knockbackForce, attack.hitstun
        );
    }
}