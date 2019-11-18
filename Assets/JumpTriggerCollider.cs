using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTriggerCollider : PickupAttractionCollider
{
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb.AddForce(((collision.transform.position + Vector3.one) - transform.position) * attractionForce);
        }
    }
}
