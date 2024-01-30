using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float attackRange = 2.5f;
    public float moveRange = 5f;
    public float attackCooldown = 4.0f;

    private Transform player;
    private Animator animator;
    private float lastAttackTime;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isAttacking)
        {
            // If attacking, wait until the attack animation is complete before moving again
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"))
            {
                isAttacking = false;
                if (distanceToPlayer > attackRange)
                {
                    animator.SetBool("IsWalking", true);
                }
                else
                {
                    animator.SetBool("Idle", true);
                }
            }
            else
            {
                return;
            }
        }
        else if (distanceToPlayer <= attackRange && !isAttacking)
        {
            // Within attackRange, initiate attack
            animator.SetBool("IsWalking", false);
            animator.SetBool("Idle", false);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("AttackEnemy");
                lastAttackTime = Time.time;
                isAttacking = true;
                animator.SetBool("Idle", true);
            }
        }
        else if (distanceToPlayer > attackRange && !isAttacking)
        {
            // Player is outside attackRange, start walking animation
            animator.SetBool("IsWalking", true);
            animator.SetBool("Idle", false);
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer <= moveRange)
        {
            // Move towards the player if within the moveRange
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            animator.SetBool("IsWalking", true);
            animator.SetBool("Idle", false); // Ensure the "Idle" state is not set
        }
        else
        {
            // Player is outside both moveRange and attackRange, play "Idle" animation
            animator.SetBool("IsWalking", false);
            animator.SetBool("Idle", true);
        }

        // Direct enemy to face the player
        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.x < 0 && !isAttacking)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }





    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SwordCollider"))
        {
            // Check if currently attacking
            if (isAttacking)
            {
                // Interrupt the attack and play TakeDamage animation
                animator.CrossFade("TakeDamage", 0.1f); // Adjust the crossfade duration as needed
                isAttacking = false;
            }
            else
            {
                // Play TakeDamage animation directly if not attacking
                animator.SetTrigger("TakeDamage");
            }
        }
    }

}


