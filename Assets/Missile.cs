using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public GameObject sprite;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
        sprite.transform.SetParent(null);
    }

    private void Update()
    {
        //Vector2 moveDirection = rigidBody.velocity;
        //if (moveDirection != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //    sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }

    private void OnDestroy()
    {
        if (sprite != null) Destroy(sprite, 5f);
    }
}
