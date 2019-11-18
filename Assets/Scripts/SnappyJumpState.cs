using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappyJumpState : IState
{
    Vector2 jumpVector;
    Rigidbody2D jumpingBody;
    StateMachine stateMachine;

    public SnappyJumpState(Rigidbody2D jumpingBody, Vector2 jumpVector, StateMachine stateMachine)
    {
        this.jumpingBody = jumpingBody;
        this.jumpVector = jumpVector;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        jumpingBody.transform.Translate(Vector2.up * 0.25f);
        jumpingBody.AddForce(jumpVector);

        //Debug.Log("New state - SnappyJumpState");

        stateMachine.SwitchToPreviousState();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        //Debug.Log("Leaving state - SnappyJumpState - ");
    }
}
