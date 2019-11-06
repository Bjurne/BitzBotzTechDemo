using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager _instance;

    public static ObjectPoolManager Instance { get { return _instance; } }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab);
                newObject.SetActive(false);
                newObject.transform.SetParent(this.transform);
                objectPool.Enqueue(newObject);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, GameObject spawningCharacter = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn == null) return null;

        if (objectToSpawn.activeInHierarchy)
        {
            ReturnObjectHome(objectToSpawn);
        }

        if (tag == "Projectile") 
        {
            if (spawningCharacter != null)
            {
                objectToSpawn.transform.SetParent(spawningCharacter.transform);
            }
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.SetParent(this.transform);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnObjectHome(GameObject returningObject)
    {
        if (returningObject.activeInHierarchy) returningObject.SetActive(false);
        if (returningObject.transform.position != transform.position) returningObject.transform.position = this.transform.position;
        if (returningObject.transform.parent != transform) returningObject.transform.SetParent(this.transform);
        try
        {
            Rigidbody2D rb = returningObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            if (rb.gravityScale < 0) rb.gravityScale *= -1;
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
}
