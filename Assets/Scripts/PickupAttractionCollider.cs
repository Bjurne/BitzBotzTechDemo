using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttractionCollider : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float attractionForce = 25f;
    protected bool active;

    private void OnEnable()
    {
        active = false;
        rb = GetComponentInParent<Rigidbody2D>();
        Invoke("Activate", 0.5f);
    }

    private void Activate()
    {
        active = true;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (active && collision.tag == "Player")
        {
            rb.AddForce((collision.transform.position - transform.position) * attractionForce);
        }
    }
}
