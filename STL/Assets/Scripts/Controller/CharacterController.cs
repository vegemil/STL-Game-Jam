using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private CharacterBase character;
    [SerializeField] private CameraController playerCamera;


    public CharacterMovement Movement => movement;
    public CharacterBase Character => character;
    public CameraController PlayerCamera => playerCamera;

    private void Awake()
    {
        Initalize();
    }

    public void Initalize()
    {
        movement.Initalize();
        PlayerCamera.onClickObject = OnClickObject;
    }


    void OnClickObject(RaycastEventBinder raycastTarget)
    {
        movement.MoveToPoint(raycastTarget.transform.position);
    }

}
