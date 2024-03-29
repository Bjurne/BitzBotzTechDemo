﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public float secondsBeforeDeactivation;

    private void OnEnable()
    {
        Invoke("Deactivate", secondsBeforeDeactivation);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivationBlinking());
    }
    

    private IEnumerator DeactivationBlinking()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            for (int i = 0; i < 3; i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.5f);
            }
            gameObject.SetActive(false);
            yield return null;
        }
        else
        {
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
