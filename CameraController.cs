using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float followSpeed = 5.0f;  // Speed at which the camera follows the player
    public float damping = 0.5f;  // Damping factor for smooth camera movement
    private float offset = 3.0f;
    private float minY = 0.0f;
    Vector3 targetPosition;
    // Minimum y-coordinate for the camera

    private static bool playerSpawned = false;

    private void Start()
    {
        // Assuming your UI Text element is a child of Canva
        Scene currentScene = SceneManager.GetActiveScene();




    }
    void LateUpdate()
    {
        if (target == null)
        {
            return;  // Ensure that the target is set before attempting to follow
        }

        // Get the player's position in screen coordinates
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);

        // Calculate the target y-coordinate based on the player's position

        float targetY = screenPos.y > Screen.height / 1.6 ? target.position.y + offset : Mathf.Max(target.position.y - offset, minY);



        // Lock y-axis position of the camera
        if (transform.position.x < 30)
        {
            targetPosition = new Vector3(target.position.x, targetY, transform.position.z);
        }
        else if (transform.position.x > 30)
        {
            targetPosition = new Vector3(target.position.x, target.position.y + offset, transform.position.z);
        }



        // Smoothly move the camera towards the target position
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Apply damping effect
        transform.position = Vector3.Lerp(transform.position, newPosition, damping);
    }
}