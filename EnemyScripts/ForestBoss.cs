using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    public Transform Player;
    public float aggroRange = 15f;
    public float runningRange = 20f;
    public float moveSpeed = 5f;
    public float attackCooldown = 3f;
    public float timeBetweenAttacks = 1.5f; // Adjust this as needed

    private bool isPlayerNear = false;
    private bool isRunning = false;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private float cooldownTimer = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isPlayerNear = Vector2.Distance(transform.position, Player.position) < aggroRange;

        // Check if the player is within the running range
        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        if (!isRunning && distanceToPlayer > aggroRange && distanceToPlayer <= runningRange && Time.time - cooldownTimer >= attackCooldown)
        {
            // Start running towards the player
            Debug.Log("JesiBesi");
            isRunning = true;
            animator.SetBool("RunBoss", true);
            animator.SetBool("IdleWizard", false); // Stop the "Idle" animation
        }

        if (isRunning && !isAttacking)
        {
            // Determine the rotation based on the player's x-coordinate
            float rotationY = (Player.position.x < transform.position.x) ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

            // Move towards the player along the X-axis
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            // If the player is near and enough time has passed since the last attack, trigger an attack
            if (isPlayerNear && Time.time - attackTimer >= cooldownTimer)
            {
                isAttacking = true;
                attackTimer = Time.time;
                cooldownTimer = Time.time + attackCooldown; // Set the cooldown timer
                isRunning = false; // Cancel the run during the attack
                animator.SetBool("RunBoss", false); // Stop running during the attack
                animator.SetBool("IdleWizard", false); // Stop the "Idle" animation
                TriggerAttack();
            }
        }
        else if (isAttacking)
        {
            // Boss is currently attacking, wait for the attack animation to finish
            // You may need to adjust the condition based on your animation events or length
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                isAttacking = false;
                animator.SetBool("IdleWizard", true); // Play the "Idle" animation once after an attack
            }
        }

        // Check for player proximity after the attack animation finishes
        if (!isRunning && !isAttacking && !isPlayerNear)
        {
            // If the boss is not running, not attacking, and the player is not near, play the "Idle" animation
            animator.SetBool("IdleWizard", true);
        }
        else if (!isRunning && !isAttacking && isPlayerNear)
        {
            // If the boss is not running, not attacking, and the player is near, play the "Idle" animation
            animator.SetBool("IdleWizard", false);
        }
    }

    void TriggerAttack()
    {
        // Attack logic
        Debug.Log("Attack triggered!");
        int attackIndex = Random.Range(1, 3); // Assumes you have 2 attacks
        animator.SetTrigger("Attack" + attackIndex);
    }
}



