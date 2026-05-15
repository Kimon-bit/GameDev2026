using UnityEngine;

public class CombatantStats : MonoBehaviour
{
    public float damageMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.Damage:
                damageMultiplier += upgrade.value;
                break;

            case UpgradeType.MoveSpeed:
                moveSpeedMultiplier += upgrade.value;
                break;
        }
    }
}
