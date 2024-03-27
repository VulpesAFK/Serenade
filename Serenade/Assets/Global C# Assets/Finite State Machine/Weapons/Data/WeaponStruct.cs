using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponStruct
{
    public string AttackName;
    public float MovementSpeed;
    public float DamageAmount;

    public float KnockbackStrength;
    public Vector2 KnockbackAngle;
}
