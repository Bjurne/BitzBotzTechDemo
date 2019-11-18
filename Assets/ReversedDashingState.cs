using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversedDashingState : DashingState
{
    public ReversedDashingState(Rigidbody2D jumpingBody, PlayerController playerController, Vector2 jumpVector, StateMachine stateMachine) : base(jumpingBody, playerController, jumpVector, stateMachine)
    {
        this.jumpVector = jumpVector * -1;
        //Debug.Log("ReversedDashingState jumpvector = " + jumpVector);
    }
}
