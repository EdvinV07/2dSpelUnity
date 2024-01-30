using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public string[] openings; // Tags representing the openings of the room

    public bool HasOpening(string openingTag)
    {
        // Check if the room has the specified opening
        return System.Array.Exists(openings, tag => tag == openingTag);
    }
}


