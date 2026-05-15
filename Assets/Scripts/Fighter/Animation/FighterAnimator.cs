using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FighterAnimator : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalking(bool walking)
            => animator.SetBool("IsWalking", walking);

    public void SetGrounded(bool grounded)
        => animator.SetBool("IsGrounded", grounded);

    public void PlayLightAttack()
        => animator.SetTrigger("LightAttack");

    public void PlayHeavyAttack()
        => animator.SetTrigger("HeavyAttack");

    public void PlayHit()
        => animator.SetTrigger("Hit");

    public void PlayTrigger(string trigger)
        => animator.SetTrigger(trigger);
}