using UnityEngine;
using UnityEngine.UI;

public class BuyLeap : MonoBehaviour
{
    public Button yourButton;
    private int Leaper = 0;

    private PlayerController playerController; // Add a reference to PlayerController

    private void Start()
    {
        yourButton.onClick.AddListener(ButtonClick);

        GameObject playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();

        if (!PlayerPrefs.HasKey("Leaper"))
        {
            // Set Leaper to 0 only if the key doesn't exist
            Leaper = 0;
        }
    }

    private void ButtonClick()
    {
        if (PlayerPrefs.GetInt("Coins", 0) > -1)
        {
            Leap();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void Leap()
    {
        Debug.Log("Button pressed, and Coins > 1. Do something!");
        Leaper = 1;
        PlayerPrefs.SetInt("Leaper", Leaper);
        PlayerPrefs.Save();

        // Call the LeapPlayer method on the playerController instance
        playerController.LeapPlayer();
    }
}

