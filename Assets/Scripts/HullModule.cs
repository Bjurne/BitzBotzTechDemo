using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullModule : IModule
{
    public void IntegrateModule(PlayerController playerController)
    {
        playerController.hullPoints += 5;
        //thisModuleType = ModuleType.HullModule;
        //thisWeaponModuleType = WeaponModuleType.None;
    }
}
