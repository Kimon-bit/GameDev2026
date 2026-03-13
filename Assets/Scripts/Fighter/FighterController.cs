using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FighterController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 6f;

    public Transform opponent;

    private Vector2 moveInput;
    private Rigidbody rb;

    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        MoveHorizontal();
    }

    void Update()
    {
        FaceOpponent();
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
            transform.rotation = Quaternion.LookRotation(dir);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!isGrounded) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length == 0) return;

        // Check if touching ground
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}