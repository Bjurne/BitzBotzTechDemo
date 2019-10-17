using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, IFireWeapon
{
    public string weaponName;
    public SpriteRenderer weaponSprite;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float coolDown;
    public bool onCoolDown;
    public int numberOfProjectiles = 1;
    public float projectileDeviation;
    public float weaponRecoilPower;
    public bool projectilesAreMouseSeeking;

    public Transform activeWeaponSlot;

    public WeaponData weaponData;

    public Image reloadImage;

    //public Weapon (WeaponData weaponData, Transform activeWeaponSlot)
    //{
    //    this.weaponData = weaponData;
    //    SetWeaponData();
    //    this.activeWeaponSlot = activeWeaponSlot;
    //}

    private void Awake()
    {
        SetWeaponData();
    }

    public void SetupWeapon(WeaponData weaponData, Transform activeWeaponSlot)
    {
        this.weaponData = weaponData;
        SetWeaponData();
        this.activeWeaponSlot = activeWeaponSlot;
    }

    public void SetWeaponData()
    {
        if (weaponData != null)
        {
            weaponName = weaponData.name;
            weaponSprite.sprite = weaponData.sprite;
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpeed = weaponData.projectileSpeed;
            coolDown = weaponData.coolDown;
            numberOfProjectiles = weaponData.numberOfProjectiles;
            projectileDeviation = weaponData.projectileDeviation;
            weaponRecoilPower = weaponData.weaponRecoilPower;
            projectilesAreMouseSeeking = weaponData.projectilesAreMouseSeeking;

            name = weaponName;
        }
    }

    public void FireWeapon()
    {
        if (!onCoolDown)
        {
            StartCoroutine(CoolDown());
            StartCoroutine(Recoil());

            //FireMouseSeekingMissileShot();
            //FireRifleShot();
            //FireBirdshot();
            FireWeaponMasterFunction();
        }
    }

    public void FireRifleShot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, activeWeaponSlot);
        newProjectile.transform.SetParent(null);
        //NOTE If we don't unchild the projectile from the weapon we can create really cool guided projectiles
        newProjectile.GetComponent<Rigidbody2D>().AddForce(activeWeaponSlot.right * projectileSpeed);
        // Add deviation here too?

    }

    public void FireBirdshot()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, activeWeaponSlot);
            newProjectile.transform.Translate(UnityEngine.Random.insideUnitCircle / 10f);
            newProjectile.transform.SetParent(null);
            float newDeviationX = Random.Range(-projectileDeviation, projectileDeviation);
            float newDeviationY = Random.Range(-projectileDeviation, projectileDeviation);
            Vector2 playerVelocity = GetComponentInParent<PlayerController>().playerRigidBody.velocity; // <---- TODO solve. Since we dont apply force for movement, this number means nothing
            Vector3 thisProjectileTrajectory = new Vector3(activeWeaponSlot.right.x + newDeviationX, activeWeaponSlot.right.y + newDeviationY, 0f);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(thisProjectileTrajectory * projectileSpeed);
        }
    }

    public void FireMouseSeekingMissileShot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, activeWeaponSlot);
        //newProjectile.transform.SetParent(null);
        //NOTE If we don't unchild the projectile from the weapon we can create really cool guided projectiles
        newProjectile.GetComponent<Rigidbody2D>().AddForce(activeWeaponSlot.right * projectileSpeed);
    }

    public void FireWeaponMasterFunction()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, activeWeaponSlot);
            float newDeviationX = Random.Range(-projectileDeviation, projectileDeviation);
            float newDeviationY = Random.Range(-projectileDeviation, projectileDeviation);
            if (numberOfProjectiles < 1)
            {
                newProjectile.transform.Translate(UnityEngine.Random.insideUnitCircle / 10f);
            }
            if (!projectilesAreMouseSeeking)
            {
                newProjectile.transform.SetParent(null);
            }
            //Vector2 playerVelocity = GetComponentInParent<PlayerController>().playerRigidBody.velocity; // <---- TODO solve. Since we dont apply force for movement, this number means nothing
            Vector3 thisProjectileTrajectory = new Vector3(activeWeaponSlot.right.x + newDeviationX, activeWeaponSlot.right.y + newDeviationY, 0f);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(thisProjectileTrajectory * projectileSpeed);
        }
    }

    public IEnumerator Recoil()
    {
        transform.root.GetComponent<Rigidbody2D>().AddForce(activeWeaponSlot.right * -weaponRecoilPower);
        transform.Translate(new Vector3(-0.1f, 0f, 0f));
        yield return new WaitForSeconds(0.1f);
        transform.Translate(new Vector3(0.1f, 0f, 0f));
        transform.localPosition = new Vector3(0f, 0f, 0f);

        yield return null;
    }

    public IEnumerator CoolDown()
    {
        float timeSinceLastShot = 0;
        onCoolDown = true;

        while (timeSinceLastShot < coolDown)
        {
            yield return new WaitForSeconds(0.05f);
            timeSinceLastShot += Time.deltaTime * 10f;
            reloadImage.fillAmount = timeSinceLastShot / coolDown;
        }

        reloadImage.fillAmount = 0f;
        onCoolDown = false;
        yield return null;
    }
}
