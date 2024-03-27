using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class EnemyRangeAttackData : ScriptableObject
{
    public GameObject Projectile;
    public float ProjectileDamage = 10;
    public float ProjectileVelocity = 20;
    public float ProjectileTravelDistance = 13;
}
 