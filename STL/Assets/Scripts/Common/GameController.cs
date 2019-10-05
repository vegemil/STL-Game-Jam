using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MakeRooms roomMaker;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraController cameraController;

    private void OnEnable()
    {
        Initalize();
    }

    public void Initalize()
    {
        characterController.Initalize();
        characterController.Movement.OnEndMove = OnEndMove;
    }

    public void OnEndMove()
    { 
        Vector3 newPlayerPosition = roomMaker.CurrentRoom.GetNextRoomPosition(cameraController.LastClickObject);
        roomMaker.moveNextFloor();
        characterController.Movement.MoveToPoint(newPlayerPosition, true);
    }
}
