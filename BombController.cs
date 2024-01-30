using UnityEngine;

public class BombController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float backwardSpeed = -5f;
    public float upwardForce = 10f;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Animator animator;
    private bool isGrounded = false;

    private PlayerController playerController;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Apply force upwards when spawning
        rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Find the PlayerController script in the scene
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            // Move the bomb forward or backward based on the player's facing direction
            float horizontalSpeed = playerController.transform.localScale.x > 0 ? forwardSpeed : backwardSpeed;
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);

            // Ensure consistent upward force
            rb.velocity = new Vector2(rb.velocity.x, upwardForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Ceiling"))
        {
            if (!isGrounded)
            {
                // Lock the position and disable gravity when touching the ground, wall, or ceiling
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0f;
                isGrounded = true;

                animator.SetTrigger("ExplosionBomb");

                // Destroy the game object after the animation is finished
                Destroy(gameObject, 0.3f);
            }
        }
    }
}



