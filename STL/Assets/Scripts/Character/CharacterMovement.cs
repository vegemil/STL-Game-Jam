using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{ 
    public Action OnEndMove;

    [SerializeField] private float accSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float locateAreaLength = 0.5f;

    Vector3 moveDirection; 
    Vector3 destPoint;

    bool isMoving;


    private void Update()
    {
        CheckMove();
    }


    public void MoveToPoint(Vector3 newDestPoint, bool isNow = false)
    {
        destPoint = newDestPoint;
        moveDirection = Vector3.Normalize(destPoint - gameObject.transform.position);

        if (isNow)
            gameObject.transform.position = destPoint;

    }


    void CheckMove()
    {

        bool isLocated = locateAreaLength > (gameObject.transform.position - destPoint).sqrMagnitude;
        if (isLocated)
        {
            if (isMoving && OnEndMove != null)
                OnEndMove();
            isMoving = false;

            return;
        }

        UpdateMove();
    }

    void UpdateMove()
    {
        Vector3 addPosition;

        isMoving = true;
        moveDirection = Vector3.Normalize(destPoint - gameObject.transform.position);
        addPosition = moveDirection * maxSpeed * Time.deltaTime;
         


        transform.position += addPosition;
    }


}
