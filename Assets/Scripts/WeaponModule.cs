using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModule : Module
{

    public override void IntegrateModule(PlayerController playerController)
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

    public override void removeModule(PlayerController playerController)
    {
        //Remove weaponModule
    }
}
