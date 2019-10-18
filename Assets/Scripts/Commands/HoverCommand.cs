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
        PlayerController playerController = playerRigidbody.GetComponent<PlayerController>();
        playerController.hoveringStage += 5;

        for (int i = 0; i < 30; i++)
        {
            if (playerController.grounded) yield break;
            if (playerRigidbody.velocity.y < 5f)
            {
                playerController.hoverEffectPS.Play();
                playerRigidbody.AddForce(jumpVector/4);
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return null;
    }
}
