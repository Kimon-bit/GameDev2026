using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Combo")]
public class ComboData : ScriptableObject
{
    public List<AttackInput> sequence;

    public AttackData attack;

    public string animationTrigger;
}