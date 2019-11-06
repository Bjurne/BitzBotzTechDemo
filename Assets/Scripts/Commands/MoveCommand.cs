using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private float velocityX;

    public MoveCommand(Rigidbody2D playerRigidbody, float velocityX)
    {
        this.playerRigidbody = playerRigidbody;
        this.velocityX = velocityX;
    }

    public void Execute()
    {
        playerRigidbody.AddForce(new Vector2( velocityX, 0f ));
        //playerRigidbody.transform.Translate(velocityX, 0, 0);
    }
}
