using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    

    public PlayerController playerController;

    

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            playerController.MovementInput();
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            playerController.MovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.JumpInput();
        }
        
        //RotateActiveWeaponWithKeyboard();
        RotateActiveWeaponTowardsMouse();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerController.FireActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.ChangeActiveWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.ChangeActiveWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.ChangeActiveWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerController.ChangeActiveWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerController.ChangeActiveWeapon(4);
        }
    }

    private void RotateActiveWeaponWithKeyboard()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            playerController.RotateActiveWeaponKeyboardInput(false);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            playerController.RotateActiveWeaponKeyboardInput(true);
        }
    }

    void RotateActiveWeaponTowardsMouse()
    {
        playerController.RotateActiveWeaponMouseInput();
    }
}
