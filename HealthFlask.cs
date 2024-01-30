using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    public float healAmount = 3f;

    private void Update()
    {
        // Check for the F key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Assuming you have a PlayerHealth script on your player object
            HealthBarScript playerLife = FindObjectOfType<HealthBarScript>();
            Debug.Log("Mhm");

            // Check if the player health script and player's health are not null
            if (playerLife != null && playerLife.currentHealth < playerLife.maxHealth)
            {
                // Heal the player
                playerLife.Heal(healAmount);
                Debug.Log("Okau");
                // Optionally, play a healing sound or add visual effects
                // AudioManager.PlayHealingSound();
                // Instantiate(healingEffectPrefab, transform.position, Quaternion.identity);

                // Disable the health flask object or perform other logic
                gameObject.SetActive(false);
            }
        }
    }
}