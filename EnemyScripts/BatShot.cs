using UnityEngine;

public class BatShot : MonoBehaviour
{
    public GameObject Player;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with the player, ground, or wall based on layers
        int playerLayer = LayerMask.NameToLayer("Player");
        int groundLayer = LayerMask.NameToLayer("Ground");
        int wallLayer = LayerMask.NameToLayer("Wall");

        if (other.gameObject.layer == playerLayer || other.gameObject.layer == groundLayer || other.gameObject.layer == wallLayer)
        {
     
            Destroy(gameObject);
        }
        if (other.gameObject.layer == playerLayer)
        {
            //TakeDamage();
        }
    }
}
