using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultLayer : MonoBehaviour
{
    public float[] layerSpeeds;
    public float playerMovementSpeed = 5f; // Adjust this value based on your player's movement speed

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform layer = transform.GetChild(i);
            float speed = layerSpeeds[i] * playerMovementSpeed;
            layer.Translate(Vector3.left * speed * horizontalInput * Time.deltaTime);

            // Reset the position when the layer goes off-screen
            if (layer.position.x < -layer.GetComponent<SpriteRenderer>().bounds.size.x)
            {
                float offset = Mathf.Abs(layer.position.x) - layer.GetComponent<SpriteRenderer>().bounds.size.x;
                layer.position = new Vector3(layer.position.x + offset * 2, layer.position.y, layer.position.z);
            }
        }
    }
}
