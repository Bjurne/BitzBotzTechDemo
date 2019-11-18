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
        thisModuleType = ModuleType.None;
        Debug.LogWarning("Trying to integrate a Type.None module");
    }

    public virtual void removeModule(PlayerController playerController)
    {
        GameObject spawnedBitBox = ObjectPoolManager.Instance.SpawnFromPool("BitzBox", playerController.transform.position);
        spawnedBitBox.GetComponent<BitzBox>().SetType(thisModuleType);
        spawnedBitBox.GetComponent<Rigidbody2D>().AddForce((UnityEngine.Random.insideUnitCircle * 10f), ForceMode2D.Impulse);
    }

    public virtual void activateModuleSpecial(PlayerController player)
    {
        Debug.LogWarning("Trying to activate special ability of a module that doesnt implement a special ability");
    }
}
