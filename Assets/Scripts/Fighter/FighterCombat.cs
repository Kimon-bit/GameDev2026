using System;
using System.Collections;
using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    public GameObject hitbox;

    public float activeTime = 0.15f;
    public float cooldown = 0.4f;

    bool canAttack = true;

    public void OnAttack()
    {
        if (!canAttack) return;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        canAttack = false;

        hitbox.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        hitbox.SetActive(false);

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }
}