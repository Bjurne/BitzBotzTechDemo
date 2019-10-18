using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITakeDamage
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
    public int hoveringStage;

    [SerializeField]
    public List<IModule> integratedModules; // A list of modules currently integrated, might come in handy
    public int hullPoints;

    public WeaponData prefferedWeaponData;
    public Weapon weaponPrefab;

    public int numberOfWeaponSlots;

    public ParticleSystem hoverEffectPS;
    public ParticleSystem hoverFailEffectPS;

    public void TakeDamage(int value)
    {
        hullPoints -= 1;
        if (hullPoints <= 0)
        {
            Debug.Log("You ded");
        }
    }

    private void Start()
    {
        integratedModules = new List<IModule>();
        equippedWeapons = new List<Weapon>();
        foreach (Weapon weapon in GetComponentsInChildren<Weapon>(true))
        {
            equippedWeapons.Add(weapon);
        }

        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpVector = new Vector2(0, jumpingPower);
        
        activeWeapon = equippedWeapons[0];
        activeWeapon.gameObject.SetActive(true);
        activeWeapon.transform.SetParent(activeWeaponSlot);

        IntegrateStartModules();
    }

    private void IntegrateStartModules()
    {
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

    public void AddModule(Module.ModuleType moduleType)
    {
        IModule newModule = new Module();
        switch (moduleType)
        {
            case Module.ModuleType.HullModule:
                newModule = new HullModule();
                break;
            case Module.ModuleType.WeaponModule:
                newModule = new WeaponModule();
                break;
            case Module.ModuleType.None:
                newModule = new Module();
                break;
            default:
                break;
        }
        
        newModule.IntegrateModule(this);
        integratedModules.Add(newModule);
    }

    public void RemoveModule(IModule module)
    {
        module.removeModule(this);
        integratedModules.Remove(module);
    }

    public void MovementInput()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        ICommand command = new MoveCommand(playerRigidBody, x);
        CommandInvoker.AddCommand(command);
    }

    public void StopInput()
    {
        if (grounded)
        {
            ICommand command = new StopCommand(playerRigidBody);
            CommandInvoker.AddCommand(command);
        }
    }

    public void JumpInput()
    {
        if (grounded)
        {
            ICommand command = new SnappyJumpCommand(playerRigidBody, jumpVector);
            CommandInvoker.AddCommand(command);
        }
        else if (hoveringStage < 5)
        {
            //hoveringStage += 1;
            ICommand command = new DashCommand(playerRigidBody, jumpVector);
            CommandInvoker.AddCommand(command);
        }
        else if (!grounded && hoveringStage >= 5)
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

    public void ChangeActiveWeapon(int weaponIndex)
    {
        if (weaponIndex <= equippedWeapons.Count -1)
        {
            Quaternion originalWeaponRotation = activeWeapon.transform.rotation;
            activeWeapon.StopAllCoroutines();
            activeWeapon.gameObject.SetActive(false);
            activeWeapon.transform.SetParent(InactiveWeaponsSlot);

            activeWeapon = equippedWeapons[weaponIndex];

            activeWeapon.gameObject.SetActive(true);
            activeWeapon.transform.rotation = originalWeaponRotation;
            activeWeapon.transform.SetParent(activeWeaponSlot);
            StartCoroutine(activeWeapon.CoolDown());
        }
        else
        {
            Debug.LogWarning("Trying to access an unavailable weaponIndex");
        }
    }

    public void FireActiveWeapon()
    {
        if (activeWeapon != null) activeWeapon.FireWeapon();
    }

    public bool HasFreeWeaponSlot()
    {
        if (numberOfWeaponSlots > equippedWeapons.Count)
        {
            return true;
        }
        else return false;
    }

    public void AddWeapon()
    {
        WeaponData newWeaponData = ScriptableObject.CreateInstance<WeaponData>();
        newWeaponData = prefferedWeaponData;
        Weapon newWeapon = Instantiate(weaponPrefab, InactiveWeaponsSlot);
        newWeapon.gameObject.SetActive(false);
        newWeapon.SetupWeapon(newWeaponData, activeWeaponSlot);

        equippedWeapons.Add(newWeapon);
        ChangeActiveWeapon(equippedWeapons.IndexOf(newWeapon));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundPlatform"))
        {
            hoveringStage = 0;
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
