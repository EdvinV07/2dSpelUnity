using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; // Array of room prefabs
    public float roomSpacing = 1.0f; // Spacing between rooms

    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {
        List<GameObject> placedRooms = new List<GameObject>();

        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            GameObject currentRoom = Instantiate(roomPrefabs[i], Vector3.zero, Quaternion.identity);
            placedRooms.Add(currentRoom);

            // Try to find a compatible opening in the previously placed rooms
            if (!TryLinkRooms(currentRoom, placedRooms))
            {
                // If no compatible opening is found, destroy the current room
                Destroy(currentRoom);
                placedRooms.Remove(currentRoom);
                i--; // Decrement the index to retry placing the same room
            }
        }
    }

    bool TryLinkRooms(GameObject currentRoom, List<GameObject> placedRooms)
    {
        foreach (GameObject otherRoom in placedRooms)
        {
            if (LinkRooms(currentRoom, otherRoom))
            {
                // Link successful, position the current room next to the other room
                PositionNextTo(currentRoom, otherRoom);
                return true;
            }
        }

        return false; // Couldn't find a compatible opening in any placed room
    }

    bool LinkRooms(GameObject roomA, GameObject roomB)
    {
        // Check if the openings of roomA and roomB can link together
        RoomController roomControllerA = roomA.GetComponent<RoomController>();
        RoomController roomControllerB = roomB.GetComponent<RoomController>();

        if (roomControllerA == null || roomControllerB == null)
        {
            Debug.LogError("RoomController component not found on rooms.");
            return false;
        }

        // Example: Check if there is a "Right opening" in roomA and a "Left opening" in roomB
        return roomControllerA.HasOpening("OpsRight") && roomControllerB.HasOpening("OpsLeft");
    }

    void PositionNextTo(GameObject currentRoom, GameObject otherRoom)
    {
        // Position currentRoom next to otherRoom based on their openings
        // You'll need to implement logic to calculate the correct position
        // and rotation based on the openings of the two rooms.
        // This might involve adjusting the position and rotation of currentRoom
        // relative to otherRoom.
        // ...

        // For simplicity, we'll just place the current room to the right of the other room.
        Vector3 position = otherRoom.transform.position + new Vector3(roomSpacing, 0f, 0f);
        currentRoom.transform.position = position;
    }
}
