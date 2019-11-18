using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModule : Module
{
    private int weaponIndex;

    public override void IntegrateModule(PlayerController playerController)
    {
        thisModuleType = ModuleType.WeaponModule;
        if (playerController.HasFreeWeaponSlot())
        {
            weaponIndex = playerController.SetNextWeaponIndex();
            playerController.AddWeapon();
        }
        else
        {
            Debug.Log("Ammo");
        }
    }

    //public override void removeModule(PlayerController playerController)
    //{
    //    Debug.Log("WeaponModule removed");

    //    int randomWeaponIndex = Random.Range(0, playerController.equippedWeapons.Length);
    //    Debug.Log("randomWeaponIndex: " + randomWeaponIndex);

    //    if (playerController.equippedWeapons[randomWeaponIndex] != null)
    //    {
    //        Debug.Log("trying to remove " + playerController.equippedWeapons[randomWeaponIndex] + " from equippedWeapons at index " + randomWeaponIndex);

    //        playerController.RemoveWeapon(randomWeaponIndex);

    //        base.removeModule(playerController);
    //    }
    //    else
    //    {
    //        Debug.Log("No weapon destroyed this time");
    //    }
    //}

    public override void removeModule(PlayerController playerController)
    {
        Debug.Log("WeaponModule removed");
        
        playerController.RemoveWeapon(weaponIndex);

        base.removeModule(playerController);
    }
}
