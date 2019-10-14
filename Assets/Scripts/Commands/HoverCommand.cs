using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private Vector2 jumpVector;

    public HoverCommand(Rigidbody2D playerRigidbody, Vector2 jumpVector)
    {
        this.playerRigidbody = playerRigidbody;
        this.jumpVector = jumpVector;
    }

    public void Execute()
    {
        GameObject.FindObjectOfType<MonoScript>().StartCoroutine(Hover());
    }

    public IEnumerator Hover()
    {
        for (int i = 0; i < 30; i++)
        {
            if (playerRigidbody.GetComponent<PlayerController>().grounded) yield break;
            if (playerRigidbody.velocity.y < 5f)
            {
                playerRigidbody.AddForce(jumpVector/4);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return null;
    }
}
