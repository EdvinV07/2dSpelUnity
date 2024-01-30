using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // Array of room prefabs
    public float spacing = 1.0f; // Spacing between rooms

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        ShuffleArray(roomPrefabs); // Shuffle the array of room prefabs

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Vector3 position = new Vector3(col * spacing, row * spacing, 0f);
                Quaternion rotation = Quaternion.identity;

                GameObject room = Instantiate(roomPrefabs[row * 3 + col], position, rotation);
                // You can customize the room properties or behavior here
            }
        }
    }

    // Fisher-Yates shuffle algorithm to shuffle the array of room prefabs
    void ShuffleArray(GameObject[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

