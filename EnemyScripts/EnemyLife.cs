using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    private int eDamageTaken = 1;
    public CoinSpawner Coin;

    private Rigidbody2D enemyRigidbody;
    private BoxCollider2D boxColliderEnemy;
    private Animator animator;

    // Reference to the player's collider
    public Collider2D playerCollider;

    private void Start()
    {
        currentLives = maxLives;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        boxColliderEnemy = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        // Ignore collisions between the enemy and player
        Physics2D.IgnoreCollision(boxColliderEnemy, playerCollider, true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SwordCollider") && currentLives > 0)
        {
            TakeDamageEnemy();
            Debug.Log("EnemyHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb") && currentLives>0)
        {
            TakeDamageEnemy();
            Debug.Log("EnemyHit");
        }
    }


    private void TakeDamageEnemy()
    {
        currentLives -= eDamageTaken;

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Disable the BoxCollider2D when the enemy dies
        boxColliderEnemy.enabled = false;

        animator.SetTrigger("DieEnemy");
    }

    private void DestroyAfterAnimation()
    {
        Debug.Log("DestroyAfterAnimation called");

        if (Coin != null)
        {
            Coin.OnEnemyDestroyed(transform.position);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Coin reference is null in EnemyLife script.");
        }
    }
}