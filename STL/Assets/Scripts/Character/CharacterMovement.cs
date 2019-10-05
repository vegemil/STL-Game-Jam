using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{ 
    public Action OnEndMove;

    [SerializeField] private float accSpeed;
    [SerializeField] private float deAccSpeed = 3;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float locateAreaLength = 1f; 

    Vector3 moveDirection; 
    Vector3 destPoint;
    [SerializeField] private float speed;
    bool isMoving;
    bool isLocated = false;

    private void Update()
    {
        CheckSpeedAcc();
        CheckMove();
    }


    public void MoveToPoint(Vector3 newDestPoint, bool isNow = false)
    {
        isMoving = true;
        destPoint = newDestPoint;
        moveDirection = Vector3.Normalize(destPoint - gameObject.transform.position);

        if (isNow)
            gameObject.transform.position = destPoint; 

    }


    void CheckSpeedAcc()
    {
        if(isLocated)
            speed -= deAccSpeed * Time.deltaTime; 
        else
            speed += accSpeed * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;
        if (speed < 0)
            speed = 0;
    }

    void CheckMove()
    {
        isLocated = locateAreaLength > (gameObject.transform.position - destPoint).sqrMagnitude;
        if (isLocated)
        {
            if (isMoving && OnEndMove != null)
                OnEndMove();
            isMoving = false;
            
        }

        UpdateMove();
    }

    void UpdateMove()
    {
        Vector3 addPosition;

        moveDirection = Vector3.Normalize(destPoint - gameObject.transform.position);
        addPosition = moveDirection * speed * Time.deltaTime;
         


        transform.position += addPosition;
    }


}
