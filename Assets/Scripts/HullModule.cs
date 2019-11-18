using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullModule : Module
{
    public override void IntegrateModule(PlayerController playerController)
    {
        //Debug.Log("Integrating HullModule");
        thisModuleType = ModuleType.HullModule;
        playerController.hullPoints += 5;
        //thisModuleType = ModuleType.HullModule;
        //thisWeaponModuleType = WeaponModuleType.None;
    }

    public override void removeModule(PlayerController playerController)
    {
        playerController.hullPoints -= 5;
        base.removeModule(playerController);
    }
}
