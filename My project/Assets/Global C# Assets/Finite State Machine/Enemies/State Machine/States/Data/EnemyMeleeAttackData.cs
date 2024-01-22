using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class EnemyMeleeAttackData : ScriptableObject
{
    public float AttackRadius = 0.5f;
    public LayerMask WhatIsPlayer;
    public Vector2 KnockbackAngle = Vector2.one;
    public float KnockbackStrength = 15;
    public float AttackDamage = 10;
}
