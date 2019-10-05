using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MakeRooms roomMaker;
    [SerializeField] private CharacterController characterController;

    private void OnEnable()
    {
        Initalize();
    }

    public void Initalize()
    {
        characterController.Initalize();
    }
}
