using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinsTextElement;

    private void Start()
    {
        coinsTextElement = GetComponent<TextMeshProUGUI>(); // Assuming the script is attached to a TextMeshProUGUI element

        UpdateCoinCounterUI();

        if (coinsTextElement == null)
        {
            Debug.LogError("coinsTextElement is null");
        }

      
    }

    public void UpdateCoinCounterUI()
    {
  
        if (coinsTextElement != null)
        {
            int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
            coinsTextElement.text = "Coins: " + coinCount.ToString();
            Debug.Log("Coins Collected: " + coinCount);
            Debug.Log("UpdateCoinCounterUI called");

        }
        else
        {
            Debug.Log("UpdateCoinCounterUI nono called");
        }
           
           

        
    }


}
