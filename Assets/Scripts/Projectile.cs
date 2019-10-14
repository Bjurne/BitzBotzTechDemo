using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float secondsActiveBeforeDespawned;
    private bool active = false;
    public string fireingCharacterName;
    public GameObject impactParticlesPrefab;

    private void Awake()
    {
        fireingCharacterName = transform.root.gameObject.name;
        Destroy(this.gameObject, secondsActiveBeforeDespawned);
    }

    void Start()
    {
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
                GameObject newImpactFX = Instantiate(impactParticlesPrefab, this.transform);
                newImpactFX.transform.SetParent(null);
                Destroy(newImpactFX, 1f);
                Destroy(this.gameObject);
            }
        }
    }
}
