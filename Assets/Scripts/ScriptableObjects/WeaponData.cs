using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float coolDown;
    public int numberOfProjectiles;
    public float projectileInterval;
    public int weaponDamage;
    public float projectileDeviation;
    public float weaponRecoilPower;
    public bool projectilesAreMouseSeeking;
}
