using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialTechModule : Module
{
    protected StateMachine stateMachine;
    protected Rigidbody2D rb;
    protected Vector2 jumpVector;

    protected IState aerialState;

    public override void IntegrateModule(PlayerController playerController)
    {
        Debug.Log("AerialModule integrating");
        thisModuleType = ModuleType.AerialTechModule;
        playerController.moduleManager.currentAerialModule = this;
        stateMachine = playerController.stateMachine;
        rb = playerController.playerRigidBody;
        jumpVector = playerController.jumpVector;
        playerController.moduleManager.aerialTechLevel += 5;
        aerialState = playerController.moduleManager.GetCurrentAerialTechState();
    }

    public override void removeModule(PlayerController playerController)
    {
        Debug.Log("AerialTechModule removed");
        playerController.moduleManager.aerialTechLevel -= 5;
        aerialState = playerController.moduleManager.GetCurrentAerialTechState();
        base.removeModule(playerController);
    }

    public override void activateModuleSpecial(PlayerController playerController)
    {
        //stateMachine.ChangeState(new SnappyJumpState(rb, jumpVector, stateMachine));
        stateMachine.ChangeState(aerialState);
    }
}
