using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingModule : AerialTechModule
{
    public override void activateModuleSpecial(PlayerController playerController)
    {
        stateMachine.ChangeState(new SnappyJumpState(rb, jumpVector, stateMachine));
    }
}
