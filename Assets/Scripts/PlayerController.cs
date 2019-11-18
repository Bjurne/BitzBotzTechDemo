using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    public Rigidbody2D playerRigidBody;
    public float movementSpeed;

    public SpriteRenderer chassiSpriteRenderer;

    public float jumpingPower;
    [HideInInspector]
    public Vector2 jumpVector;

    private Weapon activeWeapon;
    public int previouslyActiveWeaponIndex;
    public Weapon[] equippedWeapons;

    [SerializeField]
    public Dictionary<int, WeaponData> prototypeWeaponDatasDictionary; // <--- Probably use something like this to handle weapon selection

    public Transform activeWeaponSlot;
    public Transform InactiveWeaponsSlot;

    private float weaponDegrees;
    public float weaponSpeed;

    private Vector3 mousePosition;
    private Vector3 weaponPosition;
    private float angle;

    private Collider2D groundedTrigger;
    public bool grounded;
    public int aerialStage;

    
    public int hullPoints;
    public int criticalDamageThreshold;

    public WeaponData prototypeWeaponData;
    public Weapon weaponPrefab;
    

    public int numberOfWeaponSlots;

    public ParticleSystem hoverEffectPS;
    public ParticleSystem hoverFailEffectPS;

    public ModuleManager moduleManager;

    public StateMachine stateMachine = new StateMachine();

    public void TakeDamage(int value)
    {
        hullPoints -= value;
        if (value >= criticalDamageThreshold)
        {
            stateMachine.ChangeState(new StallingState(stateMachine, StallingCycleCompleted, 1f));
            moduleManager.RemoveRandomModule();
            moduleManager.RemoveRandomModule();
            moduleManager.RemoveRandomModule();
        }
        if (hullPoints <= 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        Debug.Log("You ded");
        //moduleManager.RemoveAllModules();
        //foreach (Weapon weapon in equippedWeapons)
        //{
        //    RemoveWeapon(System.Array.IndexOf(equippedWeapons, weapon));
        //}
        transform.position = new Vector2(-24f, 8f);
        playerRigidBody.velocity = Vector2.zero;
        hullPoints = 15;
        //moduleManager.IntegrateStartModules();
    }

    private void Awake()
    {
        chassiSpriteRenderer.color = Color.blue;
        equippedWeapons = new Weapon[numberOfWeaponSlots];
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpVector = new Vector2(0, jumpingPower);
        //moduleManager = new ModuleManager(this);
    }

    private void Start()
    {

        stateMachine.ChangeState(new IdleState(10f, IdlingLoopRestart));
        
        //prototypeWeaponDatasDictionary = new Dictionary<int, WeaponData>();


        //for (int i = 0; i < prototypeWeaponDatas.Length; i++)
        //{
        //    prototypeWeaponDatasDictionary.Add(i, prototypeWeaponDatas[i]);
        //}

        //Weapon[] startingWeapons = GetComponentsInChildren<Weapon>(true);

        //for (int i = 0; i < startingWeapons.Length; i++)
        //{
        //    Debug.Log(i + " - " + prototypeWeaponDatasDictionary[i]);
        //}
        //Debug.Log(prototypeWeaponDatasDictionary.Count);
        
        
    }

    private void Update()
    {
        stateMachine.ExecuteStateUpdate();
    }

    public void AddStartingWeapons()
    {
        Weapon[] startingWeapons = GetComponentsInChildren<Weapon>(true);

        for (int i = 0; i < startingWeapons.Length; i++)
        {
            equippedWeapons[i] = startingWeapons[i];
        }

        activeWeapon = equippedWeapons[0];
        previouslyActiveWeaponIndex = System.Array.IndexOf(equippedWeapons, activeWeapon);
        activeWeapon.gameObject.SetActive(true);
        activeWeapon.transform.SetParent(activeWeaponSlot);
    }

    public void MovementInput()
    {
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        //ICommand command = new MoveCommand(playerRigidBody, x);
        //CommandInvoker.AddCommand(command);

        if (stateMachine.currentlyRunningState == stateMachine.currentlyRunningState as IdleState)
        {
            stateMachine.ChangeState(new SnappyMovingState(this, playerRigidBody, movementSpeed, 10f, IdlingLoopRestart));
        }
    }

    public void StopInput()
    {
        if (grounded)
        {
            //ICommand command = new StopCommand(playerRigidBody);
            //CommandInvoker.AddCommand(command);
        }
    }

    public void JumpInput()
    {
        //if (stateMachine.currentlyRunningState != stateMachine.currentlyRunningState as AirbornState)
        if (grounded)
        {
            //ICommand command = new SnappyJumpCommand(playerRigidBody, jumpVector);
            //CommandInvoker.AddCommand(command);

            stateMachine.ChangeState(new SnappyJumpState(playerRigidBody, jumpVector, stateMachine));

            //moduleManager.currentAerialModule.activateModuleSpecial(this);
        }
        else if (aerialStage < 5)
        {
            //hoveringStage += 1;
            //ICommand command = new DashCommand(playerRigidBody, jumpVector);
            //CommandInvoker.AddCommand(command);
            //stateMachine.ChangeState(new DashingState(playerRigidBody, this, jumpVector, stateMachine));
            if (moduleManager.currentAerialModule != null)
            {
                moduleManager.currentAerialModule.activateModuleSpecial(this);
            }
        }
        else if (!grounded && aerialStage >= 5)
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
        //if (weaponIndex <= CountEquippedWeapons() -1)
        if (equippedWeapons[weaponIndex] != null)
        {
            Quaternion originalWeaponRotation;
            if (activeWeapon != null)
            {
                originalWeaponRotation = activeWeapon.transform.rotation;
                activeWeapon.StopAllCoroutines();
                activeWeapon.gameObject.SetActive(false);
                activeWeapon.transform.SetParent(InactiveWeaponsSlot);
                previouslyActiveWeaponIndex = System.Array.IndexOf(equippedWeapons, activeWeapon);
            }
            else
            {
                activeWeaponSlot.rotation = Quaternion.identity;
                originalWeaponRotation = Quaternion.identity;
            }

            activeWeapon = equippedWeapons[weaponIndex];
            
            activeWeapon.gameObject.SetActive(true);
            activeWeapon.transform.rotation = originalWeaponRotation;
            activeWeapon.transform.SetParent(activeWeaponSlot);
            activeWeapon.onCoolDown = true;
            //StartCoroutine(activeWeapon.CoolDown());
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
        if (numberOfWeaponSlots > CountEquippedWeapons())
        {
            return true;
        }
        else return false;
    }

    //private WeaponData SetPrototypeWeapon()
    //{
    //    //int nextWeaponIndex = Mathf.Clamp(equippedWeapons.Count -1, 0, prototypeWeaponDatas.Length);
    //    int nextWeaponIndex = CountEquippedWeapons();
    //    //int nextWeaponIndex = equippedWeaponsDictionary.Count;
    //    if (prototypeWeaponDatas[nextWeaponIndex] != null)
    //    {
    //        prototypeWeaponData = prototypeWeaponDatas[nextWeaponIndex];
    //        //prototypeWeaponDatas.Remove(prototypeWeaponDatas[nextWeaponIndex]);
    //    }
    //    return prototypeWeaponData;
    //}

    private WeaponData SetPrototypeWeapon()
    {
        int nextWeaponIndex = SetNextWeaponIndex();
        if (moduleManager.prototypeWeaponDatas[nextWeaponIndex] != null)
        {
            prototypeWeaponData = moduleManager.prototypeWeaponDatas[nextWeaponIndex];
        }
        return prototypeWeaponData;
    }

    public int SetNextWeaponIndex()
    {
        int nextWeaponIndex = -1;
        foreach (Weapon weapon in equippedWeapons)
        {
            if (weapon == null)
            {
                nextWeaponIndex = (System.Array.IndexOf(equippedWeapons, weapon));
                return nextWeaponIndex;
            }
        }
        return nextWeaponIndex;
    }

    //public void AddWeapon()
    //{
    //    WeaponData newWeaponData = ScriptableObject.CreateInstance<WeaponData>();
    //    SetPrototypeWeapon();
    //    newWeaponData = prototypeWeaponData;
    //    Weapon newWeapon = Instantiate(weaponPrefab, InactiveWeaponsSlot);
    //    newWeapon.gameObject.SetActive(false);
    //    newWeapon.SetupWeapon(newWeaponData, activeWeaponSlot);

    //    int listPlace = -1;

    //    foreach (WeaponData weaponData in moduleManager.prototypeWeaponDatas)
    //    {
    //        if (weaponData == newWeaponData)
    //        {
    //            //listPlace = prototypeWeaponDatas.IndexOf( prototypeWeaponDatas, weaponData);
    //            listPlace = System.Array.IndexOf(moduleManager.prototypeWeaponDatas, weaponData);
    //            //Debug.Log(newWeaponData + " added to equippedWeapons at index " + listPlace);
    //        }
    //    }

    //    // TODO fix this mess. Unsafe af.
    //    if (listPlace != -1) equippedWeapons[listPlace] = newWeapon;
    //    else
    //    {
    //        //equippedWeapons.Add(newWeapon);
    //        Debug.LogWarning("Weapon inserted incorrectly into PlayerController equippedWeapons");
    //    }
    //    //int newWeaponIndex = System.Array.IndexOf(equippedWeapons, newWeapon);
    //    ChangeActiveWeapon(System.Array.IndexOf(equippedWeapons, newWeapon));
    //}

    public void AddWeapon()
    {
        int newWeaponIndex = SetNextWeaponIndex();
        WeaponData newWeaponData = ScriptableObject.CreateInstance<WeaponData>();
        newWeaponData = moduleManager.GetPrototypeWeaponData(newWeaponIndex);
        Weapon newWeapon = Instantiate(weaponPrefab, InactiveWeaponsSlot);
        newWeapon.gameObject.SetActive(false);
        newWeapon.SetupWeapon(newWeaponData, activeWeaponSlot);
        
        equippedWeapons[newWeaponIndex] = newWeapon;
        if (activeWeapon == null) ChangeActiveWeapon(System.Array.IndexOf(equippedWeapons, newWeapon));
    }

    //public void RemoveWeapon()
    //{
    //    //TODO: fix so that lost weapons reappear in the same keyboard number slot after being removed and readded.
    //    //This will allow us to eject random weapon, without making it weird.

    //    int randomWeaponIndex = Random.Range(0, equippedWeapons.Length);
    //    Debug.Log("randomWeaponIndex: " + randomWeaponIndex);
    //    //prototypeWeaponDatas.Add(equippedWeapons[randomWeaponIndex].weaponData);

    //    if (equippedWeapons[randomWeaponIndex] == null)
    //    {
    //        Debug.Log("No weapon destroyed this time");
    //        return;
    //    }

    //    Debug.Log("trying to remove " + equippedWeapons[randomWeaponIndex] + " from equippedWeapons at index " + randomWeaponIndex);

    //    GameObject weaponToDestroy = equippedWeapons[randomWeaponIndex].gameObject;

    //    equippedWeapons[randomWeaponIndex] = null;
    //    //foreach (Weapon weapon in equippedWeapons)
    //    //{
    //    //    if (weapon != null) Debug.Log(weapon.name + " is in equippedWeapons at index " + System.Array.IndexOf(equippedWeapons, weapon));
    //    //}

    //    Destroy(weaponToDestroy);
    //    //Debug.Log("trying to Destroy " + equippedWeapons[randomWeaponIndex].name);
    //    GameObject spawnedBitBox = ObjectPoolManager.Instance.SpawnFromPool("BitzBox", transform.position);
    //    spawnedBitBox.GetComponent<Rigidbody2D>().AddForce((UnityEngine.Random.insideUnitCircle * 10f), ForceMode2D.Impulse);
    //}

    public void RemoveWeapon(int weaponIndex)
    {
        if (equippedWeapons[weaponIndex] != null)
        {
            GameObject weaponToDestroy = equippedWeapons[weaponIndex].gameObject;

            equippedWeapons[weaponIndex] = null;

            Destroy(weaponToDestroy);
        }
        else
        {
            Debug.LogWarning("Trying to remove null weapon at equippedWeapons[" + weaponIndex + "]");
        }
    }

    private int CountEquippedWeapons()
    {
        int weaponCount = 0;
        foreach (Weapon weapon in equippedWeapons)
        {
            if (weapon != null) weaponCount++;
        }
        //Debug.Log("WeaponCount: " + weaponCount);
        return weaponCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundPlatform"))
        {
            aerialStage = 0;
            grounded = true;
            stateMachine.ChangeState(new IdleState(10f, IdlingLoopRestart));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundPlatform"))
        {
            //grounded = false;
            //Debug.Log("lämnar groundplatform");
            stateMachine.ChangeState(new AirbornState(this));
        }
    }

    public void IdlingLoopRestart(IdlingResults idlingResults)
    {
        //Debug.Log("Idle loop restarted after " + idlingResults.timeIdled + "seconds.");
        stateMachine.ChangeState(new IdleState(10f, IdlingLoopRestart));
    }

    private void StallingCycleCompleted(StallingResults stallingResults)
    {
        //Debug.Log("Stalling cycle completed after " + stallingResults.timeStalled + "seconds.");
        stateMachine.ChangeState(new IdleState(10f, IdlingLoopRestart));
    }
}
