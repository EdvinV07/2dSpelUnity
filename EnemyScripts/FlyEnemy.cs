using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float projectileSpeed = 5f;
    public GameObject projectilePrefab;

    private Transform player;
    private bool isFollowingPlayer = true;
    private float followTimer = 2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }
        else
        {
            ShootProjectile();
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            // Calculate the target position with the player's y as the minimum y position
            Vector2 targetPosition = new Vector2(player.position.x, Mathf.Max(player.position.y, transform.position.y));

            // Move towards the target position with some additional checks for vertical movement
            float newY = Mathf.Max(player.position.y, transform.position.y);
            newY = Mathf.Clamp(newY, player.position.y - 3f, player.position.y + 3f); // Adjust the range based on your needs
            targetPosition = new Vector2(player.position.x, newY);

            // Move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            followTimer -= Time.deltaTime;

            // Check relative position and rotate the bat
            if (player.position.x > transform.position.x)
            {
                // Player is on the right side, rotate the bat 180 degrees
                transform.rotation = Quaternion.identity;
            }
            else
            {
                // Player is on the left side, rotate the bat to its original orientation
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            if (followTimer <= 0f)
            {
                isFollowingPlayer = false;
            }
        }
    }


    void ShootProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;

        // Destroy the projectile after a certain time (adjust the time based on your needs)
        Destroy(projectile, 3f);

        // Reset the timer for the next round of following
        followTimer = 2f;
        isFollowingPlayer = true;
    }
}
