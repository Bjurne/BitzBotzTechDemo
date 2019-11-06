using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : IModule
{

    public enum ModuleType { None , HullModule, WeaponModule, AerialTechModule };
    protected ModuleType thisModuleType;

    public int hullPoints;

    public virtual void IntegrateModule(PlayerController playerController)
    {
        hullPoints = 0;
        thisModuleType = ModuleType.None;
        Debug.LogWarning("Trying to integrate a Type.None module");

    }

    public virtual void removeModule(PlayerController playerController)
    {
        Debug.LogWarning("Trying to remove a Type.None module");
    }

    public virtual void activateModuleSpecial(PlayerController player)
    {
        Debug.LogWarning("Trying to activate special ability of a module that doesnt implement a special ability");
    }
}
