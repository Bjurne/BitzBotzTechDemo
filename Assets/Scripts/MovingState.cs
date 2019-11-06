using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState
{
    Rigidbody2D movingBody;
    float movementSpeed;
    float secondsOfNoMovementBeforeIdle;
    float secondsOfNoInput;

    private System.Action<IdlingResults> idlingResultsCallback;

    public MovingState(Rigidbody2D movingBody, float movementSpeed, float secondsOfNoMovementBeforeIdle = 10f, System.Action<IdlingResults> idlingResultsCallback = null)
    {
        this.movingBody = movingBody;
        this.movementSpeed = movementSpeed;
        this.secondsOfNoMovementBeforeIdle = secondsOfNoMovementBeforeIdle;
        this.idlingResultsCallback = idlingResultsCallback;
    }

    public void Enter()
    {
        secondsOfNoInput = 0f;
        Debug.Log("New state - MovingState - ");
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        movingBody.AddForce(new Vector2(x, 0f));

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
        Debug.Log("leaving state - MovingState");
    }
}
