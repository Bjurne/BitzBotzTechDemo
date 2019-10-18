﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSeekingMissile : MonoBehaviour
{
    private bool active = false;
    private Projectile missileTargetProjectile;
    private Rigidbody2D rigidBody;
    public Vector2 maxVelocity = new Vector2(35f, 35f);
    public GameObject explosionPrefab;
    public int directHitDamage;


    private void Awake()
    {
        missileTargetProjectile = GetComponentInParent<MissileTarget>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveDirection = rigidBody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (moveDirection.x >= maxVelocity.x || moveDirection.y >= maxVelocity.y)
        {
            Destroy(missileTargetProjectile);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == missileTargetProjectile.fireingCharacterName)
        {
            active = true;
        }
    }

    private void OnDestroy()
    {
        if (missileTargetProjectile != null) Destroy(missileTargetProjectile.gameObject);
        //GameObject newExplosion = Instantiate(explosionPrefab, this.transform);
        //newExplosion.transform.SetParent(null);
        //Destroy(newExplosion, 1.5f);
        ObjectPoolManager.Instance.SpawnFromPool("Explosion", this.transform.position);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea"))
        {
            if (active)
                {
                ITakeDamage takeDamageInterface = collision.gameObject.GetComponent<ITakeDamage>();
                if (takeDamageInterface != null)
                {
                    takeDamageInterface.TakeDamage(directHitDamage);
                }

                if (missileTargetProjectile != null) Destroy(missileTargetProjectile.gameObject);
                    Destroy(this.gameObject);
                }

        }
    }
}
