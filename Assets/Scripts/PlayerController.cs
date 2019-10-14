using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public float movementSpeed;

    public float jumpingPower;
    private Vector2 jumpVector;

    private Weapon activeWeapon;
    public List<Weapon> equippedWeapons;

    public Transform activeWeaponSlot;
    public Transform InactiveWeaponsSlot;

    private float weaponDegrees;
    public float weaponSpeed;

    private Vector3 mousePosition;
    private Vector3 weaponPosition;
    private float angle;

    private Collider2D groundedTrigger; // Not in use right now. Might be needed eventually.
    public bool grounded;
    public bool hovering;

    public List<IModule> integratedModules; // A list of modules currently integrated, might come in handy
    public int hullPoints;


    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpVector = new Vector2(0, jumpingPower);
        
        activeWeapon = equippedWeapons[0];
        activeWeapon.gameObject.SetActive(true);
        activeWeapon.transform.SetParent(activeWeaponSlot);
    }

    private void IntegratedStartModules()
    {
        integratedModules = new List<IModule>();
        while (integratedModules.Count < 5)
        {
            if (integratedModules.Count > 3)
            {
                IModule module = new WeaponModule();
                module.IntegrateModule(this);
                integratedModules.Add(module);
            }
            else
            {
                IModule module = new HullModule();
                module.IntegrateModule(this);
                integratedModules.Add(module);
            }
        }
    }

    public void MovementInput()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        ICommand command = new MoveCommand(playerRigidBody, x);
        CommandInvoker.AddCommand(command);
    }

    public void JumpInput()
    {
        if (grounded)
        {
            ICommand command = new SnappyJumpCommand(playerRigidBody, jumpVector);
            CommandInvoker.AddCommand(command);
        }
        else if (!hovering)
        {
            hovering = true;
            ICommand command = new HoverCommand(playerRigidBody, jumpVector);
            CommandInvoker.AddCommand(command);
        }
        else if (!grounded && hovering)
        {
            GameObject.FindObjectOfType<MonoScript>().StopAllCoroutines(); //TODO fix proper hovering cancelation
        }
    }

    public void RotateActiveWeaponKeyboardInput(bool clockwise)
    {
        if (clockwise) weaponDegrees -= weaponSpeed;
        else weaponDegrees += weaponSpeed;
        activeWeaponSlot.transform.eulerAngles = Vector3.forward * weaponDegrees;
    }

    public void RotateActiveWeaponMouseInput()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        weaponPosition = Camera.main.WorldToScreenPoint(activeWeaponSlot.position);
        mousePosition.x = mousePosition.x - weaponPosition.x;
        mousePosition.y = mousePosition.y - weaponPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        activeWeaponSlot.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void ChangeActiveWeapon(int x)
    {
        Quaternion originalWeaponRotation = activeWeapon.transform.rotation;
        activeWeapon.StopAllCoroutines();
        activeWeapon.gameObject.SetActive(false);
        activeWeapon.transform.SetParent(InactiveWeaponsSlot);

        activeWeapon = equippedWeapons[x - 1];

        activeWeapon.gameObject.SetActive(true);
        activeWeapon.transform.rotation = originalWeaponRotation;
        activeWeapon.transform.SetParent(activeWeaponSlot);
        StartCoroutine(activeWeapon.CoolDown());
    }

    public void FireActiveWeapon()
    {
        activeWeapon.FireWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundPlatform"))
        {
            hovering = false;
            grounded = true;
            //GameObject.FindObjectOfType<MonoScript>().StopAllCoroutines();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundPlatform"))
        {
            grounded = false;
        }
    }
}
