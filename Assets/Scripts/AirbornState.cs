using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : IState
{
    PlayerController playerController;

    public AirbornState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("New state - Airborn");
        playerController.grounded = false;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Leaving state - Airborn");
        //playerController.grounded = true;
    }
}
