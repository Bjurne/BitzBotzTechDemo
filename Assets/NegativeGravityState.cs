using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeGravityState : IState
{
    PlayerController playerController;
    float originalGravityModifier;

    float timeToFloat;
    float timeSpentFloating;
    float movementSpeed;

    public NegativeGravityState(PlayerController playerController, float timeToFloat = 0f)
    {
        if (timeToFloat == 0) timeToFloat = 3f;
        this.timeToFloat = timeToFloat;
        this.playerController = playerController;
        originalGravityModifier = playerController.playerRigidBody.gravityScale;
        movementSpeed = playerController.movementSpeed;
    }

    public void Enter()
    {
        timeSpentFloating = 0f;
        playerController.aerialStage += 10;

        originalGravityModifier = playerController.playerRigidBody.gravityScale;

        if (originalGravityModifier > 0f)
        {
            playerController.playerRigidBody.gravityScale = originalGravityModifier * -0.1f;
        }
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        playerController.playerRigidBody.AddForce(new Vector2(x, 0f));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.stateMachine.SwitchToPreviousState();
        }

        if (timeSpentFloating < timeToFloat)
        {
            timeSpentFloating += Time.deltaTime;
        }
        else
        {
            playerController.stateMachine.SwitchToPreviousState();
        }
    }

    public void Exit()
    {
        if (originalGravityModifier > 0f)
        {
            playerController.playerRigidBody.gravityScale = originalGravityModifier;
        }
    }
}
