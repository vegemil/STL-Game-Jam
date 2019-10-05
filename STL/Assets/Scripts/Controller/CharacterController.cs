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

    private void Update()
    {
        UpdateAnimationState();
    }

    public void Initalize()
    {
        character.Initalize();
        movement.Initalize();
        PlayerCamera.onClickObject = OnClickObject; 
    }

    void UpdateAnimationState()
    {
        if(movement.Speed <= 0)
        {
            if (character.SpriteAnimator.CurrentAnimationState == SpriteAnimator.AnimationState.Idle)
                return;
            character.SpriteAnimator.SwitchState(SpriteAnimator.AnimationState.Idle);
            character.SpriteAnimator.SetAnimationSpeed(1);
        }
        else
        {
            if (character.SpriteAnimator.CurrentAnimationState != SpriteAnimator.AnimationState.Walk)
                character.SpriteAnimator.SwitchState(SpriteAnimator.AnimationState.Walk);

            float animationSpeedMultipler = Mathf.Clamp((movement.Speed / movement.MaxSpeed), 0, 1);
            character.SpriteAnimator.SetAnimationSpeed(animationSpeedMultipler);
        }

        

    }

    void OnClickObject(RaycastEventBinder raycastTarget)
    {
        Vector3 deltaPos = raycastTarget.transform.position - gameObject.transform.position;
        bool isRight = deltaPos.x > 0;
        if (isRight)
            character.SpriteAnimator.SetImageFlipX(true);
        else
            character.SpriteAnimator.SetImageFlipX(false);
        movement.MoveToPoint(raycastTarget.transform.position);
    }

}
