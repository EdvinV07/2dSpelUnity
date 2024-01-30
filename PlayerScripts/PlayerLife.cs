using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int Die = 0;
    public string Spawn;

    private HealthBarScript healthBar;
    private PlayerController playerController; // Corrected variable name

    void Start()
    {
        healthBar = GetComponentInChildren<HealthBarScript>();
        playerController = GetComponent<PlayerController>(); // Corrected variable assignment
    }

    void Update()
    {
        if (healthBar != null)
        {
            if (healthBar.currentHealth <= Die)
            {
                SceneManager.LoadScene(Spawn);
                healthBar.currentHealth = 10;

                // Corrected the variable name to use the correct reference
                if (playerController != null)
                {
                    playerController.SpawnPos();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyHitbox"))
        {
            Debug.Log("Hit");

            // Update the health bar when the player takes damage
            if (healthBar != null)
            {
                healthBar.TakeDamage(healthBar.DamageTaken);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BatShot") || other.CompareTag("RandomProj"))
        {
            Debug.Log("Hit");

            // Update the health bar when the player takes damage
            if (healthBar != null)
            {
                healthBar.TakeDamage(healthBar.DamageTaken);
            }
        }
    }
}






