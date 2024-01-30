using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public Image weaponImage; // Reference to the UI Image element
    public Sprite swordSprite; // Reference to the sword image
    public Sprite daggerSprite;   // Reference to the gun image

    private bool weapons = false;

    private void Start()
    {
        UpdateWeaponUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            weapons = false;
            UpdateWeaponUI();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            weapons = true;
            UpdateWeaponUI();
        }

        // Your other code for handling attacks
    }

    void UpdateWeaponUI()
    {
        // Update the UI Image based on the currently equipped weapon
        if (weapons)
        {
            weaponImage.sprite = swordSprite;
        }
        else
        {
            weaponImage.sprite = daggerSprite;
        }
    }
}
