using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private CharacterBase character;


    public CharacterMovement Movement => movement;
    public CharacterBase Character => character;

    private void Awake()
    {
        Initalize();
    }
    public void Initalize()
    {

    }



}
