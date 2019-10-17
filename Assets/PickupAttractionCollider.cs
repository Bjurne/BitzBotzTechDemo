using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttractionCollider : MonoBehaviour
{
    Rigidbody2D rb;
    public float attractionForce = 25f;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb.AddForce((collision.transform.position - transform.position) * attractionForce);
        }
    }
}
