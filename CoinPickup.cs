using UnityEngine;

public class CoinPickup : MonoBehaviour

{
    private bool Taken = false;
    private CoinSpawner coinSpawner;

    // Initialize the CoinPickup with a reference to the CoinSpawner
    public void Initialize(CoinSpawner spawner)
    {
        coinSpawner = spawner;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the coin is the player
        if (collision.collider.CompareTag("Player"))
        {
            
            // Call a method on the player to handle the coin pickup
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if (player != null && Taken == false)
            {
                    player.PickupCoin();
                    Destroy(gameObject); // Destroy the coin object
                    Taken = true;
                
            
            }
        }
    }
}
