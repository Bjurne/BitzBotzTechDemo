using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapSlime : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea"))
        {
            try
            {
                if (collision.attachedRigidbody.gravityScale > 0)
                {
                    collision.attachedRigidbody.gravityScale *= -1f;
                    collision.attachedRigidbody.velocity *= 0.6f;
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("TriggerArea"))
        {
            try
            {
                if (collision.attachedRigidbody.gravityScale < 0)
                {
                    collision.attachedRigidbody.gravityScale *= -1f;
                    collision.attachedRigidbody.velocity *= 0.6f;
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
    }
}
