using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : IState
{
    Vector2 jumpVector;
    Rigidbody2D jumpingBody;
    StateMachine stateMachine;
    PlayerController playerController;

    public DashingState(Rigidbody2D jumpingBody, PlayerController playerController, Vector2 jumpVector, StateMachine stateMachine)
    {
        this.jumpingBody = jumpingBody;
        this.jumpVector = jumpVector;
        this.stateMachine = stateMachine;
        this.playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("New state - DashingState");

        if (Mathf.Abs(jumpingBody.velocity.y) < 6f && Mathf.Abs(jumpingBody.velocity.x) < 6f)
        {
            playerController.hoveringStage += 2;

            Vector2 dashDirection = playerController.activeWeaponSlot.right;

            playerController.hoverEffectPS.Play();
            jumpingBody.AddForce(dashDirection * jumpVector.y * 1.5f); // <---- NOTE Good place for float extremePowerDashingSkillVariable
        }
        else
        {
            // player state == stalling? Might be harsh
            playerController.hoverFailEffectPS.Play();
        }

        stateMachine.SwitchToPreviousState();
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        Debug.Log("Leaving state - DashingState");
    }
}
