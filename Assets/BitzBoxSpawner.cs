using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitzBoxSpawner : MonoBehaviour
{
    public GameObject bitzBoxPrefab;
    public float spawnRate = 15f;
    private float time;

    void Update()
    {
        if (time < spawnRate) time += Time.deltaTime;
        else
        {
            time = 0f;
            Instantiate(bitzBoxPrefab, transform.position, Quaternion.identity);
        }
    }
}
