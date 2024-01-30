using UnityEngine;
using UnityEngine.UIElements;

public class BossLife : MonoBehaviour
{
    public int maxLives = 10;
    public int currentLives;
    private int bossDamageTaken = 1;
    public CoinSpawner Coin;

    private Rigidbody2D bossRigidbody;
    private BoxCollider2D boxColliderBoss;
    private Animator animator;

    public Collider2D playerCollider;

    private bool isDead = false;

    private void Start()
    {
        currentLives = maxLives;
        bossRigidbody = GetComponent<Rigidbody2D>();
        boxColliderBoss = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        Physics2D.IgnoreCollision(boxColliderBoss, playerCollider, true);


 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.collider.CompareTag("SwordCollider") && currentLives > 0)
        {
            TakeDamageBoss();
            Debug.Log("Boss Hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && other.CompareTag("Bomb") && currentLives > 0)
        {
            TakeDamageBoss();
            Debug.Log("Boss Hit");
        }
    }

    private void TakeDamageBoss()
    {
        currentLives -= bossDamageTaken;



        if (currentLives <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        isDead = true;

        // Stop all movement
        bossRigidbody.velocity = Vector2.zero;
        bossRigidbody.angularVelocity = 0f;

        // Trigger the death animation
        animator.SetTrigger("DieBoss");
    }

    // This method will be called as an animation event
    public void DisableColliderAndDestroy()
    {
        // Disable the BoxCollider2D when the boss dies
        boxColliderBoss.enabled = false;

        // Spawn coins and destroy the boss GameObject
        if (Coin != null)
        {
            Coin.OnEnemyDestroyed(transform.position);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Coin reference is null in BossLife script.");
        }
    }
}
