using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private float velocityX;
    private Rigidbody2D rb;

    public StopCommand(Rigidbody2D playerRigidbody)
    {
        this.playerRigidbody = playerRigidbody;
    }

    public void Execute()
    {
        playerRigidbody.velocity *= 0.97f;
    }
}
