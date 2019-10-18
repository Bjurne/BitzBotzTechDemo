using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    BitzBox thisBitzBox;

    private void Start()
    {
        thisBitzBox = GetComponentInParent<BitzBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            thisBitzBox.PickedUp(playerController);
            ObjectPoolManager.Instance.ReturnObjectHome(thisBitzBox.gameObject);
        }
    }
}
