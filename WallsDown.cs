using UnityEngine;

public class WallsDown : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public float xOffset = 1f;  // Offset value for comparison
    public float deltaY = -2.5f;  // Amount to lower the object's y position

    private bool hasMoved = false;  // Flag to track whether the object has already moved

    void Update()
    {
        // Check if the player's x position is greater than the object's x position + offset and the object hasn't moved yet
        if (!hasMoved && player.transform.position.x > transform.position.x + xOffset)
        {
            // Move the object by changing its y position
            MoveObjectByDeltaY();
        }
    }

    void MoveObjectByDeltaY()
    {
        // Lower the object's y position by the specified amount
        transform.position = new Vector3(transform.position.x, transform.position.y + deltaY, transform.position.z);
        
        // Set the flag to true, indicating that the object has moved
        hasMoved = true;
    }
}


