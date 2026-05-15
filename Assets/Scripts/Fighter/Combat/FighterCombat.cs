using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    [Header("Hitbox")]
    public FighterHitbox hitbox;

    [Header("Attacks")]
    public AttackData lightAttack;
    public AttackData heavyAttack;
    public AttackData airAttack;

    [Header("Combos")]
    public List<ComboData> combos;

    [Header("Combo Timing")]
    public float comboResetTime = 1f;

    bool canAttack = true;

    FighterAnimator fighterAnimator;

    List<AttackInput> currentCombo = new();

    float lastInputTime;

    void Awake()
    {
        fighterAnimator = GetComponent<FighterAnimator>();

        if (hitbox == null)
        {
            hitbox = GetComponentInChildren<FighterHitbox>(true);
        }
    }

    void Update()
    {
        if (currentCombo.Count > 0 &&
            Time.time - lastInputTime > comboResetTime)
        {
            //Debug.Log("COMBO RESET (timeout)");
            currentCombo.Clear();
        }
    }

    public void OnLightAttack() => TryAttack(AttackInput.Light);
    public void OnHeavyAttack() => TryAttack(AttackInput.Heavy);
    public void OnAirAttack() => TryAttack(AttackInput.Air);

    void TryAttack(AttackInput input)
    {
        if (!canAttack) return;

        currentCombo.Add(input);
        lastInputTime = Time.time;

        //Debug.Log("COMBO BUFFER: " + string.Join(",", currentCombo));

        if (CheckCombos())
        {
            currentCombo.Clear();
            return;
        }

        AttackData attack = GetBasicAttack(input);

        PlayBasicAnimation(input);
        StartCoroutine(PerformAttack(attack));
    }

    bool CheckCombos()
    {
        foreach (ComboData combo in combos)
        {
            if (combo.sequence == null || combo.sequence.Count == 0)
                continue;

            if (ComboMatches(combo.sequence))
            {
                //Debug.Log("COMBO MATCHED: " + combo.animationTrigger);
                //Debug.Log("ATTACK DATA: " + combo.attack);

                currentCombo.Clear();

                fighterAnimator.PlayTrigger(combo.animationTrigger);

                StartCoroutine(PerformAttack(combo.attack));

                return true;
            }
        }
        return false;
    }

    bool ComboMatches(List<AttackInput> sequence)
    {
        if (currentCombo.Count < sequence.Count)
            return false;

        int start = currentCombo.Count - sequence.Count;

        for (int i = 0; i < sequence.Count; i++)
        {
            if (currentCombo[start + i] != sequence[i])
                return false;
        }

        return true;
    }

    AttackData GetBasicAttack(AttackInput input)
    {
        return input switch
        {
            AttackInput.Light => lightAttack,
            AttackInput.Heavy => heavyAttack,
            AttackInput.Air => airAttack,
            _ => lightAttack
        };
    }

    void PlayBasicAnimation(AttackInput input)
    {
        switch (input)
        {
            case AttackInput.Light:
                fighterAnimator.PlayLightAttack();
                break;

            case AttackInput.Heavy:
                fighterAnimator.PlayHeavyAttack();
                break;

            case AttackInput.Air:
                fighterAnimator.PlayLightAttack();
                break;
        }
    }

    IEnumerator PerformAttack(AttackData attack)
    {
        canAttack = false;

        yield return new WaitForSeconds(attack.startup);

        hitbox.currentAttack = attack;

        BoxCollider col = hitbox.GetComponent<BoxCollider>();
        col.size = attack.hitboxSize;
        hitbox.transform.localPosition = attack.hitboxOffset;

        hitbox.gameObject.SetActive(true);

        yield return new WaitForSeconds(attack.activeTime);

        hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(attack.recovery);

        canAttack = true;
    }
}