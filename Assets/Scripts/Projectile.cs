using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float secondsActiveBeforeDespawned;
    protected bool active = false;
    public string fireingCharacterName;
    public GameObject impactParticlesPrefab;
    public SpriteRenderer sprite;
    public int projectileDamage = 1;

    [HideInInspector]
    public int weaponDamage = 0;

    protected virtual void OnEnable()
    {
        //weaponDamage = GetComponentInParent<Weapon>().weaponDamage;
        active = false;
        fireingCharacterName = transform.root.gameObject.name;
        //Destroy(this.gameObject, secondsActiveBeforeDespawned);
        Invoke("ReturnToPool", secondsActiveBeforeDespawned);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == fireingCharacterName)
        {
            active = true;
        }
    }

    //private void ActivateProjectile()
    //{
    //    active = true;
    //    Collider2D collider = GetComponent<Collider2D>();
    //    if (collider.)
    //    {

    //    }
    //}

    private void OnDisable()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (gameObject.activeInHierarchy)
        {
            ObjectPoolManager.Instance.ReturnObjectHome(this.gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea"))
        {
            if (active)
            {
                //GameObject newImpactFX = Instantiate(impactParticlesPrefab, this.transform);
                //newImpactFX.transform.SetParent(null);
                //Destroy(newImpactFX, 1f);
                ObjectPoolManager.Instance.SpawnFromPool("Sparkles", transform.position);

                ITakeDamage takeDamageInterface = collision.gameObject.GetComponent<ITakeDamage>();
                if (takeDamageInterface != null)
                {
                    takeDamageInterface.TakeDamage(projectileDamage + weaponDamage);
                }
                //Destroy(this.gameObject);
                ObjectPoolManager.Instance.ReturnObjectHome(this.gameObject);
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea") && collision.gameObject.name != fireingCharacterName)
        {
            if (active)
            {
                //GameObject newImpactFX = Instantiate(impactParticlesPrefab, this.transform);
                //newImpactFX.transform.SetParent(null);
                //Destroy(newImpactFX, 1f);
                ObjectPoolManager.Instance.SpawnFromPool("Sparkles", transform.position);

                ITakeDamage takeDamageInterface = collision.gameObject.GetComponent<ITakeDamage>();
                if (takeDamageInterface != null)
                {
                    takeDamageInterface.TakeDamage(projectileDamage + weaponDamage);
                }
                //Destroy(this.gameObject);
                ObjectPoolManager.Instance.ReturnObjectHome(this.gameObject);
            }
        }
    }
}
