using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponButton : MonoBehaviour
{
    public PlayerController playerController;
    public int targetWeaponIndex;

    SelectWeaponButton()
    {
        playerController.ChangeActiveWeapon(targetWeaponIndex);
    }
}
