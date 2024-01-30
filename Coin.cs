using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float yOffset = 0.7f;
    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;

            // Increment the coin count
            int currentCoinCount = PlayerPrefs.GetInt("CoinCount", 0);
            currentCoinCount++;
            Debug.Log("Coins" + currentCoinCount);
            // Save the updated coin count back to PlayerPrefs
            PlayerPrefs.SetInt("CoinCount", currentCoinCount);

            // Notify the UI to update the coin counter
            FindObjectOfType<CoinUI>()?.UpdateCoinCounterUI(); // Ensure CoinUI is not null before calling the method
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }


    public void OnEnemyDestroyed(Vector3 enemyPosition)
    {
        // Spawn a coin at the enemy's position with an offset on the Y axis
        Vector3 spawnPosition = new Vector3(enemyPosition.x, enemyPosition.y + yOffset, enemyPosition.z);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }

    // Called when something enters the trigger collider
}





