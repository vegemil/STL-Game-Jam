using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{


    [SerializeField] private float moveSpeed;


    Vector3 moveDirection; 
    Vector3 destPoint;


    private void Update()
    {
        UpdateMove();
    }


    public void MoveToPoint(Vector3 newDestPoint, bool isNow)
    {
        destPoint = newDestPoint;
        moveDirection = Vector3.Normalize(newDestPoint - gameObject.transform.position);

        if (isNow)
            gameObject.transform.position = destPoint;

    }


    void CheckMove()
    {
        if (gameObject.transform.position == destPoint)
            return;

        UpdateMove();
    }

    void UpdateMove()
    {
        Vector3 addPosition;

        addPosition = moveDirection * moveSpeed * Time.deltaTime;


        bool isFinalMove = addPosition.magnitude < (moveDirection - gameObject.transform.position).magnitude;
        if (isFinalMove)
            transform.position = destPoint;
        else
            transform.position += addPosition;
    }


}
