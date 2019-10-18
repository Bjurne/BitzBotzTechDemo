using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCommand : ICommand
{
    private Rigidbody2D playerRigidbody;
    private Vector2 jumpVector;
    private PlayerController playerController;

    public DashCommand(Rigidbody2D playerRigidbody, Vector2 jumpVector)
    {
        this.playerRigidbody = playerRigidbody;
        this.jumpVector = jumpVector;
        playerController = playerRigidbody.GetComponent<PlayerController>();
    }

    public void Execute()
    {
        if (Mathf.Abs(playerRigidbody.velocity.y) < 6f && Mathf.Abs(playerRigidbody.velocity.x) < 6f)
        {
            playerController.hoveringStage += 2;

            Vector2 dashDirection = playerController.activeWeaponSlot.right;

            playerController.hoverEffectPS.Play();
            playerRigidbody.AddForce(dashDirection * jumpVector.y * 1.5f); // <---- NOTE Good place for float extremePowerDashingSkillVariable
        }
        else
        {
            // player state == stalling? Might be harsh
            playerController.hoverFailEffectPS.Play();
        }
    }
}
