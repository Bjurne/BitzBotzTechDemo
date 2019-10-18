using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float secondsActiveBeforeDespawned;
    private bool active = false;
    public string fireingCharacterName;
    public GameObject impactParticlesPrefab;
    public SpriteRenderer sprite;
    public int projectileDamage = 1;

    [HideInInspector]
    public int weaponDamage = 0;

    private void OnEnable()
    {
        //weaponDamage = GetComponentInParent<Weapon>().weaponDamage;
        fireingCharacterName = transform.root.gameObject.name;
        Destroy(this.gameObject, secondsActiveBeforeDespawned);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == fireingCharacterName)
        {
            active = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                ObjectPoolManager.Instance.SpawnFromPool("Sparkles", this.transform.position);
                Destroy(this.gameObject);

                ITakeDamage takeDamageInterface = collision.gameObject.GetComponent<ITakeDamage>();
                if (takeDamageInterface != null)
                {
                    takeDamageInterface.TakeDamage(projectileDamage + weaponDamage);
                }
            }
        }
    }
}
