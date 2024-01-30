using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BuyBomb : MonoBehaviour
{
   
    public Button yourButton;
    public bool Bomb = false;

    private void Start()
    {
   
        yourButton.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
       
        if (PlayerPrefs.GetInt("Coins", 0) > -1)
        {
         
            BuyBoom();
        }
        else
        {
            
            Debug.Log("Not enough coins!");
        }
    }

    public void BuyBoom()
    {
       
        Debug.Log("Button pressed, and Coins > 1. Do something!");
        Bomb = true;

    }
}
