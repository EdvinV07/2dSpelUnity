using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    [SerializeField] private float knockbackForce = 10f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyHitbox"))
        {
            playerAnimator.SetTrigger("Hit");

            // Calculate knockback direction (only along x-axis)
            float knockbackDirection = Mathf.Sign(transform.position.x - collision.transform.position.x);

            // Apply knockback force (only along x-axis)
            playerRigidbody.velocity = new Vector2(knockbackDirection * knockbackForce, playerRigidbody.velocity.y);
        }
    }
}



