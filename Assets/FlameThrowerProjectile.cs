using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerProjectile : Projectile
{
    public Rigidbody2D[] childProjectiles;
    public float expansionRate;

    public SpriteRenderer child01Sprite;
    public SpriteRenderer child02Sprite;
    public GameObject[] childProjectile;

    public Rigidbody2D rigidBody;

    void Start()
    {
        child01Sprite.sprite = base.sprite.sprite;
        child02Sprite.sprite = base.sprite.sprite;

        foreach (GameObject child in childProjectile)
        {
            //Vector2 randomExpansionForce = UnityEngine.Random.insideUnitCircle * expansionRate;

            //child.transform.Translate(randomExpansionForce);
            StartCoroutine(LerpChildPosition(child));
        }

        foreach (Rigidbody2D rb in childProjectiles)
        {
            Vector2 randomExpansionForce = UnityEngine.Random.insideUnitCircle * expansionRate;
            rb.AddForce(randomExpansionForce);
        }
    }

    private IEnumerator LerpChildPosition(GameObject child)
    {
        float timeStartedLerping = Time.time;
        Vector2 startPostition = child.transform.localPosition;
        Vector2 endPosition = UnityEngine.Random.insideUnitCircle * expansionRate;

        float timeSinceStarted = 0f;
        float percentageComplete = 0f;
        float timeTakenDuringLerp = secondsActiveBeforeDespawned;
        
        while (percentageComplete <= 1f)
        {
            child.transform.localPosition = (Vector2.Lerp(startPostition, endPosition, percentageComplete));
            //percentageComplete += 0.01f;

            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
}
