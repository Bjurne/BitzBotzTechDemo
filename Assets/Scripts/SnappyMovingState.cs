using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappyMovingState : IState
{
    PlayerController playerController;
    Rigidbody2D movingBody;
    float movementSpeed;
    float secondsOfNoMovementBeforeIdle;
    float secondsOfNoInput;

    private System.Action<IdlingResults> idlingResultsCallback;

    public SnappyMovingState(PlayerController playerController, Rigidbody2D movingBody, float movementSpeed, float secondsOfNoMovementBeforeIdle = 10f, Action<IdlingResults> idlingResultsCallback = null)
    {
        this.playerController = playerController;
        this.movingBody = movingBody;
        this.movementSpeed = movementSpeed;
        this.secondsOfNoMovementBeforeIdle = secondsOfNoMovementBeforeIdle;
        this.idlingResultsCallback = idlingResultsCallback;
    }

    public void Enter()
    {
        secondsOfNoInput = 0f;
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        if (Mathf.Abs(movingBody.velocity.x) < movementSpeed / 100f && Mathf.Abs(x) > 0f)
        {
            movingBody.AddForce(new Vector2(x / 4f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            if (playerController.grounded)
            {
                movingBody.velocity *= 0.9f;
            }
        }

        if (x == 0 && idlingResultsCallback != null)
        {
            secondsOfNoInput += Time.deltaTime;

            if (secondsOfNoInput >= secondsOfNoMovementBeforeIdle)
            {
                IdlingResults idlingResults = new IdlingResults(secondsOfNoMovementBeforeIdle);
                idlingResultsCallback(idlingResults);
            }
        }
    }

    public void Exit()
    {

    }
}
