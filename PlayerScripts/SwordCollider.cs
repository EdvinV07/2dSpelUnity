using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private Collider2D swordCollider;

    void Start()
    {
        // Assuming the collider is attached to the same GameObject as this script
        swordCollider = GetComponent<Collider2D>();

        // Ensure the collider is initially enabled
        swordCollider.enabled = true;
    }

    void Update()
    {
        // Access the PlayerController script on the player GameObject
        PlayerController playerController = transform.parent.GetComponent<PlayerController>();

        // Check if the player is rolling and disable the collider accordingly
        if (playerController != null && playerController.IsRolling())
        {
            swordCollider.enabled = false;
        }
        else
        {
            swordCollider.enabled = true;
        }
    }


}