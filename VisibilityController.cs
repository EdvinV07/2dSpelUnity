using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public Transform playerTransform;
    public float visibilityDistance = 5f;

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= visibilityDistance)
        {
            // GameObject is near, make it visible
            GetComponent<MeshRenderer>().enabled = true;
      
        }
        else
        {
            // GameObject is far, make it invisible
            GetComponent<MeshRenderer>().enabled = false;

        }
    }
}
