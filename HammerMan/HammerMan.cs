using UnityEngine;
using TMPro;
using System.Collections;

public class HammerMan : MonoBehaviour
{
    public Transform player;
    public float jumpHeight = 5f;
    public float jumpDuration = 1f;
    public float waitDuration = 0.5f;
    public float attackCooldown = 2f;
    public float attackRange = 2f;
    public float gravityScale = 1f;
    public float attackSpeed = 3f;
    public float moveRange = 10f;
    public TextMeshProUGUI popupText; // Reference to the TextMeshProUGUI object
    private bool isJumping = false;
    private Vector3 initialPlayerPosition;

    private Animator animator;
    private Rigidbody2D rb;
    private bool hasShownPopup = false; // Flag to track if the popup has been shown

    private HealthBarScript healthBar;


    private void Start()
    {
        InvokeRepeating("RandomAttack", 0f, attackCooldown);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("IsWalking", true);
        popupText.gameObject.SetActive(false);

        healthBar = GetComponentInChildren<HealthBarScript>();

    }

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the move range for the first time
        if (playerDistance <= moveRange && !hasShownPopup)
        {
          
            hasShownPopup = true;
            ShowPopupText("Colossus Crusher");
   
        }
    }

    private void ShowPopupText(string text)
    {
        // Show the TextMeshProUGUI with the specified message for 3 seconds
        StartCoroutine(ShowTextCoroutine(text));
    }

    private IEnumerator ShowTextCoroutine(string text)
    {
        popupText.text = text;
        popupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        popupText.gameObject.SetActive(false);
    }

    private void RandomAttack()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the move range before deciding to attack
        if (playerDistance <= moveRange)
        {
            int randomAttack = Random.Range(1, 4);

            switch (randomAttack)
            {
                case 1:
                    Debug.Log("Jump Attack");
                    StartCoroutine(JumpAttack());
                    break;

                case 2:
                    // Check player distance for Case 2 (PokeAttack)
                    if (playerDistance <= attackRange)
                    {
                        Debug.Log("Poke");
                        StartCoroutine(PokeAttack());
                    }
                    else
                    {
                        Debug.Log("Player out of range for PokeAttack, choosing a different action or doing nothing.");
                    }
                    break;

                case 3:
                    Debug.Log("SnaourAttack");
                    StartCoroutine(SnaourAttack());
                    break;
            }
        }
    }

    // ... (rest of the code remains unchanged)




private IEnumerator JumpAttack()
    {
        if (!isJumping)
        {
            isJumping = true;
            animator.SetTrigger("HammerDunk");
            animator.SetBool("IsWalking", false);
            rb.gravityScale = 0f;

            // Rotate towards the player
            Vector3 directionToPlayer = player.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Jump up
            float elapsedTime = 0f;
            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y + jumpHeight, initialPosition.z);

            while (elapsedTime < jumpDuration)
            {
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / jumpDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Disable gravity while waiting at the top
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = false;
            }

            // Store the initial player position
            initialPlayerPosition = player.position;

            // Wait at the top
            yield return new WaitForSeconds(waitDuration);

            // Enable gravity after waiting
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.velocity = Vector3.zero; // Reset velocity to prevent unwanted movement
            }

            // Rotate back to normal
            transform.rotation = Quaternion.identity;

            // Fly towards the initial player position
            elapsedTime = 0f;
            while (elapsedTime < jumpDuration)
            {
                transform.position = Vector3.Lerp(targetPosition, initialPlayerPosition, elapsedTime / jumpDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isJumping = false;
            animator.SetBool("IsWalking", true);
            rb.gravityScale = 1f;
            yield return new WaitForSeconds(1f);
        }
    }


    private IEnumerator PokeAttack()
    {
        // Check relative positions
        float playerX = player.position.x;
        float bossX = transform.position.x;
        animator.SetBool("IsWalking", false);


        if (playerX < bossX)
        {
            // Player is to the left of the boss
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            // Player is to the right of the boss or at the same position
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        // Play HammerDash animation
        animator.SetTrigger("AttackEnemy");

        // Wait for a specific duration (adjust as needed)
        yield return new WaitForSeconds(attackAnimationDuration);

        animator.SetBool("IsWalking", true);
        yield return new WaitForSeconds(1f);

        // Set back to walking animation

    }

    private float attackAnimationDuration = 1.0f; // Adjust the duration based on your animation


    private IEnumerator SnaourAttack()
    {
        // Play SnaourAttack animation
        animator.SetTrigger("SnaourAttack");

        yield return new WaitForSeconds(0.2f);
        // Get the player's X position
        float targetX = player.position.x;

        // Calculate the direction towards the player
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Lock the Y position
        float initialY = transform.position.y;

        // Move towards the player's X position while keeping Y locked
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y = initialY; // Lock the Y position
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, attackSpeed * Time.deltaTime);
            yield return null;
        }

        // Set back to walking animation
        animator.SetBool("isWalking", true);
        yield return new WaitForSeconds(1f);
    }


}