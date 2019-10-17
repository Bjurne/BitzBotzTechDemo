using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTarget : Projectile
{
    public GameObject targetSeekingMissile;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
        targetSeekingMissile.transform.SetParent(null);
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
        if (targetSeekingMissile != null)
        {
            targetSeekingMissile.GetComponent<Rigidbody2D>().gravityScale = 1f;
            Destroy(targetSeekingMissile, 5f);
        }
    }
}
