using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, ITakeDamage
{
    private Weapon activeWeapon;
    public List<Weapon> equippedWeapons;

    public Transform activeWeaponSlot;
    public Transform InactiveWeaponsSlot;

    private float weaponDegrees;
    public float weaponSpeed;

    [HideInInspector]
    public GameObject acquiredTarget;

    private Vector3 targetPosition;
    private Vector3 weaponPosition;
    private float angle;

    public int hullPoints = 25;
    public int criticalDamageThreshold = 6;

    public float respawnTime;

    public void TakeDamage(int value)
    {
        hullPoints -= value;
        if (value >= criticalDamageThreshold)
        {
            ObjectPoolManager.Instance.SpawnFromPool("BitzBox", this.transform.position);
            ObjectPoolManager.Instance.SpawnFromPool("BitzBox", this.transform.position);
            ObjectPoolManager.Instance.SpawnFromPool("BitzBox", this.transform.position);
            ObjectPoolManager.Instance.SpawnFromPool("Explosion", this.transform.position);
            if (respawnTime != 0f)
            {
                gameObject.SetActive(false);
                Invoke("Respawn", respawnTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (hullPoints <= 0)
        {
            if (respawnTime != 0f)
            {
                gameObject.SetActive(false);
                Invoke("Respawn", respawnTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        activeWeapon = equippedWeapons[0];
        activeWeapon.gameObject.SetActive(true);
        activeWeapon.transform.SetParent(activeWeaponSlot);
    }

    void Respawn()
    {
        hullPoints = 25;
        gameObject.SetActive(true);
    }
    
    //void Update()
    //{
    //    if (!activeWeapon.onCoolDown)
    //    {
    //        RotateActiveWeaponToAcquiredTarget();
    //    }
    //}

    public void RotateActiveWeaponToAcquiredTarget()
    {
        targetPosition = acquiredTarget.transform.position;
        targetPosition.z = 0f;
        //weaponPosition = Camera.main.WorldToScreenPoint(activeWeaponSlot.position);
        weaponPosition = activeWeaponSlot.position;
        targetPosition.x = targetPosition.x - weaponPosition.x;
        targetPosition.y = targetPosition.y - weaponPosition.y;
        angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        activeWeaponSlot.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (acquiredTarget == null) acquiredTarget = collision.gameObject;

            RotateActiveWeaponToAcquiredTarget();

            if (!activeWeapon.onCoolDown)
            {
                activeWeapon.FireWeapon();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == acquiredTarget)
        {
            acquiredTarget = null;
        }
    }
}
