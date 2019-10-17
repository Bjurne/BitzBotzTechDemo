using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public float secondsBeforeDeactivation;

    private void OnEnable()
    {
        Invoke("Deactivate", secondsBeforeDeactivation);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
