using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperMineDetonationTrigger : MonoBehaviour
{
    private bool active;

    private void OnEnable()
    {
        active = false;
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
            playerController.TakeDamage(8);
            playerController.playerRigidBody.AddExplosionForce(15000f, transform.position, 0.6f);
            ObjectPoolManager.Instance.SpawnFromPool("Explosion", transform.position);
            ObjectPoolManager.Instance.ReturnObjectHome(transform.parent.gameObject);
        }
    }
}
