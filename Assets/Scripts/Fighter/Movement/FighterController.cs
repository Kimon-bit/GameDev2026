using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FighterController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 6f;

    public Transform opponent;

    Vector2 moveInput;

    Rigidbody rb;

    bool isGrounded;
    bool inHitstun;

    [Header("Ground Check")]
    public Transform groundCheck;

    public float groundCheckRadius = 0.2f;

    public LayerMask groundLayer;

    FighterAnimator fighterAnimator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        fighterAnimator = GetComponent<FighterAnimator>();
    }

    void FixedUpdate()
    {
        if (!inHitstun)
        {
            MoveHorizontal();
        }
    }

    void Update()
    {
        CheckGrounded();

        FaceOpponent();

        UpdateAnimations();
    }

    void MoveHorizontal()
    {
        Vector3 velocity = rb.linearVelocity;

        velocity.x = moveInput.x * speed;

        rb.linearVelocity = velocity;
    }

    void FaceOpponent()
    {
        if (!opponent) return;

        Vector3 dir = opponent.position - transform.position;

        dir.y = 0;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnMoveAI(Vector2 input)
    {
        moveInput = input;
    }

    public void OnJump()
    {
        if (!isGrounded) return;

        if (inHitstun) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGrounded = false;
    }

    void UpdateAnimations()
    {
        bool walking = Mathf.Abs(moveInput.x) > 0.1f;

        fighterAnimator.SetWalking(walking);

        fighterAnimator.SetGrounded(isGrounded);
    }

    public IEnumerator ApplyHitstun(float duration)
    {
        inHitstun = true;

        yield return new WaitForSeconds(duration);

        inHitstun = false;
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }
}