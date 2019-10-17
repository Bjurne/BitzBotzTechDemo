using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModule : IModule
{

    public void IntegrateModule(PlayerController playerController)
    {
        if (playerController.HasFreeWeaponSlot())
        {
            playerController.AddWeapon();
        }
        else
        {
            Debug.Log("Ammo");
        }
    }

    public void removeModule(PlayerController playerController)
    {
        //Remove weaponModule
    }
}
