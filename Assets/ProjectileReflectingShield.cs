using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReflectingShield : MonoBehaviour
{
    private bool active;
    private GameObject shieldedCharacter;

    public void Activate(GameObject characterToShield)
    {
        active = true;
        shieldedCharacter = characterToShield;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.GetComponent<Projectile>() != null)
            {
                Projectile projectile = collision.GetComponent<Projectile>();

                if (projectile.active && projectile.fireingCharacterName != transform.root.gameObject.name)
                {
                    Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        rb.velocity *= -1f;
                    }
                }
            }
        }
    }
}
