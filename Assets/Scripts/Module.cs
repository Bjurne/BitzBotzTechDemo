using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : IModule
{

    public enum ModuleType { None , HullModule, WeaponModule };
    protected ModuleType thisModuleType;

    public int hullPoints;

    public void IntegrateModule(PlayerController playerController)
    {
        hullPoints = 0;
        thisModuleType = ModuleType.None;
        Debug.Log("Module");
    }

    public void removeModule(PlayerController playerController)
    {

    }
}
