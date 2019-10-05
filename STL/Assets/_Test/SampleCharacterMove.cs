using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacterMove : MonoBehaviour
{

    [SerializeField] private RaycastEventBinder[] eventBinders;
    [SerializeField] private CharacterController characterController;

    private void Start()
    {
        foreach(var binders in eventBinders)
        {
            binders.onClick = OnClickObject;
        }
    }

    void OnClickObject(RaycastEventBinder clickObject)
    {
        characterController.Movement.MoveToPoint(clickObject.transform.position);
    }

}
