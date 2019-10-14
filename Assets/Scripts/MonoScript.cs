using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoScript : MonoBehaviour
{
    public void StartNonMonoCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }
}
