using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour, IModule
{

    public enum ModuleType { HullModule, WeaponModule, None };
    protected ModuleType thisModuleType;

    public enum WeaponModuleType { HullModule, WeaponModule, None };
    protected WeaponModuleType thisWeaponModuleType;

    public int hullPoints;

    public void IntegrateModule(PlayerController playerController)
    {
        hullPoints = 0;
        thisModuleType = ModuleType.None;
        thisWeaponModuleType = WeaponModuleType.None;
        Debug.Log("Module");
    }
}
