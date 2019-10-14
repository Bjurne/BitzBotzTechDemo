using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappyJumpCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private Vector2 jumpVector;

    public SnappyJumpCommand(Rigidbody2D playerRigidbody, Vector2 jumpVector)
    {
        this.playerRigidbody = playerRigidbody;
        this.jumpVector = jumpVector;
    }

    public void Execute()
    {
        playerRigidbody.transform.Translate(Vector2.up * 0.25f);
        playerRigidbody.AddForce(jumpVector);
    }
}
