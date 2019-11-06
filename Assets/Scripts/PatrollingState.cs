using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    Rigidbody2D patrollingRigidbody;
    float patrollingInput;
    float movementSpeed;

    private float translationValue;

    public PatrollingState(Rigidbody2D patrollingRigidbody, float patrollingInput, float movementSpeed)
    {
        this.patrollingRigidbody = patrollingRigidbody;
        this.patrollingInput = patrollingInput;
        this.movementSpeed = movementSpeed;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        translationValue = patrollingInput * Time.deltaTime * movementSpeed;
        patrollingRigidbody.transform.Translate(translationValue, 0, 0);
    }

    public void Exit()
    {
        
    }
}
