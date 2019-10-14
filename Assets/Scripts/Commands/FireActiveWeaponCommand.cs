using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActiveWeaponCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private Vector2 jumpVector;

    public FireActiveWeaponCommand(Rigidbody2D playerRigidbody)
    {
        this.playerRigidbody = playerRigidbody;
    }

    public void Execute()
    {

    }
}
