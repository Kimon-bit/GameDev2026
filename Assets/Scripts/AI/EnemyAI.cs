using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public FighterController controller;
    public FighterCombat combat;

    public Transform target;

    public float attackRange = 2f;

    float attackCooldown;

    void Update()
    {
        if (!target) return;

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist > attackRange)
        {
            MoveTowardsTarget();
        }
        else
        {
            Attack();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;

        controller.OnMoveAI(new Vector2(dir.x, 0));
    }

    void Attack()
    {
        if (Time.time < attackCooldown)
            return;

        attackCooldown = Time.time + Random.Range(0.8f, 1.5f);

        int choice = Random.Range(0, 2);

        if (choice == 0)
            combat.OnLightAttack();
        else
            combat.OnHeavyAttack();
    }
}