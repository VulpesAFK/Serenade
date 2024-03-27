using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Data/Entity Data/Base Data")]
public class EnemyEntityData : ScriptableObject
{
    public float WallCheckDistance = 0.2f;
    public float LedgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float CloseRangeActionDistance = 1;
    
    
    public float MaxAggroDistance = 4f;
    public float MinAggroDistance = 3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float StunResistance = 3f;
    public float StunRecoveryTime = 2f;

    public GameObject HitParticle;

    public float MaxHealth = 30;
    public float DamageHopVelocity = 10f;
}
