using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerRoom : MonoBehaviour
{
    public GameObject[] roomTemplates;
    public int gridSizeX = 4;
    public int gridSizeY = 4;
    public float roomSize = 1f; // Adjust as needed

    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * roomSize, 0, y * roomSize);
                GameObject room = Instantiate(GetRandomRoomTemplate(), spawnPosition, Quaternion.identity);

                // Connect the room to adjacent rooms
                ConnectRooms(room, x, y);
            }
        }
    }

    void ConnectRooms(GameObject room, int x, int y)
    {
        // Check openings based on the position of the room
        if (x > 0)
        {
            ConnectToAdjacentRoom(room, x - 1, y, "RightOpening", Vector3.left);
        }

        if (x < gridSizeX - 1)
        {
            ConnectToAdjacentRoom(room, x + 1, y, "LeftOpening", Vector3.right);
        }

        if (y > 0)
        {
            ConnectToAdjacentRoom(room, x, y - 1, "TopOpening", Vector3.back);
        }

        if (y < gridSizeY - 1)
        {
            ConnectToAdjacentRoom(room, x, y + 1, "BottomOpening", Vector3.forward);
        }
    }

    void ConnectToAdjacentRoom(GameObject room, int x, int y, string openingTag, Vector3 offset)
    {
        GameObject adjacentRoom = FindRoomWithOpening(x, y, openingTag);
        if (adjacentRoom != null)
        {
            // Implement logic to align and connect the rooms
            // Adjust the position and rotation based on your room designs
            room.transform.position = adjacentRoom.transform.position + offset * roomSize;
        }
    }

    GameObject FindRoomWithOpening(int x, int y, string openingTag)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            Vector3 checkPosition = new Vector3(x * roomSize, 0, y * roomSize);
            Collider[] colliders = Physics.OverlapBox(checkPosition, Vector3.one * roomSize / 2f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(openingTag))
                {
                    return collider.gameObject;
                }
            }
        }
        return null;
    }

    GameObject GetRandomRoomTemplate()
    {
        return roomTemplates[Random.Range(0, roomTemplates.Length)];
    }
}

