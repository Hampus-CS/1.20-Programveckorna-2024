using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public List<Transform> rooms = new List<Transform>();
    private List<Transform> availableRooms = new List<Transform>();
    public bool isNearDoor = false;

    void Start()
    {
        availableRooms.AddRange(rooms);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && availableRooms.Count > 0 && isNearDoor)
        {
            TeleportPlayerToRandomRoom();
        }
    }

    void TeleportPlayerToRandomRoom()
    {
        int randomIndex = Random.Range(0, availableRooms.Count);
        Transform roomToTeleport = availableRooms[randomIndex];

        Transform playerTransform = this.transform;
        playerTransform.position = roomToTeleport.position;

        availableRooms.RemoveAt(randomIndex);
    }

}