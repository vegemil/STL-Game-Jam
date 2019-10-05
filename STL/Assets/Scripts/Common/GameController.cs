using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MakeRooms roomMaker;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CameraController cameraController;
    private int currentLayer;

    private void OnEnable()
    { 
    }

    public void Initalize()
    {
        roomMaker.onChangedRoom = OnChangedRoom;
        roomMaker.onEndGame = OnEndGame;
        characterController.Initalize();
        characterController.Movement.OnEndMove = OnEndMove;
        UpdateLayerBGM();
        currentLayer = 0;
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
        int SFIndex = (int)AudioManager.EffectType.CameraMoveLeft + (int)doorType;
        AudioManager.Instance.PlayEffectAudio((AudioManager.EffectType)SFIndex);
        SFIndex = (int)AudioManager.EffectType.LeftDoor + (int)doorType;
        AudioManager.Instance.PlayEffectAudio((AudioManager.EffectType)SFIndex);

        cameraController.PlayMove(cameraMoveDirection, () =>
        {
            Vector3 newPlayerPosition = roomMaker.CurrentRoom.GetNextRoomPosition(cameraController.LastClickObject);

            roomMaker.moveRoom(doorType);

            characterController.Movement.MoveToPoint(newPlayerPosition, true);
        }, ()=>
        {
            if(doorType == Room.DoorTyps.Floor)
                AudioManager.Instance.PlayEffectAudio(AudioManager.EffectType.Landing);
        });
        
    }

    void UpdateLayerBGM()
    {
        if (currentLayer == roomMaker.RoomLayer)
            return;
        currentLayer = roomMaker.RoomLayer;
        if (roomMaker.RoomLayer < 2)
        {
            AudioManager.Instance.StopBGM();
        }
        else if (roomMaker.RoomLayer < 4)
        {
            AudioManager.Instance.SetLoopBGM(false);
            AudioManager.Instance.PlayBGM(AudioManager.BGMType.State3_4);
        }
        else if (roomMaker.RoomLayer < 6)
        {
            AudioManager.Instance.SetLoopBGM(true);
            AudioManager.Instance.PlayBGM(AudioManager.BGMType.State5_7);
        }
        else
        {
            AudioManager.Instance.PlayBGM(AudioManager.BGMType.LastState);
        }
    }

    void OnChangedRoom()
    {
        CharacterSpriteData.AnimationTyps newAnimation = (CharacterSpriteData.AnimationTyps)roomMaker.RoomLayer;
        characterController.Character.SpriteAnimator.SetAnimationData(newAnimation);

        UpdateLayerBGM();

    }

    void OnEndGame()
    {
        Initalize();
    }
}
