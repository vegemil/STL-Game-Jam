using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum DoorTyps
    {
        Right,
        Left,
        Floor,
    }

    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject floorDoor; 
    [SerializeField] private GameObject center;
     

    public GameObject LeftDoor => leftDoor;
    public GameObject RightDoor => rightDoor;
    public GameObject FloorDoor => floorDoor;
    public Vector3 CenterPosition => center.transform.position;

    public DoorTyps getDoorType(GameObject targetDoor)
    {
        DoorTyps enterDoorType = DoorTyps.Floor; 

        if (targetDoor == leftDoor)
            enterDoorType = DoorTyps.Left;
        else if (targetDoor == rightDoor)
            enterDoorType = DoorTyps.Right;
        else if (targetDoor == floorDoor)
            enterDoorType = DoorTyps.Floor;

        return enterDoorType;
    }

    public Vector3 GetNextRoomPosition(GameObject enterDoor)
    {
        DoorTyps enterDoorType = DoorTyps.Floor;
        Vector3 newSpawnPosition = Vector3.zero;

        if (enterDoor == leftDoor)
            enterDoorType = DoorTyps.Left;
        else if (enterDoor == rightDoor)
            enterDoorType = DoorTyps.Right;
        else if (enterDoor == floorDoor)
            enterDoorType = DoorTyps.Floor;

        switch(enterDoorType)
        {
            case DoorTyps.Floor:
                newSpawnPosition = CenterPosition;
                break;
            case DoorTyps.Left:
                newSpawnPosition = rightDoor.transform.position;
                break;
            case DoorTyps.Right:
                newSpawnPosition = leftDoor.transform.position;
                break;
        }

        return newSpawnPosition;
    }
}
