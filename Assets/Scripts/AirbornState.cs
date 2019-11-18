using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : IState
{
    PlayerController playerController;
    float movementSpeed;

    public AirbornState(PlayerController playerController)
    {
        this.playerController = playerController;
        movementSpeed = playerController.movementSpeed;
    }

    public void Enter()
    {
        //Debug.Log("New state - Airborn");
        playerController.grounded = false;
    }

    public void Execute()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        playerController.playerRigidBody.AddForce(new Vector2(x, 0f));
    }

    public void Exit()
    {
        //Debug.Log("Leaving state - Airborn");
        //playerController.grounded = true;
    }
}
