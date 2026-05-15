using UnityEngine;

public interface IDamageable
{
    void TakeHit(float damage, Vector3 hitDirection, float knockbackForce, float hitstun);
}
