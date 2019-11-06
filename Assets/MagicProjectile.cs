using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : Projectile
{
    public ParticleSystem LightningEffectPS;

    override protected void OnEnable()
    {
        base.OnEnable();
        Invoke("SuperCharged", 1.5f);
    }

    private void SuperCharged()
    {
        GetComponent<Projectile>().projectileDamage = 14;
    }
}
