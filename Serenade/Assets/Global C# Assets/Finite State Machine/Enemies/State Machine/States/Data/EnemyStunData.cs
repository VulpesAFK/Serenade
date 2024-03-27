using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class EnemyStunData : ScriptableObject
{
    public float StunTime = 2;
    public float StunKnockBackTime = 0.2f;
    public Vector2 StunKnockBackAngle;
    public float StunKnockBackVelocity = 20;
}
