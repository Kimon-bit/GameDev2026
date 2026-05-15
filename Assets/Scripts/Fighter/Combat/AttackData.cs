using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Info")]
    public string attackName;

    [Header("Damage")]
    public float damage = 10;

    [Header("Timing")]
    public float startup = 0.05f;
    public float activeTime = 0.15f;
    public float recovery = 0.3f;

    [Header("Hitbox")]
    public Vector3 hitboxSize = Vector3.one;
    public Vector3 hitboxOffset = Vector3.forward;

    [Header("Combat")]
    public float hitstun = 0.2f;
    public float knockbackForce = 5f;
}