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
        if (cameraController.IsMovingCamera)
            return;
        if (!cameraController.LastClickObject)
            return;
        Room.DoorTyps doorType = roomMaker.CurrentRoom.getDoorType(cameraController.LastClickObject);
        CameraController.MoveDirectionType cameraMoveDirection = CameraController.MoveDirectionType.Bottom;

        cameraController.CleaeClickObject();
        switch (doorType)
        {
            case Room.DoorTyps.Floor:
                cameraMoveDirection = CameraController.MoveDirectionType.Bottom;
                break;
            case Room.DoorTyps.Right:
                cameraMoveDirection = CameraController.MoveDirectionType.Right;
                break;
            case Room.DoorTyps.Left:
                cameraMoveDirection = CameraController.MoveDirectionType.Left;
                break; 
        }


        cameraController.PlayMove(cameraMoveDirection, () =>
        {
            Vector3 newPlayerPosition = roomMaker.CurrentRoom.GetNextRoomPosition(cameraController.LastClickObject);

            if (doorType == Room.DoorTyps.Floor)
                roomMaker.moveNextFloor();
            else
                roomMaker.moveSameFloor();

            characterController.Movement.MoveToPoint(newPlayerPosition, true);
        }, ()=>
        {
            //Vector3 newPlayerPosition = roomMaker.CurrentRoom.CenterPosition;

            //characterController.Movement.MoveToPoint(newPlayerPosition);
        });
        
    }
}
