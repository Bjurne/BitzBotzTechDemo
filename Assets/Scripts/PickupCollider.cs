using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    BitzBox thisBitzBox;
    private bool active;

    private void OnEnable()
    {
        active = false;
        thisBitzBox = GetComponentInParent<BitzBox>();
        Invoke("Activate", 0.5f);
    }

    private void Activate()
    {
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            thisBitzBox.PickedUp(playerController);
            ObjectPoolManager.Instance.ReturnObjectHome(thisBitzBox.gameObject);
        }
    }
}
